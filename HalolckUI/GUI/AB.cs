using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalolckUI.GUI
{
    public partial class AB : UserControl
    {
        public AB()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            label4.Text = slider1.L_Value.ToString();
            label7.Text = slider2.L_Value.ToString();
            label9.Text = ((float)slider3.L_Value / 10 + 0.0).ToString("F1");


            if (toggleCheckbox1.Checked)
            {
                label2.ForeColor = Color.White;
                label11.ForeColor = Color.White;

                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;

                slider1.L_SliderColor = Color.RoyalBlue;
            }
            else
            {
                label2.ForeColor = Color.Gray;
                label11.ForeColor = Color.Gray;

                label3.ForeColor = Color.Gray;
                label4.ForeColor = Color.Gray;

                slider1.L_SliderColor = Color.DarkBlue;
            }
            slider1.L_Enable = toggleCheckbox1.Checked;


            if (toggleCheckbox2.Checked)
            {
                label5.ForeColor = Color.White;
                label12.ForeColor = Color.White;

                label8.ForeColor = Color.White;
                label7.ForeColor = Color.White;

                slider2.L_SliderColor = Color.RoyalBlue;
            }
            else
            {
                label5.ForeColor = Color.Gray;
                label12.ForeColor = Color.Gray;

                label8.ForeColor = Color.Gray;
                label7.ForeColor = Color.Gray;

                slider2.L_SliderColor = Color.DarkBlue;
            }
            slider2.L_Enable = toggleCheckbox2.Checked;

            if (toggleCheckbox3.Checked)
            {
                label6.ForeColor = Color.White;
                label13.ForeColor = Color.White;
            }
            else
            {
                label6.ForeColor = Color.Gray;
                label13.ForeColor = Color.Gray;
            }
        }

        private void slider3_LValueChanged(object sender, CustomSlider.LEventArgs e)
        {
            label9.Text = ((float)slider3.L_Value/10).ToString("F1");
        }

        private void slider2_LValueChanged(object sender, CustomSlider.LEventArgs e)
        {
            label7.Text = slider2.L_Value.ToString();
        }

        private void slider1_LValueChanged(object sender, CustomSlider.LEventArgs e)
        {
            label4.Text = slider1.L_Value.ToString();
        }

        private void toggleCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleCheckbox1.Checked)
            {
                label2.ForeColor = Color.White;
                label11.ForeColor = Color.White;

                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;

                slider1.L_SliderColor = Color.RoyalBlue;
            }
            else
            {
                label2.ForeColor = Color.Gray;
                label11.ForeColor = Color.Gray;

                label3.ForeColor = Color.Gray;
                label4.ForeColor = Color.Gray;

                slider1.L_SliderColor = Color.DarkBlue;
            }
            slider1.L_Enable = toggleCheckbox1.Checked;

        }

        private void toggleCheckbox2_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleCheckbox2.Checked)
            {
                label5.ForeColor = Color.White;
                label12.ForeColor = Color.White;

                label8.ForeColor = Color.White;
                label7.ForeColor = Color.White;

                slider2.L_SliderColor = Color.RoyalBlue;
            }
            else
            {
                label5.ForeColor = Color.Gray;
                label12.ForeColor = Color.Gray;

                label8.ForeColor = Color.Gray;
                label7.ForeColor = Color.Gray;

                slider2.L_SliderColor = Color.DarkBlue;
            }
            slider2.L_Enable = toggleCheckbox2.Checked;

        }

        private void toggleCheckbox3_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleCheckbox3.Checked)
            {
                label6.ForeColor = Color.White;
                label13.ForeColor = Color.White;
            }
            else
            {
                label6.ForeColor = Color.Gray;
                label13.ForeColor = Color.Gray;
            }
        }

        private void ellipseButoon3_Click(object sender, EventArgs e)
        {

        }
    }
}
