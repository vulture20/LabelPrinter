using BarcodeLib;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LabelPrinter
{
    public partial class Form1 : Form
    {
        private int actualPage = 1;
        public Config config = new Config();

        public Form1()
        {
            InitializeComponent();
        }

        public Config getConfigObj() {
            return config;
        }

        private void initializeConfig()
        {
            if (File.Exists(@"LabelPrinter.config"))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Config));
                    StreamReader sr = new StreamReader(@"LabelPrinter.config");
                    config = (Config)ser.Deserialize(sr);
                    sr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Konfigurationsdatei konnte nicht geladen werden!\n" + ex.ToString());
                    //Application.Exit();
                }
            }
            else
            {
                config.Text = @"AKAFÖ Diamant";
                config.Nummernkreis = 101;
                config.Anfang = 1;
                config.Ende = 130;
                config.OffsetX = 5;
                config.OffsetY = 5;
                config.EtikettenBreite = 38.1F;
                config.EtikettenHoehe = 21.2F;
                config.RandLinks = 9.75F;
                config.RandOben = 10.7F;
                config.FontCompanyName = "SansSerif";
                config.FontCompanySize = 12;
                config.FontSerialName = "Courier New";
                config.FontSerialSize = 12;

                XmlSerializer ser = new XmlSerializer(typeof(Config));
                try
                {
                    FileStream str = new FileStream(@"LabelPrinter.config", FileMode.Create);
                    ser.Serialize(str, config);
                    str.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Konfigurationsdatei konnte nicht angelegt werden!\n" + ex.ToString());
                    //Application.Exit();
                }
            }
        }

        private void refreshControls()
        {
            textBox1.Text = config.Text;
            numericUpDown1.Value = config.Nummernkreis;
            numericUpDown2.Value = config.Anfang;
            numericUpDown3.Value = config.Ende;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeConfig();
            refreshControls();

            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1170); // all sizes are converted from mm to inches & then multiplied by 100.
            textBox2.Text = Math.Ceiling((numericUpDown3.Value - numericUpDown2.Value) / 65).ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value <= numericUpDown2.Value)
            {
                MessageBox.Show("End-Wert darf nicht kleiner oder gleich dem Anfangswert sein!");
                numericUpDown3.Value = numericUpDown2.Value + 1;
            }

            textBox2.Text = Math.Ceiling((numericUpDown3.Value - numericUpDown2.Value) / 65).ToString();

            config.Anfang = Convert.ToInt32(numericUpDown2.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value <= numericUpDown2.Value)
            {
                MessageBox.Show("End-Wert darf nicht kleiner oder gleich dem Anfangswert sein!");
                numericUpDown3.Value = numericUpDown2.Value + 1;
            }

            textBox2.Text = Math.Ceiling((numericUpDown3.Value - numericUpDown2.Value) / 65).ToString();

            config.Ende = Convert.ToInt32(numericUpDown3.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                actualPage = 1;
                printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beim Druck ist ein Fehler aufgetreten: ", ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualPage = 1;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            const int offsetX = 5, offsetY = 5;
            const float etikettenBreite = (float)(38.1);
            const float randLinks = (float)(9.75);
            const float etikettenHoehe = (float)(21.2);
            const float randOben = (float)(10.7);

            Font fontCompany = new Font(config.FontCompanyName, config.FontCompanySize);
            Font fontSerial = new Font(config.FontSerialName, config.FontSerialSize);
            Barcode barcode = new Barcode();
            Image barcodeImage;
            Pen pen = new Pen(Color.Black);
            int counter = Convert.ToInt32(numericUpDown2.Value) + ((actualPage - 1) * 65);

            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            barcode.Alignment = AlignmentPositions.LEFT;
            barcode.Height = 35;
            barcode.Width = 200;
            barcode.IncludeLabel = false;
            barcode.LabelFont = fontSerial;
            barcode.LabelPosition = LabelPositions.BOTTOMCENTER;

            if ((numericUpDown3.Value - numericUpDown2.Value + 1) / 65 > actualPage)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 13; y++)
                {
                    if (counter <= numericUpDown3.Value)
                    {
                        float fx = (float)(randLinks + (x * etikettenBreite) - offsetX);
                        float fy = (float)(randOben + (y * etikettenHoehe) - offsetY);
                        String serial = numericUpDown1.Value.ToString() + " " + counter.ToString("D6");

                        barcodeImage = barcode.Encode(TYPE.CODE128, serial);
                        e.Graphics.DrawImage(barcodeImage, new PointF(fx + 3.0F, fy + 1.0F));
                        barcodeImage.Dispose();
                        e.Graphics.DrawString(serial, fontSerial, Brushes.Black, new PointF(fx + (etikettenBreite - e.Graphics.MeasureString(serial, fontSerial).Width) / 2, fy + 10.0F));
                        e.Graphics.DrawString(textBox1.Text, fontCompany, Brushes.Black, new PointF(fx + (etikettenBreite - e.Graphics.MeasureString(textBox1.Text, fontCompany).Width) / 2, fy + 16.0F));

                        counter++;
                    }
                }
            }

            if (gitterDruckenToolStripMenuItem.Checked)
            {
                for (int x = 0; x <= 5; x++)
                {
                    float fx = (float)(randLinks + (x * etikettenBreite) - offsetX);

                    e.Graphics.DrawLine(pen, new PointF(fx, 0), new PointF(fx, e.MarginBounds.Bottom));
                }

                for (int y = 0; y <= 13; y++)
                {
                    float fy = (float)(randOben + (y * etikettenHoehe) - offsetY);

                    e.Graphics.DrawLine(pen, new PointF(0, fy), new PointF(e.MarginBounds.Right, fy));
                }
            }

            actualPage++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numericUpDown3.Value += 65-((numericUpDown3.Value - numericUpDown2.Value) % 65) - 1;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(Config));
                TextWriter WriteFileStream = new StreamWriter(@"LabelPrinter.config");
                ser.Serialize(WriteFileStream, config);
            }
            catch { }
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();

            box.ShowDialog();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void experteneinstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 expertenEinstellungen = new Form2();

            expertenEinstellungen.TheParent = this;
            expertenEinstellungen.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            config.Text = textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            config.Nummernkreis = Convert.ToInt32(numericUpDown1.Value);
        }
    }
}
