using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace encstudio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            YumuşakKenarlar();
        }

        public void YumuşakKenarlar()
        {
            GraphicsPath graphicpath = new GraphicsPath();

            graphicpath.StartFigure();

            graphicpath.AddArc(0, 0, 25, 25, 180, 90);

            graphicpath.AddLine(25, 0, this.Width - 25, 0);

            graphicpath.AddArc(this.Width - 25, 0, 25, 25, 270, 90);

            graphicpath.AddLine(this.Width, 25, this.Width, this.Height - 25);

            graphicpath.AddArc(this.Width , this.Height , 25, 25, 0, 90);

            graphicpath.AddLine(this.Width - 25, this.Height, 25, this.Height);

            graphicpath.AddArc(0, this.Height , 25, 25, 90, 90);

            graphicpath.CloseFigure();

            this.Region = new Region(graphicpath);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void ÜstPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("Windows did not allow the program to be shut down for security reasons. Please try again. If you still get the same error, please contact your administrator.","Encryption Studio",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Close");
            t.SetToolTip(button2, "Minimize");
            t.SetToolTip(button8, "Send Feedback");
            textBox1.Text = Properties.Settings.Default.Default_Enc_Pass;
            textBox4.Text = Properties.Settings.Default.Default_Dec_Pass;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                timer1.Start();
                Properties.Settings.Default.Default_Enc_Pass = textBox5.Text;
                Properties.Settings.Default.Save();
                label8.ForeColor = Color.Green;
                label8.Text = "Encrypt Password Save Completed!";
            }
            catch (Exception)
            {
                label8.ForeColor = Color.Red;
                label8.Text = "ERROR";
            }
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 2)
            {
                label8.Text = "";
                label9.Text = "";
                i = 0;
                timer1.Stop();
                timer1.Enabled = false;
            }
            i++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                timer1.Start();
                Properties.Settings.Default.Default_Dec_Pass = textBox6.Text;
                Properties.Settings.Default.Save();
                label9.ForeColor = Color.Green;
                label9.Text = "Decrypt Password Save Completed!";
            }
            catch (Exception)
            {
                label9.ForeColor = Color.Red;
                label9.Text = "ERROR";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "ENC File|*.enc";
                if (s.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = İşlemler.Encrypt(textBox2.Text, textBox1.Text);
                    System.IO.File.WriteAllText(s.FileName, textBox2.Text);
                }
            }
            else
            {
                textBox2.Text = İşlemler.Encrypt(textBox2.Text, textBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = İşlemler.Decrypt(textBox3.Text, textBox4.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "ENC File|*.enc";
            if (o.ShowDialog() == DialogResult.OK)
            {
                string a = System.IO.File.ReadAllText(o.FileName);
                textBox3.Text = a;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FDBForm f = new FDBForm();
            f.ShowDialog();
        }
    }
}
