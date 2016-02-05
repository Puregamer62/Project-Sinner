using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JRPC_Client;
using XDevkit;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Deidara_RTE_Tool
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        IXboxConsole Jtag;
        private object form1;
        private byte[] bytes;

        public object XShowMessageUI { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private bool CheckVersion() //Update Check
        {
            string version = Application.ProductVersion;
            string[] info = new WebClient().DownloadString("http://pastebin.com/raw/bN0RW8Bq").Split('\n');
            if (!info[0].Contains(version))
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!CheckVersion())
            {
                var frm1 = new Form1();
            }
            else
            {
                MessageBox.Show("An Update Is Available Please Download It", "Application Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("http://pastebin.com/raw/bN0RW8Bq");
                Environment.Exit(0);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //Opening xNotify and Main Information Grab
        {
            if (Jtag.Connect(out Jtag))
            {
                Jtag.XNotify("Welcome To Project Sinner The Open Source AIO Tool");
                Jtag.XNotify("For More Tools Be Sure To Visit Se7ensins.com");
                MessageBox.Show("Connected");
                textBox1.Text = "" + ((XDevkit.IXboxConsole)Jtag).GetCPUKey();
                textBox2.Text = "" + ((XDevkit.IXboxConsole)Jtag).GetKernalVersion();
                textBox3.Text = "" + ((XDevkit.IXboxConsole)Jtag).XboxIP();
                textBox4.Text = "" + Jtag.GetTemperature(JRPC.TemperatureType.CPU);
                label2.Text = "Connected";
            }
            else
            {
                MessageBox.Show("Error: Could not connect. Please check your connection!");
                label2.Text = "Connection Failed";
            }
        }

        private void button1_Click(object sender, EventArgs e) //Screenshot
        {
            string a = Application.StartupPath + "\\xboxScreenshot.bmp";
                Jtag.ScreenShot(a);
            System.Diagnostics.Process.Start(Application.StartupPath + "\\xboxScreenshot.bmp");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Jtag.XNotify("" + textBox5.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Jtag.SetMemory(0x821fc04c, new byte[] { 0x38, 0xc0, 0xff, 0xff });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Jtag.WriteByte(0x821f5b7f, 1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Jtag.SetMemory(0x821fc04c, new byte[] { 0x7f, 0xa6, 0xeb, 120 });
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ((XDevkit.IXboxConsole)Jtag).SetMemory(0x82255e1c, new byte[] { 0x2b, 11, 0, 1 });
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Jtag.WriteByte(0x821f5b7f, 0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Jtag.SetMemory(0x82259bc8, new byte[] { 0x48, 70, 0x13, 0x41 });
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] buffer1 = new byte[4];
            buffer1[0] = 0x60;
            Jtag.SetMemory(0x82259bc8, buffer1);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e) //MSP Spoof
        {
            // Offset: 0x8168A690
            // Set the MSP Amount in BIN/HEX
            byte[] bytes0 = {
    0x38, 0x80, 0x00, 0x05, 0x80, 0x63,
    0x00, 0x1C, 0x90, 0x83, 0x00, 0x04,
    0x38, 0x80, 0x05, 0x39, 0x90, 0x83,
    0x00, 0x08, 0x38, 0x60, 0x00, 0x00,
    0x4E, 0x80, 0x00, 0x20 };

            Jtag.SetMemory(0x8168A690, bytes);
            //Offset: 0x818ED084
            byte[] bytes1 = { 0x48, 0x00, 0x00, 0xC8 };
            //Offset: 0x9015C15C
            byte[] bytes2 = { 0x39, 0x60, 0x00, 0x00 };
            //Offset: 0x9015C108
            byte[] bytes3 = { 0x60, 0x00, 0x00, 0x00 };


            // The Spoof
            Jtag.SetMemory(0x8168A690, bytes0);
            Jtag.SetMemory(0x818ED084, bytes1);
            Jtag.SetMemory(0x9015C15C, bytes2);
            Jtag.SetMemory(0x9015C108, bytes3);
        }

        private void button14_Click(object sender, EventArgs e)
        {
          
        }

        private void button15_Click(object sender, EventArgs e)
        {
           
        }

        private void button16_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[] {
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
             };
            for (uint i = 0; i < 3; i++)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
