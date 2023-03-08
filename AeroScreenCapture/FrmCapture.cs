﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using static AeroScreenCapture.Win32;

namespace AeroScreenCapture
{
  public partial class FrmCapture : Form
  {
    public event Action<Bitmap> CaptureFinished;

    #region 构造函数

    public FrmCapture()
    {
      InitializeComponent();

      SetStyle(ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.Selectable
                 | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.SupportsTransparentBackColor,
               true);

      // 如果1号屏幕在右边，那就不行了
      FormBorderStyle = FormBorderStyle.None;
      Location = StartLocation;

      Size = new Size(ScreenWidth, ScreenHeight);
      TopMost = true;
      ShowInTaskbar = false;

      m_MHook = new MouseHook();
      this.FormClosing += (s, e) =>
      {
        try
        {
          this.timer1.Dispose();
          m_MHook.UnLoadHook();
          this.DeleteResource();
        }
        catch (Exception exception)
        {
          System.Diagnostics.Debug.WriteLine($"FormClosing {exception}");
        }
      };
      this.panel1.Paint += panel1_Paint;
      this.panel2.Paint += panel1_Paint;
      imageProcessBox1.MouseLeave += (s, e) => this.Cursor = Cursors.Default;
      //后期一些操作历史记录图层
      m_layer = new List<Bitmap>();

    }

    private static void panel1_Paint(object sender, PaintEventArgs e)
    {
      var pel = (Panel)sender;
      pel.BackColor = Color.FromArgb(222, 238, 254);
      var bdColor = Color.FromArgb(71, 150, 207);
      ControlPaint.DrawBorder(e.Graphics, pel.ClientRectangle,
          bdColor, 1, ButtonBorderStyle.Solid,
          bdColor, 1, ButtonBorderStyle.Solid,
          bdColor, 1, ButtonBorderStyle.Solid,
          bdColor, 1, ButtonBorderStyle.Solid);
    }

    private void DeleteResource()
    {
      m_bmpLayerCurrent?.Dispose();
      m_bmpLayerShow?.Dispose();
      m_layer.Clear();
      imageProcessBox1.DeleteResource();
      GC.Collect();
    }

    #endregion

    #region Properties

    /// <summary>
    /// 多个屏幕最左上角的点
    /// </summary>
    public static Point StartLocation => new Point(Screen.AllScreens.Min(s => s.Bounds.X),
        Screen.AllScreens.Min(s => s.Bounds.Y));

    public static int ScreenWidth => Screen.AllScreens.Sum(screen => screen.Bounds.Width);

    public static int ScreenHeight => Screen.AllScreens.Sum(screen => screen.Bounds.Height);

    /// <summary>
    /// 获取或设置是否捕获鼠标
    /// </summary>
    public bool IsCaptureCursor { get; set; }

    /// <summary>
    /// 获取或设置是否从剪切板获取图像
    /// </summary>
    public bool IsFromClipBoard { get; set; }

    /// <summary>
    /// 获取或设置是否显示图像信息
    /// </summary>
    public bool ImgProcessBoxIsShowInfo
    {
      get => imageProcessBox1.IsShowInfo;
      set => imageProcessBox1.IsShowInfo = value;
    }
    /// <summary>
    /// 获取或设置操作框点的颜色
    /// </summary>
    public Color ImgProcessBoxDotColor
    {
      get => imageProcessBox1.DotColor;
      set => imageProcessBox1.DotColor = value;
    }
    /// <summary>
    /// 获取或设置操作框边框颜色
    /// </summary>
    public Color ImgProcessBoxLineColor
    {
      get => imageProcessBox1.LineColor;
      set => imageProcessBox1.LineColor = value;
    }

    /// <summary>
    /// 获取或设置放大图形的原始尺寸
    /// </summary>
    public Size ImgProcessBoxMagnifySize
    {
      get => imageProcessBox1.MagnifySize;
      set => imageProcessBox1.MagnifySize = value;
    }

    /// <summary>
    /// 获取或设置放大图像的倍数
    /// </summary>
    public int ImgProcessBoxMagnifyTimes
    {
      get => imageProcessBox1.MagnifyTimes;
      set => imageProcessBox1.MagnifyTimes = value;
    }
    /// <summary>
    /// 获取ImageProcessBox控件对象
    /// </summary>
    public ImageProcessBox ImgProcessBox
    { get => imageProcessBox1; }
    #endregion

