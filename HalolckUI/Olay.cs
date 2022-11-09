using HalolckUI.memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
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
        string WINDOW_NAME = "Taskmgr";
        private const string processName = "ac_client";//バラバラになっていることで別モニターのobsや画面共有にwallhackを、メイン画面はクリーンなままを可能としている
        private Process process;
        System.Diagnostics.Process[] ps;
        IntPtr intPtr;
        #region CProperet
        public bool ShowFov = true;
        public bool ESPEnable = true;
        public int Fov = 21;
        public bool ShowEnable = true;
        public int DistanceMax = 1000;

        //aimbot
        public bool AimbotEnable = false;
        public bool FlickEnable = false;
        public bool SilentEnable = false;
        public bool TriggerEnable = false;
        public int AimbotSmooth = 1;
        public int FlickSmooth = 1;

        //espchbbool
        public bool ESPBOXEnable = true;
        public bool LineEnable = false;
        public bool HealthEnable = false;
        public bool NameEnable = false;
        public bool DistanceEnable = false;
        //color(pen)
        public Pen FovPen = new Pen(Color.Green);
        public Pen ESPVisPen = new Pen(Color.Red);
        public Pen ESPINVisPen = new Pen(Color.White);
        public Pen StringPen = new Pen(Color.White);

        #endregion
        private void F_Overlay_Load(object sender, EventArgs e)
        {
            try
            {
                ps = System.Diagnostics.Process.GetProcessesByName(WINDOW_NAME);
                if (ps.Length == 0)
                {
                    MessageBox.Show($"指定プロセス\"{WINDOW_NAME}\"が存在しません。\r\n最初に\"{WINDOW_NAME}\"を起動してください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;

                }
                else if (ps.Length != 1)
                {
                    MessageBox.Show($"指定プロセス\"{WINDOW_NAME}\"が複数存在します。\r\n1つ({ps[0].MainWindowHandle.ToString()})にしかハックは適応されません。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                intPtr = ps[0].MainWindowHandle;

            }
            catch
            {
                MessageBox.Show($"指定プロセス\"{WINDOW_NAME}\"へのアタッチ時にエラーが発生しました。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }
            AttachGame();
            timer2.Start();
            timer1.Start();
            timer3.Start();
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
                    g.DrawEllipse(FovPen, new RectangleF(windowsize.Width / 2 - Fov * 10 / 2, windowsize.Height / 2 - Fov * 10 / 2, Fov * 2 * 10 / 2, Fov * 2 * 10 / 2));
                }

            }
            

            //リソースを解放する
            g.Dispose();
            //PictureBox1に表示する
            pictureBox1.Image = canvas;
        }
        private List<Player> players = new List<Player>();
        private int numPlayers;
        private void PlayerGet(Graphics g)
        {

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

       

        private void AttachGame()
        {
            int count = 0;
            
                //check if game is running
            if (Memory.GetProcessesByName(processName, out process))
            {


                    //try to attach to game process
                try
                {
                        //success  
                    IntPtr handle = Memory.OpenProcess(process.Id);

                }
                catch (Exception ex)
                {
                    //fail
                    MessageBox.Show("Attach failed: " + ex.Message);
                    Application.Exit();
                    return;
                }
            }
            else
            {
                MessageBox.Show(processName + "が見つかりませんでした。先に起動してください。");
                Application.Exit();
                return;
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
        private static double dist2d(double x1, double y1, double x2, double y2)
        {
            var distance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));

            // 計算した距離を返す
            return distance;
        }

        private static double dist3d(double x1, double y1, double z1,double x2, double y2, double z2)
    	{
    		double l;
    		double dx, dy, dz;
    
    		// x座標の差を計算してdxに代入
    		dx = x2 - x1;
    		// y座標の差を計算してdyに代入
    		dy = y2 - y1;
    		// y座標の差を計算してdyに代入
    		dz = z2 - z1;
    
    		// ２点間の距離を計算してlに代入
    		l = Math.Sqrt(dx* dx + dy* dy + dz* dz );
    
    		// 計算した距離を返す
    		return l;
    	}
}
}
