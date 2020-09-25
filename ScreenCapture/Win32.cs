using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ScreenCapture
{
    public static class Win32
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]//获取桌面的句柄
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]//在桌面找寻子窗体
        public static extern IntPtr ChildWindowFromPointEx(IntPtr pHwnd, LPPOINT pt, uint uFlgs);
        public const int CWP_SKIPDISABLED = 0x2;   //忽略不可用窗体
        public const int CWP_SKIPINVISIBL = 0x1;   //忽略隐藏的窗体
        public const int CWP_All = 0x0;            //一个都不忽略

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);
        public const uint WM_LBUTTONUP = 0x202;

        [DllImport("user32.dll")]//进行坐标转换 （再窗体内部进行查找）
        public static extern bool ScreenToClient(IntPtr hWnd, out LPPOINT lpPoint);

        public struct LPPOINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]//获得句柄对象的位置
        public static extern bool GetWindowRect(IntPtr hWnd, out LPRECT lpRect);

        public struct LPRECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out PCURSORINFO pci);

        public struct PCURSORINFO
        {
            public int cbSize;
            public int flag;
            public IntPtr hCursor;
            public LPPOINT ptScreenPos;
        }

        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest,
            int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);[DllImport("GDI32.dll")]
        public static extern int CreateCompatibleDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(int hObject);


        [DllImport("gdi32.dll")]
        public static extern int CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int hdc, int nIndex);

        [DllImport("GDI32.dll")]
        public static extern int SelectObject(int hdc, int hgdiobj);

        [DllImport("User32.dll")]
        public static extern int GetWindowDC(int hWnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(int hWnd, int hDC);
    }
}
