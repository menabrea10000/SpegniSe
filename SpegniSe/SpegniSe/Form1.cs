using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SpegniSe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Spegni()
        {
            Console.WriteLine("spento");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Ferma")
            {
                timer1.Enabled = false;
                button1.Text = "SpegniSe";            
            }
            else
            { 
                timer1.Enabled = true;
                button1.Text = "Ferma";
                switch (comboBox1.Text)
                {
                    case "Tempo trascorso":
                        label1.Text = textBox1.Text;
                        Int32 x;
                        if (Int32.TryParse(textBox1.Text.Trim(), out x))
                        {
                            label1.Text = Convert.ToString(x);
                        }
                        else
                        {
                            timer1.Enabled = false;
                            MessageBox.Show("Non Numerico");
                        }
                        break;
                    default:
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
                default:
                    textBox1.Enabled = false;
                    textBox1.Visible = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Int32 x;
            x = Convert.ToInt32(label1.Text);
            x = x - 1;
            label1.Text = x.ToString();
            if (x == 0)
            {
                Spegni();
            }
        }
    }
}
