using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generador_de_automatas_ER
{
    public class Archivo
    {
        private String ruta;
        private String contenido;
        private String extension;
        private OpenFileDialog buscador;
        private TextReader lector;
        private TextWriter escritor;
        private Form1 padre;
        private String nombre;
        public Archivo(String extension)
        {
            this.extension = extension;         
        }
        public Archivo(String extension, Form1 consola)
        {
            this.extension = extension;
            this.padre = consola;
        }
        //Getters and Setters----------------------------------------------------------
        public String getRuta() { return ruta; }
        public String getContenido() { return contenido; }
        public void setRuta(String ruta) { this.ruta = ruta; }
        public void setContenido(String contenido) { this.contenido = contenido; }

        //Metodos de la clase----------------------------------------------------------
        public bool NuevoArchivo()
        {
           
            buscador = new OpenFileDialog();
            buscador.Filter = this.extension;

            if (buscador.ShowDialog()==DialogResult.OK) {
                setRuta(buscador.FileName);
                this.nombre = buscador.SafeFileName;
                padre.consola("Archivo abierto");
                return true;
            }
            return false;
        }
        public void GuardarArchivo(String contenido)
        {
            if (ExisteArchivo())
            {
                escritor = new StreamWriter(ruta);
                escritor.WriteLine(contenido);
                escritor.Close();
            }
            else
            {

            }
        }
        public String abrirArchivo(String ruta)
        {
            
            lector = new StreamReader(ruta);
            contenido = lector.ReadToEnd();
            lector.Close();
            return contenido;
        }
        public String getNombre() { return this.nombre; }
        private bool ExisteArchivo ()
        {
            return ruta != ""||ruta!=null;
        }

    }
}
