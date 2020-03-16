using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_automatas_ER
{
    class AnalizadorLexico
    {
        public ArrayList tbl_simbolos;
        private String contenido;
        public int errores = 0, tokens = 0, comentarios = 0;

        public AnalizadorLexico(String contenido)
        {
            this.contenido = contenido;
            tbl_simbolos = new ArrayList();
            analizar();
        }
        public String caracter(int asc)
        {
            return char.ConvertFromUtf32(asc);
        }
        private int qEstadoIr(int caracter)
        {
            if ((caracter >= 65 && caracter <= 90) || (caracter <= 97 && caracter >= 122))
            {
                return 16;
            }
            else
            {
                switch (caracter)
                {
                    case 60:
                        return 3;
                    case 47:
                        return 1;
                    case 34:
                        return 7;
                    case 45:
                        return 6;
                    case 91:
                        return 12;
                }
            }
            return -666;
        }
        public void analizar()
        {
            if (!(this.contenido.Equals("")))
            {
                int cActual, cSiguiente, sig;
                int error = 0, estado = 0, cantidad = 0, ID = -1, indice = 0;
                String lexemaAux = "";
                String tokenAux = "";
                String tipoToken = "";
                String texto = contenido;
                String[] split = texto.Split('\n');
                int caracteres = 0;
                for (int fila = 0; fila < split.Length; fila++)
                {
                    for (int columna = 0; columna < split[fila].Length; columna++)
                    {
                        cActual = split[fila].ElementAt(columna);
                        if((columna + 1)<=split.Length)
                        {
                            cSiguiente = split[fila].ElementAt(columna + 1);
                        }
                        else
                        {
                            cSiguiente = -4;
                        }
                            
                        
                       
                        sig = cSiguiente;
                        Console.WriteLine(caracteres++);

                    }
                    
                }
            }
        }

    }
}
