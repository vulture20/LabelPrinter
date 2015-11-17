using System;
using System.Windows.Forms;

namespace LabelPrinter
{
    public partial class Form2 : Form
    {
        public Form1 TheParent;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(maskedTextBox3.Text.Replace('_', '0'));
                this.TheParent.config.OffsetX = Convert.ToInt32(numericUpDown1.Value);
                this.TheParent.config.OffsetY = Convert.ToInt32(numericUpDown2.Value);
                this.TheParent.config.EtikettenBreite = float.Parse(maskedTextBox1.Text) / 10;
                this.TheParent.config.EtikettenHoehe = float.Parse(maskedTextBox2.Text) / 10;
                this.TheParent.config.RandLinks = float.Parse(maskedTextBox3.Text.Replace('_', '0')) / 100;
                this.TheParent.config.RandOben = float.Parse(maskedTextBox4.Text.Replace('_', '0')) / 100;

                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Fehlerhafte Eingabe!" + ex.ToString());
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Fehlerhafte Eingabe!" + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            textBox5.Text = fontDialog1.Font.Name;
            textBox7.Text = Math.Round(fontDialog1.Font.SizeInPoints).ToString();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            numericUpDown1.Value = this.TheParent.config.OffsetX;
            numericUpDown2.Value = this.TheParent.config.OffsetY;
            maskedTextBox1.Text = this.TheParent.config.EtikettenBreite.ToString("##0.0").PadLeft(5);
            maskedTextBox2.Text = this.TheParent.config.EtikettenHoehe.ToString("##0.0").PadLeft(5);
            maskedTextBox3.Text = this.TheParent.config.RandLinks.ToString("#0.00").PadLeft(5);
            maskedTextBox4.Text = this.TheParent.config.RandOben.ToString("#0.00").PadLeft(5);
            textBox5.Text = this.TheParent.config.FontCompanyName;
            textBox7.Text = this.TheParent.config.FontCompanySize.ToString();
            textBox6.Text = this.TheParent.config.FontSerialName;
            textBox8.Text = this.TheParent.config.FontSerialSize.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            textBox6.Text = fontDialog1.Font.Name;
            textBox8.Text = Math.Round(fontDialog1.Font.SizeInPoints).ToString();
        }
    }
}
