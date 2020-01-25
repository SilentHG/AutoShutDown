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

namespace AutoShutDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int hour, minute, second = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Seconds");
            comboBox1.Items.Add("Minutes");
            comboBox1.Items.Add("Hours");
            comboBox1.SelectedItem = "Hours";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double time = Convert.ToInt32(textBox1.Text);
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                if (comboBox1.SelectedItem == "Seconds")
                {
                    process.StandardInput.WriteLine("shutdown -s -t " + time + "");
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                    process.WaitForExit();
                    // MessageBox.Show(process.StandardOutput.ReadToEnd());
                }
                else if (comboBox1.SelectedItem == "Minutes")
                {
                    process.StandardInput.WriteLine("shutdown -s -t " + time * 60 + "");
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                    process.WaitForExit();
                    // MessageBox.Show(process.StandardOutput.ReadToEnd());
                }
                else if (comboBox1.SelectedItem == "Hours")
                {
                    process.StandardInput.WriteLine("shutdown -s -t " + time * 3600 + "");
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                    process.WaitForExit();
                    // MessageBox.Show(process.StandardOutput.ReadToEnd());
                }
                if (comboBox1.SelectedItem == "Seconds")
                {
                    second = Convert.ToInt32(textBox1.Text);
                }
                else if (comboBox1.SelectedItem == "Minutes")
                {
                    minute = Convert.ToInt32(textBox1.Text);

                }
                else if (comboBox1.SelectedItem == "Hours")
                {
                    hour = Convert.ToInt32(textBox1.Text);
                }
                while (second > 59)
                {
                    minute++;
                    second -= 60;

                }
                while (minute > 59)
                {
                    hour++;
                    minute -= 60;
                }
            }
            catch
            {
                MessageBox.Show("Wrong Input");
            }
           
            timer1.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine("shutdown -a");
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            //MessageBox.Show(process.StandardOutput.ReadToEnd());
            hour = minute = second = 0;
            timer1.Stop();
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hour == 0 && minute == 0)
            {
                Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics g = Graphics.FromImage(bm);
                g.CopyFromScreen(0, 0, 0, 0, bm.Size);

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                bm.Save(path + "\\AutoShutDown.jpg");
                bm.Dispose();
                timer1.Stop();
                label4.Text = "Alpha Bravo Going Down !!!";

            }
            else
            {
                second--;
                if(second <= 0 && minute>0)
                {
                    minute--;
                    second = 59;
                    
                }
                else if (minute == 0 && hour > 0)
                {
                    hour--;
                    minute = 59;
                    second = 59;
                }
                label4.Text = Convert.ToString(hour + " : " + minute + " : " + second);
            }
            
        }
    }
}
