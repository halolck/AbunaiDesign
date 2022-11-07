using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Forms;

namespace HalolckUI
{
    public partial class F_Overlay : Form
    {
        public F_Overlay()
        {
            InitializeComponent();
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
        Bitmap canvas;
        Graphics g;
        Size windowsize;
        string WINDOW_NAME = "jcpicker";
        System.Diagnostics.Process[] ps;
        IntPtr intPtr;
        #region CProperet
        public bool AimbotEnable = false;
        public bool ShowFov = true;
        public int Fov = 1;
        public bool ShowEnable = true;
        #endregion
        private void F_Overlay_Load(object sender, EventArgs e)
        {
            ps = System.Diagnostics.Process.GetProcessesByName(WINDOW_NAME);
            if (ps.Length == 0)
            {
                MessageBox.Show($"指定プロセス\"{WINDOW_NAME}\"が存在しません。\r\n最初に\"{WINDOW_NAME}\"を起動してください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }   
            else if (ps.Length > 1)
            {
                MessageBox.Show($"指定プロセス\"{WINDOW_NAME}\"が複数存在します。\r\n最初に見つけた1つ({ps[0].MainWindowHandle.ToString()})にしかハックは適応されません。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            intPtr = ps[0].MainWindowHandle;

            windowsize = new Size(this.Width, this.Height);
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //ディスプレイの幅
            int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Size = new Size(w,h);
            Random rnd = new Random();
            this.Text = GeneratePassword(rnd.Next(12, 19));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(canvas);
            g.Clear(SystemColors.Control);
            if (ShowEnable)
            {
                if (ShowFov)
                {
                    //(10, 20)-(100, 200)に、幅1の黒い線を引く
                    g.DrawEllipse(Pens.Green, new RectangleF(windowsize.Width / 2 - Fov * 10 / 2, windowsize.Height / 2 - Fov * 10 / 2, Fov * 2 * 10 / 2, Fov * 2 * 10 / 2));
                }

            }
            

            //リソースを解放する
            g.Dispose();
            //PictureBox1に表示する
            pictureBox1.Image = canvas;
        }



        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        int off = 8;
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                
                RECT rect;
                bool flag = GetWindowRect(intPtr, out rect);
                int width = rect.right - rect.left;
                int height = rect.bottom - rect.top;
                
                windowsize = new Size(width- off*2, height - off);
                if (this.Size != windowsize)
                {
                    canvas = new Bitmap(windowsize.Width, windowsize.Height);
                    this.Size = windowsize;
                }
                if (this.Left != rect.left + off || this.Top != rect.top)
                {
                    this.Left = rect.left + off;
                    this.Top = rect.top;
                }
                
            }
            catch
            {
                Application.Exit();
            }
            

        }

        private static readonly string passwordChars = "wxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuv";

        /// <summary>
        /// ランダムな文字列を生成する
        /// </summary>
        /// <param name="length">生成する文字列の長さ</param>
        /// <returns>生成された文字列</returns>
        public string GeneratePassword(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            Random r = new Random();

            for (int i = 0; i < length; i++)
            {
                //文字の位置をランダムに選択
                int pos = r.Next(passwordChars.Length);
                //選択された位置の文字を取得
                char c = passwordChars[pos];
                //パスワードに追加
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
