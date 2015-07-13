using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitDisplay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private string CleanString(string input)
        {
            StringBuilder sb = new StringBuilder();
            string list = "0123456789abcdef";
            input = input.ToLower().Replace("0x","");
            foreach (char c in input)
                foreach (char c2 in list)
                    if (c == c2)
                        sb.Append(c);
            return sb.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DrawBits();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DrawBits();
        }

        private void DrawBits()
        {
            try
            {
                string input = CleanString(toolStripTextBox1.Text);
                toolStripTextBox1.Text = input;
                byte[] buff = StringToByteArray(input);
                Graphics g = pb1.CreateGraphics();
                g.Clear(Color.White);
                FontFamily fontFamily = new FontFamily("Courier New");
                Font font = new Font(fontFamily, 10, FontStyle.Regular, GraphicsUnit.Pixel);
                Brush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));
                if (!toolStripButton2.Checked)
                {
                    for (int i = 0; i < 8; i++)
                        g.DrawString((7 - i).ToString(), font, solidBrush, new PointF(10, i * 10 + 8));
                    for (int i = 0; i < buff.Length; i++)
                        for (int j = 0; j < 8; j++)
                            if (((1 << j) & buff[i]) != 0)
                                g.FillRectangle(Brushes.Black, new Rectangle(i * 10 + 20, (7 - j) * 10 + 10, 8, 8));
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                        g.DrawString(i.ToString(), font, solidBrush, new PointF(10, i * 10 + 8));
                    for (int i = 0; i < buff.Length; i++)
                        for (int j = 0; j < 8; j++)
                            if (((0x80 >> j) & buff[i]) != 0)
                                g.FillRectangle(Brushes.Black, new Rectangle(i * 10 + 20, (7 - j) * 10 + 10, 8, 8));
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
