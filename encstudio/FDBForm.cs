using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace encstudio
{
    public partial class FDBForm : Form
    {
        public FDBForm()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.live.com";
                sc.EnableSsl = true;
                sc.Timeout = 50000;

                sc.Credentials = new NetworkCredential("koraynetwork@hotmail.com", İşlemler.Startup(İşlemler.a));

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("koraynetwork@hotmail.com", "KorayNET");

                mail.To.Add("koraynetwork@hotmail.com");
                mail.To.Add(textBox2.Text);

                mail.Subject = "Feedback for EncStudio";
                mail.IsBodyHtml = false;
                mail.Body = "Rate: " + comboBox1.Text.Length.ToString() + " Star" + Environment.NewLine +
                    textBox1.Text;

                sc.Send(mail);
                MessageBox.Show("Thank you for your feedback.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter your e-mail address!");
            }
        }
    }
}
