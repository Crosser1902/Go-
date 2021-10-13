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
            Point p = new Point(200, 0);
            this.BackColor = getColor(p);
            textBox1.Text = System.Drawing.ColorTranslator.ToHtml(getColor(p));
        }
    }
}