    private readonly MouseHook m_MHook;
    private readonly List<Bitmap> m_layer;       //记录历史图层

    private bool m_isStartDraw;
    private Point m_ptOriginal;
    private Point m_ptCurrent;
    private Bitmap m_bmpLayerCurrent;
    private Bitmap m_bmpLayerShow;

    private void FrmCapture_Load(object sender, EventArgs e)
    {
      this.InitMember();
      imageProcessBox1.BaseImage = GetScreen(IsCaptureCursor, IsFromClipBoard);
      //imageProcessBox1.BaseImage.Save("C://1.bmp", ImageFormat.Bmp);
      m_MHook.SetHook();
      m_MHook.MHookEvent += m_MHook_MHookEvent;
      imageProcessBox1.IsDrawOperationDot = false;
      this.BeginInvoke(new MethodInvoker(() => Enabled = false));
      timer1.Interval = 10;
      timer1.Enabled = true;
    }

    /// <summary>
    /// 初始化参数
    /// </summary>
    private void InitMember()
    {
      panel1.Visible = false;
      panel2.Visible = false;
      panel1.BackColor = Color.White;
      panel2.BackColor = Color.White;
      panel1.Height = tBtn_Finish.Bottom + 3;
      panel1.Width = tBtn_Finish.Right + 3;
      panel2.Height = colorBox1.Height;
      panel1.Paint += (s, e) => e.Graphics.DrawRectangle(Pens.SteelBlue, 0, 0, panel1.Width - 1, panel1.Height - 1);
      panel2.Paint += (s, e) => e.Graphics.DrawRectangle(Pens.SteelBlue, 0, 0, panel2.Width - 1, panel2.Height - 1);

      tBtn_Rect.Click += selectToolButton_Click;
      tBtn_Ellipse.Click += selectToolButton_Click;
      tBtn_Arrow.Click += selectToolButton_Click;
      tBtn_Brush.Click += selectToolButton_Click;
      tBtn_Text.Click += selectToolButton_Click;
      tBtn_Close.Click += tBtn_Close_Click;

      textBox1.BorderStyle = BorderStyle.None;
      textBox1.Visible = false;
      textBox1.ForeColor = Color.Red;
      colorBox1.ColorChanged += (s, e) => textBox1.ForeColor = e.Color;
    }

    /// <summary>
    /// 鼠标钩子
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void m_MHook_MHookEvent(object sender, MHookEventArgs e)
    {
      //如果窗体禁用 调用控件的方法设置信息显示位置
      //貌似Hook不能精确坐标(Hook最先执行执 执行完后的坐标可能与执行时传入的坐标发生了变化
      //猜测是这样) 所以放置了一个timer检测
      if (!Enabled)
      {
        imageProcessBox1.SetInfoPoint(MousePosition.X, MousePosition.Y);
      }

      //鼠标点下恢复窗体禁用
      if (e.MButton == ButtonStatus.LeftDown || e.MButton == ButtonStatus.RightDown)
      {
        Enabled = true;
        imageProcessBox1.IsDrawOperationDot = true;
      }

      #region 右键抬起

      if (e.MButton == ButtonStatus.RightUp)
      {
        if (!imageProcessBox1.IsDrawed) //没有绘制那么退出(直接this.Close右键将传递到下面)
        {
          OnCaptureFinished(null);
          BeginInvoke(new MethodInvoker(Close));
        }
      }

      #endregion

      #region 找寻窗体

      if (!Enabled)
        FoundAndDrawWindowRect();

      #endregion
    }

    #region 工具条按钮事件

