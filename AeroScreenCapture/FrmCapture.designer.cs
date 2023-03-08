﻿namespace AeroScreenCapture
{
    partial class FrmCapture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.tBtn_Finish = new AeroScreenCapture.ToolButton();
      this.tBtn_Close = new AeroScreenCapture.ToolButton();
      this.tBtn_Save = new AeroScreenCapture.ToolButton();
      this.tBtn_Cancel = new AeroScreenCapture.ToolButton();
      this.tBtn_Text = new AeroScreenCapture.ToolButton();
      this.tBtn_Brush = new AeroScreenCapture.ToolButton();
      this.tBtn_Arrow = new AeroScreenCapture.ToolButton();
      this.tBtn_Ellipse = new AeroScreenCapture.ToolButton();
      this.tBtn_Rect = new AeroScreenCapture.ToolButton();
      this.panel2 = new System.Windows.Forms.Panel();
      this.toolButton1 = new AeroScreenCapture.ToolButton();
      this.toolButton3 = new AeroScreenCapture.ToolButton();
      this.toolButton2 = new AeroScreenCapture.ToolButton();
      this.colorBox1 = new AeroScreenCapture.ColorBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.imageProcessBox1 = new AeroScreenCapture.ImageProcessBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.pictureBox2);
      this.panel1.Controls.Add(this.pictureBox1);
      this.panel1.Controls.Add(this.tBtn_Finish);
      this.panel1.Controls.Add(this.tBtn_Close);
      this.panel1.Controls.Add(this.tBtn_Save);
      this.panel1.Controls.Add(this.tBtn_Cancel);
      this.panel1.Controls.Add(this.tBtn_Text);
      this.panel1.Controls.Add(this.tBtn_Brush);
      this.panel1.Controls.Add(this.tBtn_Arrow);
      this.panel1.Controls.Add(this.tBtn_Ellipse);
      this.panel1.Controls.Add(this.tBtn_Rect);
      this.panel1.Location = new System.Drawing.Point(12, 90);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(292, 27);
      this.panel1.TabIndex = 1;
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::AeroScreenCapture.Properties.Resources.separator;
      this.pictureBox2.Location = new System.Drawing.Point(226, 1);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(2, 26);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox2.TabIndex = 10;
      this.pictureBox2.TabStop = false;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::AeroScreenCapture.Properties.Resources.separator;
      this.pictureBox1.Location = new System.Drawing.Point(138, 1);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(2, 26);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 9;
      this.pictureBox1.TabStop = false;
      // 
      // tBtn_Finish
      // 
      this.tBtn_Finish.Image = global::AeroScreenCapture.Properties.Resources.ok;
      this.tBtn_Finish.IsSelected = false;
      this.tBtn_Finish.IsSelectedButton = false;
      this.tBtn_Finish.IsSingleSelectedButton = false;
      this.tBtn_Finish.Location = new System.Drawing.Point(235, 3);
      this.tBtn_Finish.Name = "tBtn_Finish";
      this.tBtn_Finish.Size = new System.Drawing.Size(52, 21);
      this.tBtn_Finish.TabIndex = 8;
      this.tBtn_Finish.Text = "完成";
      this.tBtn_Finish.Click += new System.EventHandler(this.tBtn_Finish_Click);
      // 
      // tBtn_Close
      // 
      this.tBtn_Close.Image = global::AeroScreenCapture.Properties.Resources.close;
      this.tBtn_Close.IsSelected = false;
      this.tBtn_Close.IsSelectedButton = false;
      this.tBtn_Close.IsSingleSelectedButton = false;
      this.tBtn_Close.Location = new System.Drawing.Point(201, 3);
      this.tBtn_Close.Name = "tBtn_Close";
      this.tBtn_Close.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Close.TabIndex = 7;
      // 
      // tBtn_Save
      // 
      this.tBtn_Save.Image = global::AeroScreenCapture.Properties.Resources.save;
      this.tBtn_Save.IsSelected = false;
      this.tBtn_Save.IsSelectedButton = false;
      this.tBtn_Save.IsSingleSelectedButton = false;
      this.tBtn_Save.Location = new System.Drawing.Point(175, 3);
      this.tBtn_Save.Name = "tBtn_Save";
      this.tBtn_Save.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Save.TabIndex = 6;
      this.tBtn_Save.Click += new System.EventHandler(this.tBtn_Save_Click);
      // 
      // tBtn_Cancel
      // 
      this.tBtn_Cancel.Image = global::AeroScreenCapture.Properties.Resources.cancel;
      this.tBtn_Cancel.IsSelected = false;
      this.tBtn_Cancel.IsSelectedButton = false;
      this.tBtn_Cancel.IsSingleSelectedButton = false;
      this.tBtn_Cancel.Location = new System.Drawing.Point(147, 3);
      this.tBtn_Cancel.Name = "tBtn_Cancel";
      this.tBtn_Cancel.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Cancel.TabIndex = 5;
      this.tBtn_Cancel.Click += new System.EventHandler(this.tBtn_Cancel_Click);
      // 
      // tBtn_Text
      // 
      this.tBtn_Text.Image = global::AeroScreenCapture.Properties.Resources.text;
      this.tBtn_Text.IsSelected = false;
      this.tBtn_Text.IsSelectedButton = true;
      this.tBtn_Text.IsSingleSelectedButton = false;
      this.tBtn_Text.Location = new System.Drawing.Point(113, 3);
      this.tBtn_Text.Name = "tBtn_Text";
      this.tBtn_Text.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Text.TabIndex = 4;
      // 
      // tBtn_Brush
      // 
      this.tBtn_Brush.Image = global::AeroScreenCapture.Properties.Resources.brush;
      this.tBtn_Brush.IsSelected = false;
      this.tBtn_Brush.IsSelectedButton = true;
      this.tBtn_Brush.IsSingleSelectedButton = false;
      this.tBtn_Brush.Location = new System.Drawing.Point(86, 3);
      this.tBtn_Brush.Name = "tBtn_Brush";
      this.tBtn_Brush.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Brush.TabIndex = 3;
      // 
      // tBtn_Arrow
      // 
      this.tBtn_Arrow.Image = global::AeroScreenCapture.Properties.Resources.arrow;
      this.tBtn_Arrow.IsSelected = false;
      this.tBtn_Arrow.IsSelectedButton = true;
      this.tBtn_Arrow.IsSingleSelectedButton = false;
      this.tBtn_Arrow.Location = new System.Drawing.Point(59, 3);
      this.tBtn_Arrow.Name = "tBtn_Arrow";
      this.tBtn_Arrow.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Arrow.TabIndex = 2;
      // 
      // tBtn_Ellipse
      // 
      this.tBtn_Ellipse.Image = global::AeroScreenCapture.Properties.Resources.ellips;
      this.tBtn_Ellipse.IsSelected = false;
      this.tBtn_Ellipse.IsSelectedButton = true;
      this.tBtn_Ellipse.IsSingleSelectedButton = false;
      this.tBtn_Ellipse.Location = new System.Drawing.Point(32, 3);
      this.tBtn_Ellipse.Name = "tBtn_Ellipse";
      this.tBtn_Ellipse.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Ellipse.TabIndex = 1;
      // 
      // tBtn_Rect
      // 
      this.tBtn_Rect.Image = global::AeroScreenCapture.Properties.Resources.rect;
      this.tBtn_Rect.IsSelected = false;
      this.tBtn_Rect.IsSelectedButton = true;
      this.tBtn_Rect.IsSingleSelectedButton = false;
      this.tBtn_Rect.Location = new System.Drawing.Point(5, 3);
      this.tBtn_Rect.Name = "tBtn_Rect";
      this.tBtn_Rect.Size = new System.Drawing.Size(21, 21);
      this.tBtn_Rect.TabIndex = 0;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.toolButton1);
      this.panel2.Controls.Add(this.toolButton3);
      this.panel2.Controls.Add(this.toolButton2);
      this.panel2.Controls.Add(this.colorBox1);
      this.panel2.Location = new System.Drawing.Point(12, 124);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(250, 35);
      this.panel2.TabIndex = 2;
      // 
      // toolButton1
      // 
      this.toolButton1.Image = global::AeroScreenCapture.Properties.Resources.small;
      this.toolButton1.IsSelected = true;
      this.toolButton1.IsSelectedButton = true;
      this.toolButton1.IsSingleSelectedButton = true;
      this.toolButton1.Location = new System.Drawing.Point(5, 7);
      this.toolButton1.Name = "toolButton1";
      this.toolButton1.Size = new System.Drawing.Size(21, 21);
      this.toolButton1.TabIndex = 4;
      // 
      // toolButton3
      // 
      this.toolButton3.Image = global::AeroScreenCapture.Properties.Resources.large;
      this.toolButton3.IsSelected = false;
      this.toolButton3.IsSelectedButton = true;
      this.toolButton3.IsSingleSelectedButton = true;
      this.toolButton3.Location = new System.Drawing.Point(59, 7);
      this.toolButton3.Name = "toolButton3";
      this.toolButton3.Size = new System.Drawing.Size(21, 21);
      this.toolButton3.TabIndex = 3;
      // 
      // toolButton2
      // 
      this.toolButton2.Image = global::AeroScreenCapture.Properties.Resources.middle;
      this.toolButton2.IsSelected = false;
      this.toolButton2.IsSelectedButton = true;
      this.toolButton2.IsSingleSelectedButton = true;
      this.toolButton2.Location = new System.Drawing.Point(32, 7);
      this.toolButton2.Name = "toolButton2";
      this.toolButton2.Size = new System.Drawing.Size(21, 21);
      this.toolButton2.TabIndex = 2;
      // 
      // colorBox1
      // 
      this.colorBox1.Location = new System.Drawing.Point(87, 0);
      this.colorBox1.Name = "colorBox1";
      this.colorBox1.Size = new System.Drawing.Size(165, 35);
      this.colorBox1.TabIndex = 0;
      this.colorBox1.Text = "colorBox1";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 26);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(100, 20);
      this.textBox1.TabIndex = 3;
      this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      this.textBox1.Resize += new System.EventHandler(this.textBox1_Resize);
      this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // imageProcessBox1
      // 
      this.imageProcessBox1.BackColor = System.Drawing.Color.Black;
      this.imageProcessBox1.BaseImage = null;
      this.imageProcessBox1.CanReset = true;
      this.imageProcessBox1.Cursor = System.Windows.Forms.Cursors.Default;
      this.imageProcessBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.imageProcessBox1.DotColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
      this.imageProcessBox1.ForeColor = System.Drawing.Color.White;
      this.imageProcessBox1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
      this.imageProcessBox1.Location = new System.Drawing.Point(0, 0);
      this.imageProcessBox1.Name = "imageProcessBox1";
      this.imageProcessBox1.Size = new System.Drawing.Size(363, 268);
      this.imageProcessBox1.TabIndex = 0;
      this.imageProcessBox1.Text = "imageProcessBox1";
      this.imageProcessBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageProcessBox1_Paint);
      this.imageProcessBox1.DoubleClick += new System.EventHandler(this.imageProcessBox1_DoubleClick);
      this.imageProcessBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.imageProcessBox1_KeyDown);
      this.imageProcessBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageProcessBox1_MouseDown);
      this.imageProcessBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageProcessBox1_MouseMove);
      this.imageProcessBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageProcessBox1_MouseUp);
      // 
      // FrmCapture
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(363, 268);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.imageProcessBox1);
      this.Name = "FrmCapture";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "FrmCapture";
      this.Load += new System.EventHandler(this.FrmCapture_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCapture_KeyDown);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ToolButton tBtn_Ellipse;
        private ToolButton tBtn_Rect;
        private ToolButton tBtn_Arrow;
        private ToolButton tBtn_Brush;
        private ToolButton tBtn_Text;
        private ToolButton tBtn_Finish;
        private ToolButton tBtn_Close;
        private ToolButton tBtn_Save;
        private ToolButton tBtn_Cancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private ImageProcessBox imageProcessBox1;
        private System.Windows.Forms.Panel panel2;
        private ColorBox colorBox1;
        private ToolButton toolButton1;
        private ToolButton toolButton3;
        private ToolButton toolButton2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;

    }
}