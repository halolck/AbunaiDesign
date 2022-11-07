using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace HalolckUI
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gHook;
        public Form1()
        {
           
            InitializeComponent();
            Random rdm = new Random();
            this.Text = GeneratePassword(rdm.Next(9, 15));


            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.hook();


        }
        bool TempToggle1 = false;
        bool TempToggle2 = false;
        private Point mousePoint;
        F_Overlay f_Overlay;

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.Visible = !this.Visible;
            }

            if (e.KeyCode == Keys.End)
            {
                Application.Exit();
            }

            if (e.KeyCode == Keys.Home)
            {
                f_Overlay.ShowEnable = !f_Overlay.ShowEnable;
            }
            Console.WriteLine(e.KeyCode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            f_Overlay = new F_Overlay();
            f_Overlay.Show();
            Init();
            TopMost = true;
        }



        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
                //または、つぎのようにする
                //this.Location = new Point(
                //    this.Location.X + e.X - mousePoint.X,
                //    this.Location.Y + e.Y - mousePoint.Y);
            }
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void Init()
        {
            AimBotBtn.Image = Properties.Resources.target_b;
            AimBotBtn.ForeColor = Color.RoyalBlue;
            ESPBtn.ForeColor = Color.White;
            SettingBtn.ForeColor = Color.White;
            LicenseBtn.ForeColor = Color.White;

            ABSValLbl.Text = ABSSlider.L_Value.ToString();
            FBSBalLbl.Text = FBSSlider.L_Value.ToString();
            FOVValLbl.Text = ((float)FOVSlider.L_Value / 10).ToString("F1");


            if (ABToggle.Checked)
            {
                ABELbl.ForeColor = Color.White;
                ABKLbl.ForeColor = Color.White;

                ABSLbl.ForeColor = Color.White;
                ABSValLbl.ForeColor = Color.White;

                ABSSlider.L_SliderColor = Color.RoyalBlue;
            }
            else
            {
                ABELbl.ForeColor = Color.Gray;
                ABKLbl.ForeColor = Color.Gray;

                ABSLbl.ForeColor = Color.Gray;
                ABSValLbl.ForeColor = Color.Gray;

                ABSSlider.L_SliderColor = Color.DarkBlue;
            }
            ABSSlider.L_Enable = ABToggle.Checked;


            if (FBToggle.Checked)
            {
                FBELbl.ForeColor = Color.White;
                FBKLbl.ForeColor = Color.White;

                FBSLbl.ForeColor = Color.White;
                FBSBalLbl.ForeColor = Color.White;

                FBSSlider.L_SliderColor = Color.RoyalBlue;
            }
            else
            {
                FBELbl.ForeColor = Color.Gray;
                FBKLbl.ForeColor = Color.Gray;

                FBSLbl.ForeColor = Color.Gray;
                FBSBalLbl.ForeColor = Color.Gray;

                FBSSlider.L_SliderColor = Color.DarkBlue;
            }
            FBSSlider.L_Enable = FBToggle.Checked;

            if (SAToggle.Checked)
            {
                SAELbl.ForeColor = Color.White;
                SAKLbl.ForeColor = Color.White;
            }
            else
            {
                SAELbl.ForeColor = Color.Gray;
                SAKLbl.ForeColor = Color.Gray;
            }
            label11.Text = slider3.L_Value.ToString() + "m";
            if (toggleCheckbox3.Checked)
            {
                label1.ForeColor = Color.White;
            }
            else
            {
                label1.ForeColor = Color.Gray;
            }
            if (toggleCheckbox1.Checked)
            {
                label3.ForeColor = Color.White;
            }
            else
            {
                label3.ForeColor = Color.Gray;
            }
            if (toggleCheckbox2.Checked)
            {
                label13.ForeColor = Color.White;
            }
            else
            {
                label13.ForeColor = Color.Gray;
            }

            f_Overlay.ShowFov = toggleCheckbox2.Checked;


            ABPanel.Visible = true;
            ESPPanel.Visible = false;
            SettingPanel.Visible = false;
            LicensePanel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AimBotBtn.ForeColor = Color.White;
            ESPBtn.ForeColor = Color.White;
            SettingBtn.ForeColor = Color.White;
            LicenseBtn.ForeColor = Color.White;
            ((Button)sender).ForeColor = Color.RoyalBlue;
            ButtonCheck(sender);
        }
        private void ButtonCheck(object sender)
        {
            AimBotBtn.Image = ((Button)sender).Text == "AimBot" ? Properties.Resources.target_b: Properties.Resources.target_w;
            ESPBtn.Image = ((Button)sender).Text == "ESP" ? Properties.Resources.eye_b : Properties.Resources.eye_w;
            SettingBtn.Image = ((Button)sender).Text == "Setting" ? Properties.Resources.cog_b : Properties.Resources.cog_w;
            LicenseBtn.Image = ((Button)sender).Text == "License" ? Properties.Resources.account_b : Properties.Resources.account_w;

            //panelchange
            switch(((Button)sender).Text)
            {
                case "AimBot":
                    SelectAimbot();
                    break;
                case "ESP":
                    SelectESP();
                    break;
                case "Setting":
                    SelectSetting();
                    break;
                case "License":
                    SelectLicense();
                    break;
            }
        }

        private void SelectAimbot()
        {
            AimBotBtn.Image = Properties.Resources.target_b;
            ESPBtn.Image = Properties.Resources.eye_w;
            SettingBtn.Image = Properties.Resources.cog_w;
            LicenseBtn.Image = Properties.Resources.account_w;

            ABPanel.Visible = true;
            ESPPanel.Visible = false;
            SettingPanel.Visible = false;
            LicensePanel.Visible = false;
        }

        private void SelectESP()
        {
            AimBotBtn.Image = Properties.Resources.target_w;
            ESPBtn.Image = Properties.Resources.eye_b;
            SettingBtn.Image = Properties.Resources.cog_w;
            LicenseBtn.Image = Properties.Resources.account_w;
            ABPanel.Visible = false;
            ESPPanel.Visible = true;
            SettingPanel.Visible = false;
            LicensePanel.Visible = false;
        }

        private void SelectSetting()
        {
            AimBotBtn.Image = Properties.Resources.target_w;
            ESPBtn.Image = Properties.Resources.eye_w;
            SettingBtn.Image = Properties.Resources.cog_b;
            LicenseBtn.Image = Properties.Resources.account_w;
            ABPanel.Visible = false;
            ESPPanel.Visible = false;
            SettingPanel.Visible = true;
            LicensePanel.Visible = false;
        }

        private void SelectLicense()
        {
            AimBotBtn.Image = Properties.Resources.target_w;
            ESPBtn.Image = Properties.Resources.eye_w;
            SettingBtn.Image = Properties.Resources.cog_w;
            LicenseBtn.Image = Properties.Resources.account_b;
            ABPanel.Visible = false;
            ESPPanel.Visible = false;
            SettingPanel.Visible = false;
            LicensePanel.Visible = true;
        }

        private void slider1_LValueChanged(object sender, CustomSlider.LEventArgs e)
        {
            ABSValLbl.Text = ABSSlider.L_Value.ToString();
        }

        private void slider2_LValueChanged(object sender, CustomSlider.LEventArgs e)
        {
            FBSBalLbl.Text　= FBSSlider.L_Value.ToString();
        }

        private void slider3_LValueChanged(object sender, CustomSlider.LEventArgs e)
        {
            FOVValLbl.Text = ((float)FOVSlider.L_Value/10+0.9).ToString("F1");
            f_Overlay.Fov = FOVSlider.L_Value;
        }

        private void toggleCheckbox6_CheckedChanged(object sender, EventArgs e)
        {
            if (ABToggle.Checked)
            {
                ABELbl.ForeColor = Color.White;
                ABKLbl.ForeColor = Color.White;

                ABSLbl.ForeColor = Color.White;
                ABSValLbl.ForeColor = Color.White;

                ABSSlider.L_SliderColor = Color.RoyalBlue;

            }
            else
            {
                ABELbl.ForeColor = Color.Gray;
                ABKLbl.ForeColor = Color.Gray;

                ABSLbl.ForeColor = Color.Gray;
                ABSValLbl.ForeColor = Color.Gray;

                ABSSlider.L_SliderColor = Color.DarkBlue;
            }
            f_Overlay.AimbotEnable = ABToggle.Checked;
            ABSSlider.L_Enable = ABToggle.Checked;
        }

        private void toggleCheckbox2_CheckedChanged(object sender, EventArgs e)
        {
            if (FBToggle.Checked)
            {

                FBELbl.ForeColor = Color.White;
                FBKLbl.ForeColor = Color.White;

                FBSLbl.ForeColor = Color.White;
                FBSBalLbl.ForeColor = Color.White;

                FBSSlider.L_SliderColor = Color.RoyalBlue;
            }
            else
            {
                FBELbl.ForeColor = Color.Gray;
                FBKLbl.ForeColor = Color.Gray;

                FBSLbl.ForeColor = Color.Gray;
                FBSBalLbl.ForeColor = Color.Gray;

                FBSSlider.L_SliderColor = Color.DarkBlue;
            }
            FBSSlider.L_Enable = FBToggle.Checked;
        }

        private void toggleCheckbox3_CheckedChanged(object sender, EventArgs e)
        {
            if (SAToggle.Checked)
            {
                SAKLbl.ForeColor = Color.White;
                SAELbl.ForeColor = Color.White;

            }
            else
            {
                SAKLbl.ForeColor = Color.Gray;
                SAELbl.ForeColor = Color.Gray;


            }
        }

        private static readonly string passwordChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

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

        private void toggleCheckbox4_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleCheckbox5.Checked)
                label15.ForeColor = Color.White;
            else
                label15.ForeColor = Color.Gray;
        }

        private void ellipseButoon3_Click(object sender, EventArgs e)
        {
            ellipseButoon4.Text = "...";
            ellipseButoon4.Update();
            Console.WriteLine("...");
        }

        private void toggleCheckbox2_Click(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
        }

        private void slider3_LValueChanged_1(object sender, CustomSlider.LEventArgs e)
        {
            label11.Text = slider3.L_Value.ToString()+"m";
        }

        private void toggleCheckbox3_CheckedChanged_1(object sender, EventArgs e)
        {
            if(toggleCheckbox3.Checked)
            {
                label1.ForeColor = Color.White;
            }
            else
            {
                label1.ForeColor = Color.Gray;
            }
        }

        private void toggleCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleCheckbox1.Checked)
            {
                label3.ForeColor = Color.White;
            }
            else
            {
                label3.ForeColor = Color.Gray;
            }
        }

        private void toggleCheckbox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (toggleCheckbox2.Checked)
            {
                label13.ForeColor = Color.White;
            }
            else
            {
                label13.ForeColor = Color.Gray;
            }
            f_Overlay.ShowFov = toggleCheckbox2.Checked;
        }
    }
}