    /// <summary>
    /// 工具条前五个按钮绑定的公共事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void selectToolButton_Click(object sender, EventArgs e)
    {
      panel2.Visible = ((ToolButton)sender).IsSelected;
      if (panel2.Visible) imageProcessBox1.CanReset = false;
      else { imageProcessBox1.CanReset = m_layer.Count == 0; }
      this.SetToolBarLocation();
    }

    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tBtn_Close_Click(object sender, EventArgs e)
    {
      //this.imageProcessBox1.Dispose();
      OnCaptureFinished(null);
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    /// <summary>
    /// 撤销
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tBtn_Cancel_Click(object sender, EventArgs e)
    {
      using (Graphics g = Graphics.FromImage(m_bmpLayerShow))
      {
        g.Clear(Color.Transparent);     //清空当前临时显示的图像
      }
      if (m_layer.Count > 0)
      {            //删除最后一层
        m_layer.RemoveAt(m_layer.Count - 1);
        if (m_layer.Count > 0)
          m_bmpLayerCurrent = m_layer[m_layer.Count - 1].Clone() as Bitmap;
        else
          m_bmpLayerCurrent = imageProcessBox1.GetResultBmp();
        imageProcessBox1.Invalidate();
        imageProcessBox1.CanReset = m_layer.Count == 0 && !HaveSelectedToolButton();
      }
      else
      {                            //如果没有历史记录则取消本次截图
        this.Enabled = false;
        imageProcessBox1.ClearDraw();
        imageProcessBox1.IsDrawOperationDot = false;
        panel1.Visible = false;
        panel2.Visible = false;
      }
    }

    /// <summary>
    /// 图像保存到文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tBtn_Save_Click(object sender, EventArgs e)
    {
      //m_bSave = true;
      using (var saveDlg = new SaveFileDialog()
      {
        Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|PNG(*.png)|*.png|GIF(*.gif)|*.gif",
        FilterIndex = 3,
        FileName = "CAPTURE_" + GetTimeString()
      })
      {
        if (saveDlg.ShowDialog() == DialogResult.OK)
        {
          switch (saveDlg.FilterIndex)
          {
            case 1:
              m_bmpLayerCurrent.Clone(new Rectangle(0, 0, m_bmpLayerCurrent.Width, m_bmpLayerCurrent.Height),
                  System.Drawing.Imaging.PixelFormat.Format24bppRgb).Save(saveDlg.FileName,
                  System.Drawing.Imaging.ImageFormat.Bmp);
              this.Close();
              break;
            case 2:
              m_bmpLayerCurrent.Save(saveDlg.FileName,
                  System.Drawing.Imaging.ImageFormat.Jpeg);
              this.Close();
              break;
            case 3:
              m_bmpLayerCurrent.Save(saveDlg.FileName,
                  System.Drawing.Imaging.ImageFormat.Png);
              this.Close();
              break;
            case 4:
              m_bmpLayerCurrent.Save(saveDlg.FileName,
                  System.Drawing.Imaging.ImageFormat.Gif);
              this.Close();
              break;
          }
        }
      }
      //m_bSave = false;
    }

    /// <summary>
    /// 截图完成 触发OnCaptureFinished事件,同时保存到剪切板
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tBtn_Finish_Click(object sender, EventArgs e)
    {
      try
      {
        Clipboard.SetImage(m_bmpLayerCurrent);
        this.OnCaptureFinished(m_bmpLayerCurrent);
      }
      catch (Exception exception)
      {
        System.Diagnostics.Debug.WriteLine($"tBtn_Finish_Click {exception}");
      }
      finally
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void imageProcessBox1_DoubleClick(object sender, EventArgs e)
    {
      tBtn_Finish_Click(sender, e);
    }

    #endregion

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (!Enabled)
        imageProcessBox1.SetInfoPoint(MousePosition.X, MousePosition.Y);
    }

    private void FoundAndDrawWindowRect()
    {
      var pt = new LPPOINT
      {
        X = MousePosition.X,
        Y = MousePosition.Y
      };

      // 如果第二屏幕在左边，那么第二屏幕的坐标是负数
      // System.Diagnostics.Debug.WriteLine($"鼠标坐标: {MousePosition.X},{MousePosition.Y}");

      var hWnd = ChildWindowFromPointEx(GetDesktopWindow(), pt, CWP_SKIPINVISIBL | CWP_SKIPDISABLED);
      if (hWnd == IntPtr.Zero)
      {
        // System.Diagnostics.Debug.WriteLine($"hWnd == IntPtr.Zero: {MousePosition.X},{MousePosition.Y}");
        return;
      }

      var hTemp = hWnd;
      while (true)
      {
        ScreenToClient(hTemp, out pt);
        hTemp = ChildWindowFromPointEx(hWnd, pt, CWP_SKIPINVISIBL);
        if (hTemp == IntPtr.Zero || hTemp == hWnd)
          break;
        hWnd = hTemp;
        pt.X = MousePosition.X;
        pt.Y = MousePosition.Y; // 坐标还原为屏幕坐标
      }

      GetWindowRect(hWnd, out var rect);
      imageProcessBox1.SetSelectRect(
          new Rectangle(rect.Left - StartLocation.X, rect.Top - StartLocation.Y,
              rect.Right - rect.Left, rect.Bottom - rect.Top));
    }

    #region 文本框

    //文本改变时重置文本框大小
    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        var se = TextRenderer.MeasureText(textBox1.Text, textBox1.Font);
        textBox1.Size = se.IsEmpty ? new Size(50, textBox1.Font.Height) : se;
      }
      catch (Exception exception)
      {
        System.Diagnostics.Debug.WriteLine($"textBox1_TextChanged {exception}");
        this.Close();
      }

    }

