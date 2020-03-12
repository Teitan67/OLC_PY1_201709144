using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generador_de_automatas_ER
{
    public partial class Form1 : Form
    {
        Archivo archivo;
        public Form1()
        {
            InitializeComponent();
        }
        //Escritura en consola
        public  void consola(String contenido)
        {
            richTextBox1.AppendText("\n"+contenido);
        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(new MyTabForm(new Form2(tabControl1,this)));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            tabControl1.TabPages.Add(new MyTabForm(new Form2(tabControl1, this)));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count<0)
            {


            }
            else { }
        }
    }
}
