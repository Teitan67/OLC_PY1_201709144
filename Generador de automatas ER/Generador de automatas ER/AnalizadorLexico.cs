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
            if (asc > 0)
            {
                return char.ConvertFromUtf32(asc);
            }
            else
            {
                return "" ;
            }
            
        }
        private int qEstadoIr(int caracter)
        {
            if ((caracter >= 65 && caracter <= 90) || (caracter >= 97 && caracter <= 122))
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
                    case 32:
                        return -1;
                    case 46:
                    case 124:
                    case 63:
                    case 42:
                    case 43:
                    case 58:
                    case 59:
                        return 91;
                    case 92:
                        return 17;
                }
            }
            return -666;
        }
        private int qIdEs(int caracter)
        {
            switch (caracter)
            {
                case 46:
                    return 0;
                case 124:
                    return 1;
                case 63:
                    return 2;
                case 42:
                    return 3;
                case 43:
                    return 4;
                case 58:
                    return 14;
                case 59:
                    return 17;
                case 110:
                    return 8;
                case 116:
                    return 11;
                case 39:
                    return 9;
                case 34:
                    return 10;
            }
            return -1;
        }
        private bool validarMultilinea(int sig1,String []contenido1, int fila1)
        {
            int sig = sig1;
            String[] contenido = contenido1;
            int fila = fila1;
            
            if (sig==33)
            {
                return true;
            }
            else
            {
                if (contenido.Length > fila + 1)
                {

                    if (contenido[fila + 1].ElementAt(0) == 33&&sig==-4)
                    {
                        
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
              
                
            }
            return false;

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
                        if ((columna + 1) < split[fila].Length)
                        {
                            cSiguiente = split[fila].ElementAt(columna + 1);
                        }
                        else
                        {
                            cSiguiente = -4;
                        }
                        sig = cSiguiente;
                        
                        if (estado==0)
                        {
                            if (sig == 44 || sig == 126)
                            {
                                estado = 9;
                            }
                            else
                            {
                                estado = qEstadoIr(cActual);
                            }
                        }
                        switch (estado)
                        {
                            case 1:
                                lexemaAux += caracter(cActual);
                                if (sig == 47)
                                {
                                    estado = 2;
                                }
                                else
                                {
                                    estado = -666;
                                }
                                break;
                            case 2:
                                if(columna == split[fila].Length-1)
                                {
                                    estado = -1;
                                }
                                else
                                {
                                    lexemaAux += caracter(cActual);
                                }
                                break;
                            case 3:
                                lexemaAux += caracter(cActual);
                                if (sig == 33)
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    estado = -666;
                                }
                                break;
                             case 4:
                                lexemaAux += caracter(cActual);
                                
                                if (validarMultilinea(sig,split,fila))
                                {
                                    estado = 5;
                                }
                                else
                                {
                                    estado = 4;
                                }
                              
                                break;
                            case 5:
                                lexemaAux+= caracter(cActual); 
                                if (sig == 62)
                                {
                                    estado = -10;
                                }
                                else
                                {
                                    estado = -666;
                                }
                                break;
                            case 6:
                                lexemaAux += caracter(cActual);
                                if (sig==62)
                                {
                                    ID = 16;
                                    estado = 90;
                                }
                                else
                                {
                                    estado = -666;
                                }
                                break;
                            case 7:
                                lexemaAux += caracter(cActual);
                                estado = 8;
                                break;
                            case 8:
                                lexemaAux += caracter(cActual);
                                if (sig==34)
                                {
                                    ID = 5;
                                    estado = 90;
                                }
                                else
                                {
                                    estado = 8;
                                }
                                break;
                            case 9:
                                lexemaAux += caracter(cActual);
                                if (sig == 44)
                                {
                                    estado = 11;
                                }
                                else if(sig==126)
                                {
                                    estado = 10;
                                }
                                else
                                {
                                    estado = -666;
                                }

                                break;
                            case 10:
                                lexemaAux += caracter(cActual);
                                if (sig>0&&sig<255||sig==-4)
                                {
                                    estado = 90;
                                    ID = 6;
                                }
                                else
                                {
                                    estado = -666;
                                }
                                break;
                            case 11:
                                lexemaAux += caracter(cActual);
                                if (sig>0&&sig<254)
                                {
                                    estado = 92;
                                }
                                else
                                {
                                    estado = -666;
                                }
                                break;
                            case 16:
                                lexemaAux += caracter(cActual);
                                if ((sig >= 65 && sig <= 90) || (sig >= 97 && sig <= 122)||(sig>=48&&sig<=57))
                                {
                                    estado = 16;   
                                }
                                else
                                {
                                    if (lexemaAux.Equals("CONJ"))
                                    {
                                        ID = 13;
                                    }
                                    else
                                    {
                                        ID = 15;
                                    }
                                    estado = 0;
                                }
                                break;
                            case 17:
                                lexemaAux += caracter(cActual);
                                if (sig == 110)
                                {
                                    estado = 90;
                                    ID = 8;
                                }
                                else if (sig == 116)
                                {
                                    estado = 90;
                                    ID = 11;
                                }
                                else if (sig == 39)
                                {
                                    estado = 90;
                                    ID = 9;
                                }
                                else if (sig == 34)
                                {
                                    estado = 90;
                                    ID = 10;
                                }
                                else
                                {
                                    estado = -666;
                                }
                                    break;
                            case -10:
                                ID = -1;
                                estado = -1;
                                break;
                            case 90:
                                lexemaAux += caracter(cActual);
                                estado = 0;
                                break;
                            case 91:
                                lexemaAux += caracter(cActual);
                                ID = qIdEs(cActual);
                                estado = 0;
                                break;
                            case 92:
                                lexemaAux += caracter(cActual);
                                if(sig==44)
                                {
                                    estado = 11;
                                }
                                else
                                {
                                    estado = 90;
                                    ID = 7;
                                }
                                break;
                            case -1:
                                estado = -1;
                                break;
                            default:
                                lexemaAux+= caracter(cActual);
                                estado = -666;
                                break;

                        }
                        if (estado == 0)
                        {
                            tbl_simbolos.Add(new Token(ID, lexemaAux, columna, fila));

                            ID = -1;
                            lexemaAux = "";
                            estado = 0;

                        }
                        else if (estado == -1)
                        {
                            lexemaAux += caracter(cActual);
                            Console.WriteLine(("Comentario ignorado: " + lexemaAux));
                            lexemaAux = "";
                            estado = 0;
                        }
                        else if (estado == -666)
                        {
                            Console.WriteLine("Error Lexico: " + lexemaAux);
                            lexemaAux = "";
                            estado = 0;
                        }
                       // Console.WriteLine(cActual);
                    }
                    
                }
            }
        }

    }
}