    //文本框失去焦点时 绘制文本
    private void textBox1_Validating(object sender, CancelEventArgs e)
    {
      textBox1.Visible = false;
      if (string.IsNullOrEmpty(textBox1.Text.Trim())) { textBox1.Text = ""; return; }
      using (var g = Graphics.FromImage(m_bmpLayerCurrent))
      {
        var sb = new SolidBrush(colorBox1.SelectedColor);
        g.DrawString(textBox1.Text, textBox1.Font, sb,
            textBox1.Left - imageProcessBox1.SelectedRectangle.Left,
            textBox1.Top - imageProcessBox1.SelectedRectangle.Top);
        sb.Dispose();
        textBox1.Text = "";
        SetLayer();        //将文本绘制到当前图层并存入历史记录
        imageProcessBox1.Invalidate();
      }
    }

    //窗体大小改变时重置字体 从控件中获取字体大小
    private void textBox1_Resize(object sender, EventArgs e)
    {
      //在三个大小选择的按钮点击中设置字体大小太麻烦 所以Resize中获取设置
      int se = 10;
      if (toolButton2.IsSelected) se = 12;
      if (toolButton3.IsSelected) se = 14;
      if (this.textBox1.Font.Height == se) return;
      textBox1.Font = new Font(this.Font.FontFamily, se);
    }


    #endregion

    #region 截图后的一些后期绘制

    private void imageProcessBox1_MouseDown(object sender, MouseEventArgs e)
    {
      if (imageProcessBox1.Cursor != Cursors.SizeAll &&
          imageProcessBox1.Cursor != Cursors.Default)
        panel1.Visible = false;         //表示改变选取大小 隐藏工具条
                                        //若果在选区内点击 并且有选择工具
      if (e.Button == MouseButtons.Left && imageProcessBox1.IsDrawed && HaveSelectedToolButton())
      {
        if (imageProcessBox1.SelectedRectangle.Contains(e.Location))
        {
          if (tBtn_Text.IsSelected)
          {         //如果选择的是绘制文本 弹出文本框
            textBox1.Location = e.Location;
            textBox1.Visible = true;
            textBox1.Focus();
            return;
          }
          m_isStartDraw = true;
          Cursor.Clip = imageProcessBox1.SelectedRectangle;
        }
      }
      m_ptOriginal = e.Location;
    }

