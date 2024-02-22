using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pingServer
{
    public partial class Form1 : Form
    {

        private Timer timer;
        private string kullaniciAdi;
        private DateTime gonderiZamani;
        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string inputHost = textBox1.Text;
            testConn(inputHost);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Start();
            gonderiZamani = DateTime.Now;
        }

        private string userAccName()
        {
            string userName = Environment.UserName;
            return userName;
        }

        private void testConn(string txtHost)
        {
            string hedefHost = txtHost;
            if (hedefHost != " " && hedefHost != "" && hedefHost != null)
            {
                try
                {
                    Ping pingSender = new Ping();
                    PingReply pingReply = pingSender.Send(hedefHost);
                    DateTime basariZamani = DateTime.Now;
                    string basariDurum;

                    if (pingReply.Status == IPStatus.Success)
                    {
                        label2.Text = " ";
                        basariDurum = "Bağlantı Başarılı!";
                        listBox1.Items.Add($"Kullanıcı Adı: {kullaniciAdi} Giden Host: {hedefHost} Başarı Durumu: {basariDurum} Gönderi Zamanı: {gonderiZamani} Başarı Zamanı: {basariZamani}");
                    }
                    else if (pingReply.Status == IPStatus.TimedOut)
                    {
                        label2.Text = "Bağlantı Zaman Aşımı!";
                        basariDurum = "Bağlantı Zaman Aşımı!";
                        listBox2.Items.Add($"Kullanıcı Adı: {kullaniciAdi} Giden Host: {hedefHost} Başarı Durumu: {basariDurum} Gönderi Zamanı: {gonderiZamani} Başarı Zamanı: {basariZamani}");
                    }
                    else if (pingReply.Status != IPStatus.Success)
                    {
                        label2.Text = "Bağlantı Başarısız!";
                        basariDurum = "Bağlantı Başarısız!";
                        listBox2.Items.Add($"Kullanıcı Adı: {kullaniciAdi} Giden Host: {hedefHost} Başarı Durumu: {basariDurum} Gönderi Zamanı: {gonderiZamani} Başarı Zamanı: {basariZamani}");
                    }
                    else
                    {
                        label2.Text = "Tanımlanmamış Hata!";
                    }
                }
                catch
                {
                    label2.Text = "İnternet Bağlantısı Bulunamadı!";
                }
            }
            else
            {
                label2.Text = "Bir Host Adı Giriniz!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kullaniciAdi = userAccName();
        }
    }
}
