using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_automatas_ER
{
    class tokenErrores
    {
        public String Lexema;
        public int fila, columna,indice;
        public tokenErrores(int indice,String lexema, int columna, int fila)
        {
            this.indice = indice;
            this.Lexema = lexema;
            this.columna = columna;
            this.fila = fila;
        }
    }
}