    private void imageProcessBox1_MouseMove(object sender, MouseEventArgs e)
    {
      m_ptCurrent = e.Location;
      //根据是否选择有工具决定 鼠标指针样式
      if (imageProcessBox1.SelectedRectangle.Contains(e.Location) && HaveSelectedToolButton() && imageProcessBox1.IsDrawed)
      {
        this.Cursor = Cursors.Cross;
      }
      else if (!imageProcessBox1.SelectedRectangle.Contains(e.Location))
      {
        this.Cursor = Cursors.Default;
      }

      if (imageProcessBox1.IsStartDraw && panel1.Visible)   //在重置选取的时候 重置工具条位置(成立于移动选取的时候)
        this.SetToolBarLocation();
      if (m_isStartDraw && m_bmpLayerShow != null)
      {        //如果在区域内点下那么绘制相应图形
        using (Graphics g = Graphics.FromImage(m_bmpLayerShow))
        {
          int tempWidth = 1;
          if (toolButton2.IsSelected) tempWidth = 3;
          if (toolButton3.IsSelected) tempWidth = 5;
          Pen p = new Pen(colorBox1.SelectedColor, tempWidth);
          g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

          #region   绘制矩形

          if (tBtn_Rect.IsSelected)
          {
            int tempX = e.X - m_ptOriginal.X > 0 ? m_ptOriginal.X : e.X;
            int tempY = e.Y - m_ptOriginal.Y > 0 ? m_ptOriginal.Y : e.Y;
            g.Clear(Color.Transparent);
            g.DrawRectangle(p, tempX - imageProcessBox1.SelectedRectangle.Left, tempY - imageProcessBox1.SelectedRectangle.Top, Math.Abs(e.X - m_ptOriginal.X), Math.Abs(e.Y - m_ptOriginal.Y));
            imageProcessBox1.Invalidate();
          }

          #endregion

          #region    绘制圆形

          if (tBtn_Ellipse.IsSelected)
          {
            g.DrawLine(Pens.Red, 0, 0, 200, 200);
            g.Clear(Color.Transparent);
            g.DrawEllipse(p, m_ptOriginal.X - imageProcessBox1.SelectedRectangle.Left, m_ptOriginal.Y - imageProcessBox1.SelectedRectangle.Top, e.X - m_ptOriginal.X, e.Y - m_ptOriginal.Y);
            imageProcessBox1.Invalidate();
          }

          #endregion

          #region    绘制箭头

          if (tBtn_Arrow.IsSelected)
          {
            g.Clear(Color.Transparent);
            System.Drawing.Drawing2D.AdjustableArrowCap lineArrow =
                new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5, true);
            p.CustomEndCap = lineArrow;
            g.DrawLine(p, (Point)((Size)m_ptOriginal - (Size)imageProcessBox1.SelectedRectangle.Location), (Point)((Size)m_ptCurrent - (Size)imageProcessBox1.SelectedRectangle.Location));
            imageProcessBox1.Invalidate();
          }

          #endregion

          #region    绘制线条

          if (tBtn_Brush.IsSelected)
          {
            Point ptTemp = (Point)((Size)m_ptOriginal - (Size)imageProcessBox1.SelectedRectangle.Location);
            p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            g.DrawLine(p, ptTemp, (Point)((Size)e.Location - (Size)imageProcessBox1.SelectedRectangle.Location));
            m_ptOriginal = e.Location;
            imageProcessBox1.Invalidate();
          }

          #endregion

          p.Dispose();
        }
      }
    }

    private void imageProcessBox1_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.IsDisposed) return;
      if (e.Button == MouseButtons.Right)
      {   //右键清空绘制
        this.Enabled = false;
        imageProcessBox1.ClearDraw();
        imageProcessBox1.CanReset = true;
        imageProcessBox1.IsDrawOperationDot = false;
        m_layer.Clear();    //清空历史记录
        m_bmpLayerCurrent = null;
        m_bmpLayerShow = null;
        ClearToolBarBtnSelected();
        panel1.Visible = false;
        panel2.Visible = false;
      }
      if (!imageProcessBox1.IsDrawed)
      {       //如果没有成功绘制选取 继续禁用窗体
        this.Enabled = false;
        imageProcessBox1.IsDrawOperationDot = false;
      }
      else if (!panel1.Visible)
      {
        //否则显示工具条
        this.SetToolBarLocation();          //重置工具条位置
        panel1.Visible = true;
        m_bmpLayerCurrent = imageProcessBox1.GetResultBmp();    //获取选取图形
        m_bmpLayerShow = new Bitmap(m_bmpLayerCurrent.Width, m_bmpLayerCurrent.Height);
      }
      //如果移动了选取位置 重新获取选取的图形
      if (imageProcessBox1.Cursor == Cursors.SizeAll && m_ptOriginal != e.Location)
      {
        m_bmpLayerCurrent.Dispose();
        m_bmpLayerCurrent = imageProcessBox1.GetResultBmp();
      }

      if (!m_isStartDraw) return;
      Cursor.Clip = Rectangle.Empty;
      m_isStartDraw = false;
      if (e.Location == m_ptOriginal && !tBtn_Brush.IsSelected) return;
      this.SetLayer();        //将绘制的图形绘制到历史图层中
    }

    //绘制后期操作
    private void imageProcessBox1_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      if (m_layer.Count > 0)  //绘制保存的历史记录的最后一张图
        g.DrawImage(m_layer[m_layer.Count - 1], imageProcessBox1.SelectedRectangle.Location);
      if (m_bmpLayerShow != null)     //绘制当前正在拖动绘制的图形(即鼠标点下还没有抬起确认的图形)
        g.DrawImage(m_bmpLayerShow, imageProcessBox1.SelectedRectangle.Location);
    }

    #endregion

    /// <summary>
    /// 获取整个桌面图像
    /// </summary>
    /// <param name="bCaptureCursor"></param>
    /// <param name="bFromClipBoard"></param>
    /// <returns></returns>
    public static Bitmap GetScreen(bool bCaptureCursor, bool bFromClipBoard)
    {
      var bmp = new Bitmap(ScreenWidth, ScreenHeight);
      if (bCaptureCursor)      //是否捕获鼠标
        DrawCurToScreen();

      //做完以上操作 才开始捕获桌面图像

      //create DC for the entire virtual screen
      var hdcSrc = CreateDC("DISPLAY", null, null, IntPtr.Zero);
      var hdcDest = CreateCompatibleDC(hdcSrc);
      var hBitmap = CreateCompatibleBitmap(hdcSrc, ScreenWidth, ScreenHeight);
      SelectObject(hdcDest, hBitmap);

      // set the destination area White - a little complicated
      using (Image ii = bmp)
      {
        using (var gf = Graphics.FromImage(ii))
        {
          var hdc = gf.GetHdc();
          //use whiteness flag to make destination screen white
          BitBlt(hdcDest, 0, 0, ScreenWidth, ScreenHeight, (int)hdc, 0, 0, 0x00FF0062);
        }
      }

      bmp.Dispose();

      //Now copy the areas from each screen on the destination hbitmap
      var screenData = Screen.AllScreens;
      int X, X1, Y, Y1;
      var x = StartLocation.X;
      var y = StartLocation.Y;
      var wid = ScreenWidth;
      var hei = ScreenHeight;

      foreach (var t in screenData)
      {
        if (t.Bounds.X > (x + wid) || (t.Bounds.X + t.Bounds.Width) < x || t.Bounds.Y > (y + hei) ||
            (t.Bounds.Y + t.Bounds.Height) < y)
        {
          // no common area
        }
        else
        {
          // something  common
          if (x < t.Bounds.X) X = t.Bounds.X; else X = x;
          if ((x + wid) > (t.Bounds.X + t.Bounds.Width))
            X1 = t.Bounds.X + t.Bounds.Width;
          else X1 = x + wid;
          if (y < t.Bounds.Y) Y = t.Bounds.Y; else Y = y;
          if ((y + hei) > (t.Bounds.Y + t.Bounds.Height))
            Y1 = t.Bounds.Y + t.Bounds.Height;
          else Y1 = y + hei;
          // Main API that does memory data transfer
          BitBlt(hdcDest, X - x, Y - y, X1 - X, Y1 - Y, hdcSrc, X, Y,
              0x40000000 | 0x00CC0020); //SRCCOPY AND CAPTUREBLT
        }
      }

      // send image to clipboard
      var imf = Image.FromHbitmap(new IntPtr(hBitmap));
      //Clipboard.SetImage(imf);
      DeleteDC(hdcSrc);
      DeleteDC(hdcDest);
      DeleteObject(hBitmap);

      return imf;
      //imf.Dispose();

      //// g.CopyFromScreen 无法捕捉多个显示器的
      //using (var g = Graphics.FromImage(bmp))
      //{
      //    g.CopyFromScreen(0, 0, 0, 0, bmp.Size);

      //    if (!bFromClipBoard) return bmp;
      //    using (var img_clip = Clipboard.GetImage())
      //    {
      //        if (img_clip == null) return bmp;
      //        using (var sb = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
      //        {
      //            g.FillRectangle(sb, 0, 0, bmp.Width, bmp.Height);
      //            g.DrawImage(img_clip,
      //                (bmp.Width - img_clip.Width) >> 1,
      //                (bmp.Height - img_clip.Height) >> 1,
      //                img_clip.Width, img_clip.Height);
      //        }
      //    }
      //}
      //return bmp;
    }

    /// <summary>
    /// 在桌面绘制鼠标
    /// </summary>
    /// <returns></returns>
    public static Rectangle DrawCurToScreen()
    {
      //如果直接将捕获当的鼠标画在bmp上 光标不会反色 指针边框也很浓 也就是说
      //尽管bmp上绘制了图像 绘制鼠标的时候还是以黑色作为鼠标的背景 然后在将混合好的鼠标绘制到图像 会很别扭
      //所以 干脆直接在桌面把鼠标绘制出来再截取桌面
      using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
      {   //传入0默认就是桌面 Win32.GetDesktopWindow()也可以
        Win32.PCURSORINFO pci;
        pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Win32.PCURSORINFO));
        Win32.GetCursorInfo(out pci);
        if (pci.hCursor != IntPtr.Zero)
        {
          Cursor cur = new Cursor(pci.hCursor);
          Rectangle rect_cur = new Rectangle((Point)((Size)MousePosition - (Size)cur.HotSpot), cur.Size);
          g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
          //g.CopyFromScreen(rect_cur.Location, rect_cur.Location, rect_cur.Size); //在桌面绘制鼠标前 先在桌面绘制一下当前的桌面图像
          //如果不绘制当前桌面 那么cur.Draw的时候会是用历史桌面的快照 进行鼠标的混合 那么到时候混出现底色(测试中就是这样的)
          cur.Draw(g, rect_cur);
          return rect_cur;
        }
        return Rectangle.Empty;
      }
    }

    /// <summary>
    /// 设置工具条位置
    /// </summary>
    private void SetToolBarLocation()
    {
      var tempX = imageProcessBox1.SelectedRectangle.Right - panel1.Width;
      var tempY = imageProcessBox1.SelectedRectangle.Bottom + 5;
      var tempHeight = panel2.Visible ? panel2.Height + 2 : 0;
      if (tempY + panel1.Height + tempHeight >= this.Height)
        tempY = imageProcessBox1.SelectedRectangle.Top - panel1.Height - 10 - imageProcessBox1.Font.Height;

      if (tempY - tempHeight <= 0)
      {
        if (imageProcessBox1.SelectedRectangle.Top - 5 - imageProcessBox1.Font.Height >= 0)
          tempY = imageProcessBox1.SelectedRectangle.Top + 5;
        else
          tempY = imageProcessBox1.SelectedRectangle.Top + 10 + imageProcessBox1.Font.Height;
      }

      if (tempX + panel1.Width >= Width)
        tempX = Width - panel1.Width - 5;
      panel1.Left = tempX;
      panel2.Left = tempX;
      panel1.Top = tempY;
      panel2.Top = imageProcessBox1.SelectedRectangle.Top > tempY ? tempY - tempHeight : panel1.Bottom + 2;
    }

    //确定是否工具条上面有被选中的按钮
    private bool HaveSelectedToolButton()
    {
      return tBtn_Rect.IsSelected || tBtn_Ellipse.IsSelected
          || tBtn_Arrow.IsSelected || tBtn_Brush.IsSelected
          || tBtn_Text.IsSelected;
    }

    //清空选中的工具条上的工具
    private void ClearToolBarBtnSelected()
    {
      tBtn_Rect.IsSelected = tBtn_Ellipse.IsSelected = tBtn_Arrow.IsSelected =
          tBtn_Brush.IsSelected = tBtn_Text.IsSelected = false;
    }

    /// <summary>
    /// 设置历史图层
    /// </summary>
    private void SetLayer()
    {
      if (IsDisposed) return;
      using (var g = Graphics.FromImage(m_bmpLayerCurrent))
      {
        g.DrawImage(m_bmpLayerShow, 0, 0);
      }
      var bmpTemp = m_bmpLayerCurrent.Clone() as Bitmap;
      m_layer.Add(bmpTemp);
    }

    /// <summary>
    /// 保存时获取当前时间字符串作文默认文件名
    /// </summary>
    /// <returns></returns>
    private static string GetTimeString()
    {
      var time = DateTime.Now;
      return time.Date.ToShortDateString().Replace("/", "") + "_" +
          time.ToLongTimeString().Replace(":", "");
    }

    private void OnCaptureFinished(Bitmap bitmap)
    {
      CaptureFinished?.Invoke(bitmap);
    }
    /// <summary>
    /// 按ESC退出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmCapture_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        OnCaptureFinished(null);
        this.DialogResult = DialogResult.Cancel;
        this.Close();
      }
    }

    private void imageProcessBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        OnCaptureFinished(null);
        this.DialogResult = DialogResult.Cancel;
        this.Close();
      }
    }
  }
}
