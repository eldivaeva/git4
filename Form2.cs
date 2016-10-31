using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace design
{
    public partial class addResource : Form
    {
        public addResource()
        {
            InitializeComponent();
        }

        public static int index = 0;
        private void Form2_Load(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Globals.MyGlobals.res_name[Globals.MyGlobals.index++] = textBox1.Text;
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                main.dataGridView2.Rows.Add();
                main.dataGridView2.Rows[index].Cells[0].Value = textBox1.Text;
                main.dataGridView2.Rows[index].Cells[1].Value = comboBox1.Text;
                main.dataGridView2.Rows[index].Cells[2].Value = textBox3.Text;
                index++;
                main.listBox1.Items.Add(textBox1.Text);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox3.Text == "")
                simpleButton1.Enabled = false;
            if ((textBox1.Text != "") && (comboBox1.Text != "") && (textBox3.Text != ""))
                simpleButton1.Enabled = true;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox3.Text == "")
                simpleButton1.Enabled = false;
            if ((textBox1.Text != "") && (comboBox1.Text != "") && (textBox3.Text != ""))
                simpleButton1.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox3.Text == "")
                simpleButton1.Enabled = false;
            if ((textBox1.Text != "") && (comboBox1.Text != "") && (textBox3.Text != ""))
                simpleButton1.Enabled = true;
        }

    }
}
