using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_automatas_ER
{
    class AnalizadorLexico
    {
        public ArrayList  tbl_simbolos, tbl_errores;
        private String contenido;
        public int errores = 0, tokens = 0, comentarios = 0;
        private Form1 consola;
        private Archivo archivo;
        private String nombre;
        public AnalizadorLexico(String contenido,Form1 Padre,String nombre)
        {
            this.contenido = contenido;
            tbl_simbolos = new ArrayList();
            tbl_errores = new ArrayList();
            this.consola=Padre;
            this.nombre = nombre;
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
                        return -2;
                    case 46:
                    case 124:
                    case 63:
                    case 42:
                    case 43:
                    case 58:
                    case 59:
                    case 37:
                        return 91;
                    case 92:
                        return 17;
                    case 123:
                        return 18;
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
        public String qTokenEs(int id) {
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
                    token = "Cadena especial";
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
                case 18:
                    token = "Mension de conjunto";
                    break;
                default:
                    token = "Indefinido";
                    break;
            }
            return token;

        }
        public void generarReporte()
        {
            String xlm ;
            String html;
            if (errores > 0)
            {
                xlm = "<ListaErrores>\n";
                html = "";
                consola.consola("Errores en el texto...");
                for (int i= 0;i< tbl_errores.Count;i++)
                {
                    tokenErrores aux = (tokenErrores)tbl_errores[i];
                    
                    xlm += "\t<Error>\n";
                    xlm += "\t\t<Valor>" + aux.Lexema+ "</Valor>\n";
                    xlm += "\t\t<Fila>" + aux.fila+ "</Fila>\n";
                    xlm += "\t\t<Columna>" + aux.columna+ "</Columna>\n";
                    xlm += "\t</Error>\n";
                    html += "\t<tr>\n";
                    html += "\t\t<td>" + i + "</td>\n";
                    html += "\t\t<td>" + "-666" + "</td>\n";
                    html += "\t\t<td>" + "ERROR LEXICO" + "</td>\n";
                    html += "\t\t<td>" + aux.Lexema + "</td>\n";
                    html += "\t\t<td>" + aux.columna + "</td>\n";
                    html += "\t\t<td>" + aux.fila + "</td>\n";
                    html += "\t</tr>\n";


                }
                xlm += "</ListaErrores>\n";
                consola.consola(xlm);
                generarHmtl(html, "Html| *.html");
                genetarXml(xlm, "Xml| *.xml");
            }
            else
            {
                consola.consola("Generando reporte de tokens...");
                xlm = "<ListaTokens>\n";
                html = "";
                for (int i = 0; i < tbl_simbolos.Count; i++)
                {
                    Token aux = (Token)tbl_simbolos[i];
                   
                    xlm += "\t<Token>\n";
                    xlm += "\t\t<Nombre>"+aux.token+ "</Nombre>\n";
                    xlm += "\t\t<Valor>" + aux.lexema + "</Valor>\n";
                    xlm += "\t\t<Fila>" + aux.fila + "</Fila>\n";
                    xlm += "\t\t<Columna>" + aux.columna + "</Columna>\n";
                    xlm += "\t</Token>\n";
                    //html
                    html += "\t<tr>\n";
                    html += "\t\t<td>" + i+ "</td>\n";
                    html += "\t\t<td>" + aux.id + "</td>\n";
                    html += "\t\t<td>" + aux.token + "</td>\n";
                    html += "\t\t<td>" + aux.lexema + "</td>\n";
                    html += "\t\t<td>" + aux.columna + "</td>\n";
                    html += "\t\t<td>" + aux.fila + "</td>\n";
                    html += "\t</tr>\n";

                }
                xlm += "</ListaTokens>\n";
                consola.consola(xlm);
                genetarXml(xlm, "Xml| *.xml");
                //consola.consola(html);
                generarHmtl(html, "Html| *.html");

            }
        }
        private void generarHmtl(String contenido,String extension)
        {
            archivo = new Archivo(extension, consola);
            String[] nombre = this.nombre.Split('.');
            archivo.setRuta("C:\\PY1\\" + nombre[0] + ".html");
            String cabeza=archivo.abrirArchivo("C:\\PY1\\reportes_Cabeza.txt");
            cabeza += contenido + archivo.abrirArchivo("C:\\PY1\\reportes_Fin.txt");
            archivo.GuardarArchivo(cabeza);
            

            Process.Start(archivo.getRuta());
  
        }
        private void genetarXml(String contenido, String extension)
        {
            String []nombre = this.nombre.Split('.');
            archivo = new Archivo(extension, consola);
            archivo.setRuta("C:\\PY1\\"+nombre[0]+".xml");
            archivo.GuardarArchivo(contenido);

           // Process.Start(archivo.getRuta());

        }
        public void analizar()
        {
            if (!(this.contenido.Equals("")))
            {
                int cActual, cSiguiente, sig;
                int  estado = 0, ID = -1, comentarios=0;
                String lexemaAux = "";
                String texto = contenido;
                String[] split = texto.Split('\n');
                consola.consola("Analizando....");
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
                                //lexemaAux += caracter(cActual);
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
                                //lexemaAux += caracter(cActual);
                                estado = 8;
                                break;
                            case 8:
                                lexemaAux += caracter(cActual);
                                if (sig==34)
                                {
                                    ID = 5;
                                    estado = 95;
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
                                if (sig>0&&sig<255||sig==-4)
                                {
                                    lexemaAux += caracter(cActual);
                                    estado = 90;
                                    ID = 6;
                                }
                                else if(sig==32)
                                {
                                    estado=10;
                                }
                                else
                                {
                                    lexemaAux += caracter(cActual);
                                    estado = -666;
                                }
                                break;
                            case 11:
                               
                                if (sig>0&&sig<254)
                                {
                                    lexemaAux += caracter(cActual);
                                    estado = 92;
                                }
                                else if(sig==32)
                                {
                                    estado=11;
                                }
                                else
                                {
                                    lexemaAux += caracter(cActual);
                                    estado = -666;
                                }
                                break;
                            case 12:
                                //lexemaAux += caracter(cActual);
                                if(sig==58)
                                {
                                    estado=13;
                                }
                                else
                                {
                                    estado=-666;
                                }
                                break;
                            case 13:
                                //lexemaAux += caracter(cActual);
                                estado=14;
                                break;
                            case 14:
                                lexemaAux += caracter(cActual);
                                if(sig==58)
                                {
                                    estado=15;
                                }
                                else
                                { 
                                    estado=14;
                                }
                                break;
                            case 15:
                                if(sig==93)
                                {
                                    estado=95;
                                    ID=12;
                                }
                                else
                                {
                                    estado=-666;
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
                            case 18:
                               
                                if(sig>=65&&sig<=90||sig>=97&&sig<=122)
                                {
                                     estado=19;
                                }
                                else
                                {
                                    estado=-666;
                                }
                               
                                break;
                            case 19:
                                lexemaAux += caracter(cActual);
                                if (sig==125)
                                {
                                    ID=18;
                                    estado=95;
                                }
                                else if(sig>=48&&sig<=57||sig>=65&&sig<=90||sig>=97&&sig<=122)
                                {
                                   
                                    estado=19;
                                }
                                else
                                {
                                    
                                    estado=-666;
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
                            case 95:
                                estado = 0;
                                break;
                            case -1:
                                estado = -1;
                                break;
                            case -2:
                                estado=-2;
                                break;
                            default:
                                lexemaAux+= caracter(cActual);
                                estado = -666;
                                break;

                        }
                        if (estado == 0)
                        {
                            tokens++;
                            tbl_simbolos.Add(new Token(ID, lexemaAux, columna, fila));
                            consola.consola("No:"+tokens+" ID:"+ID+" Token: "+qTokenEs(ID)+" Lexema: "+lexemaAux+" Columna: "+columna+" Fila: "+fila);
                            ID = -1;
                            lexemaAux = "";
                            estado = 0;                           
                        }
                        else if (estado == -1)
                        {
                            lexemaAux += caracter(cActual);
                            Console.WriteLine(("Comentario ignorado: " + lexemaAux));
                            lexemaAux = "";
                            comentarios++;
                            estado = 0;

                        }
                        else if(estado==-2)
                        {
                            lexemaAux = "";
                            estado = 0;
                        }           
                        else if (estado == -666)
                        {
                            //Console.WriteLine("Error Lexico: " + lexemaAux);
                             ++errores;
                            tbl_errores.Add(new tokenErrores(errores,lexemaAux,columna,fila));
                            consola.consola("Error:"+errores+" Error lexico: "+lexemaAux);
                            lexemaAux = "";
                           
                            estado = 0;
                        }
                       // Console.WriteLine(cActual);
                    }
                    
                }
                 consola.consola("Tokens: "+tokens+" Errores: "+ errores + " Comentarios: "+comentarios);
            }
        }
        
        //Graficador de automata

        class Afn
        {
            int Inicio, fin;
            String contenido;
            public Afn(int inicio, int fin, String contenido)
            {
                this.Inicio = inicio;
                this.fin = fin;

                this.contenido = contenido;
            }
        }
        
        private Afn t0_conjuncion(int estado, String cA,String cB)
        {
            Afn automata;
            String contenido ="";
            int A0=++estado, A1=++estado, A2=++estado;
            contenido += "	ESTADO-"+ A0 + " ->ESTADO-"+ A1 + " [ label = \""+cA+"\" ]";
            contenido += "  ESTADO-"+A1+"->ESTADO-"+ A2+"[label = \""+cB+"\"]; ";
            automata = new Afn(A0,A2,contenido);
            return automata;
        }
        private Afn t0_conjuncion( Afn cA, String cB)
        {
            Afn automata;
            int estado=cA. 
            String contenido = "";
            int A0 = ++estado, A1 = ++estado, A2 = ++estado;
            contenido += "	ESTADO-" + A0 + " ->ESTADO-" + A1 + " [ label = \"" + cA + "\" ]";
            contenido += "  ESTADO-" + A1 + "->ESTADO-" + A2 + "[label = \"" + cB + "\"]; ";
            automata = new Afn(A0, A2, contenido);
            return automata;
        }

    } 
}
