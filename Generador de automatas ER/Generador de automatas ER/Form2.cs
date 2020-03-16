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
    public partial class Form2 : MyFormPage
    {
        TabControl tab;
        Archivo archivo;
        Form1 padre;
        public Form2(TabControl tabControl1,Form1 Padre)
        {
            InitializeComponent();
            archivo = new Archivo("ER| *.er", Padre);
            this.pnl = panel1;
            this.tab = tabControl1;
            this.padre = Padre;
            if (archivo.NuevoArchivo())
            {

                this.richTextBox1.Text = archivo.abrirArchivo(archivo.getRuta());
                this.Text = archivo.getNombre();
            }
            
        }
        public Form2(String nombre, TabControl tabControl1, Form1 Padre)
        {
            InitializeComponent();
            archivo = new Archivo("ER| *.er", Padre);
            this.Text = nombre;
            this.pnl = panel1;
            this.tab = tabControl1;
            this.padre = Padre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eliminarTab();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnalizadorLexico analizador = new AnalizadorLexico(richTextBox1.Text);
            analizador.analizar();
            padre.consola("Analizando....");
        }
        private void eliminarTab() {
            tab.TabPages.Remove(tab.SelectedTab);
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminarTab();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archivo.GuardarArchivo(richTextBox1.Text);
        }
    }
}
