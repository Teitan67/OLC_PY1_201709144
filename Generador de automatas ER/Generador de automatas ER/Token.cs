using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_automatas_ER
{
    public class Token
    {
        public int id;
        public String token;
        public String lexema;
        public int columna;
        public int fila;

        public Token(int id, String token, String lexema, int columna, int fila)
        {
            this.id = id;
            this.token = qTokenEs(id);
            this.lexema = lexema;
            this.columna = columna;
            this.fila = fila;
        }

        String qTokenEs(int id) {
            String token = "";
            switch (id)
            {
                case 0:
                    token ="Conjuncion";
                    break;
                case 1:
                    token = "Disyuncion";
                    break;
                case 2:
                    token = "Anulable";
                    break;
                case 3:
                    token = "Multiplicidad anulable";
                    break;
                case 4:
                    token = "Multiplicidad";
                    break;
                case 5:
                    token = "Simbolo";
                    break;
                case 6:
                    token = "C_intevalo";
                    break;
                case 7:
                    token = "C_especificos";
                    break;
                case 8:
                    token = "Salto de linea";
                    break;
                case 9:
                    token = "Comilla simple";
                    break;
                case 10:
                    token = "Comilla doble";
                    break;
                case 11:
                    token = "Tabulacion";
                    break;
                case 12:
                    token = "Caracter especial";
                    break;
                case 13:
                    token = "PR_CONJ";
                    break;
                case 14:
                    token = "Dos puntos";
                    break;
                case 15:
                    token = "Identificador";
                    break;
                case 16:
                    token = "Igualacion";
                    break;
                case 17:
                    token = "Punto y coma";
                    break;
            }
            return token;

        }
    }
}
