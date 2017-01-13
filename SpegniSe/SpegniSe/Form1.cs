using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SpegniSe
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);

        public Int32 x;
        public Point Punto;

        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Spegni()
        {
            Console.WriteLine("spento");
            /*var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi); */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Ferma")
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                button1.Text = "SpegniSe";            
            }
            else
            {                 
                switch (comboBox1.Text)
                {
                    case "Tempo trascorso":
                        label1.Text = textBox1.Text;
                        if (Int32.TryParse(textBox1.Text.Trim(), out x))
                        {
                            //label1.Text = Convert.ToString(x);
                            timer1.Enabled = true;
                            button1.Text = "Ferma";
                        }
                        else
                        {
                            timer1.Enabled = false;
                            MessageBox.Show("Non Numerico");
                            button1.Text = "SpegniSe";
                            timer1.Enabled = false;
                        }
                        break;
                    case "Cambio Colore":
                        MessageBox.Show("Posiziona il mouse");
                        label2.BackColor = GetColorAt(MousePosition.X, MousePosition.Y);
                        Punto= new Point(MousePosition.X, MousePosition.Y);
                        timer2.Enabled = true;
                        button1.Text = "Ferma";
                        break;
                    case "Data":                       
                        timer3.Enabled = true;
                        button1.Text = "Ferma";
                        break;
                    default:
                        button1.Text = "Ferma";
                        break;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Tempo trascorso":
                    textBox1.Enabled = true;
                    textBox1.Visible = true;
                    break;
                case "Data":
                    textBox1.Enabled = true;
                    textBox1.Visible = true;
                    textBox1.Text = DateTime.Now.ToString();
                    break;
                default:
                    textBox1.Enabled = false;
                    textBox1.Visible = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //x = Convert.ToInt32(label1.Text);
            x = x - 1;
            label1.Text = x.ToString();
            if (x == 0)
            {
                Spegni();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (label2.BackColor != GetColorAt(Punto.X, Punto.Y))
            {
                Spegni();
            }
            label2.BackColor = GetColorAt(Punto.X,Punto.Y);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            DateTime voteTime = Convert.ToDateTime(textBox1.Text);
            TimeSpan TimeRemaining = voteTime - DateTime.Now;
            label1.Text = TimeRemaining.Hours + " : " + TimeRemaining.Minutes + " : " + TimeRemaining.Seconds;
            if (TimeRemaining.TotalSeconds <= 0)
            {
                Spegni();
            }
        }
    }
}
