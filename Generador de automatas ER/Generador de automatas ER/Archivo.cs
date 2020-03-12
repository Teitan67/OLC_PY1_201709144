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
        private SaveFileDialog centinela;
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
            this.ruta = "";
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
            if (NoExisteArchivo())
            {
                centinela = new SaveFileDialog();
                centinela.Filter = this.extension;
                if (centinela.ShowDialog() == DialogResult.OK)
                {
                    setRuta(centinela.FileName);
                    this.nombre = centinela.FileName;
                    padre.consola("Creando ruta...");
                    GuardarArchivo(contenido);
                }
            }
            else
            {
                escritor = new StreamWriter(ruta);
                escritor.WriteLine(contenido);
                escritor.Close();
                padre.consola("Archivo guardado...");
            }
        }
        public String abrirArchivo(String ruta)
        {

            contenido = File.ReadAllText(ruta);
     
            return contenido;
        }
        public String getNombre() { return this.nombre; }
        private bool NoExisteArchivo ()
        {
            return (ruta.Equals("")||ruta.Equals(null));
        }

    }
}
