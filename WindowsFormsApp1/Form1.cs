using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr Hwnd); //其在MSDN中原型为HDC GetDC(HWND hWnd),HDC和HWND都是驱动器句柄（长指针），在C#中只能用IntPtr代替了
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int GetPixel(IntPtr hdc, Point p);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //模拟鼠标滚轮滚动操作，必须配合dwData参数
        const int MOUSEEVENTF_WHEEL = 0x0800;

        class Board
        {
            public int lefttopx;
            public int lefttopy;
            public int boarderlen;
            public int [,] a = new int [19,19];
            public Point[,] b = new Point[19, 19];
            public Board(int x, int y, int length)
            {
                lefttopx = x;
                lefttopy = y;
                boarderlen = length;
                for(int i=0;i<19;i++)
                {
                    for(int j=0;j<19;j++)
                    {
                        a[i, j] = 0;
                        Point p = new Point();
                        p.X = x;
                        p.Y = y;
                        b[i, j] = p;
                        y = y + boarderlen;
                    }
                    y = lefttopy;
                    x = x + boarderlen;
                }
            }

            public int checkPoint(int xname, int yname)
            {
                Point p = b[xname, yname];
                p.X = p.X + Convert.ToInt32(boarderlen * 0.3);
                p.Y = p.Y - Convert.ToInt32(boarderlen * 0.15);
                Color tempColor = getColor(p);
                int blackThreshold = 55;
                int whiteThreshold = 200;
                int result = 0;
                if (tempColor.R > blackThreshold && tempColor.G > blackThreshold && tempColor.B > blackThreshold)
                {
                    result = 2;
                }
                if (tempColor.R>whiteThreshold && tempColor.G>whiteThreshold && tempColor.B>whiteThreshold)
                {
                    result = 2;
                }
                return result;
            }

            public void scanBoard()
            {

                return;
            }
        }

        public void clickMouse(int x, int y)
        {
            mouse_event(MOUSEEVENTF_MOVE, x * 65536 / 1024, y * 65536 / 1024, 0, 0);
        }

        public static Color getColor(Point p)
        {
            // Point p = new Point(MousePosition.X, MousePosition.Y);//取置顶点坐标
            IntPtr hdc = GetDC(new IntPtr(0));//取到设备场景(0就是全屏的设备场景)
            int c = GetPixel(hdc, p);//取指定点颜色
            int r = (c & 0xFF);//转换R
            int g = (c & 0xFF00) / 256;//转换G
            int b = (c & 0xFF0000) / 65536;//转换B
                                           // pictureBox1.BackColor = Color.FromArgb(r, g, b);
            return Color.FromArgb(r, g, b);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point p = new Point( 500,  500);
            this.BackColor = getColor(p);
            textBox1.Text = getColor(p).ToString();
            
            clickMouse(100, 100);
        }
    }
}
