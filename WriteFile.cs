using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3TP4
{
    class WriteFile
    {
        public static void EscribirArchivo(List<Articulo> articulos)
        {
            try
            {
                StreamWriter writer = new StreamWriter(@"C:\Users\choco\Desktop\temp.txt");
                int filas = 1;
                int columnas = 6;
                StringBuilder buffer = new StringBuilder();
                for (int f = 0; f < articulos.Count; f++)
                {
                    for (int c = 0; c < columnas; c++)
                    {
                        switch (c)
                        {
                            case 0:
                                buffer.Append(articulos[f].ID);
                                buffer.Append("\t");//separador de columnas
                                break;
                            case 1:
                                buffer.Append(articulos[f].FechaAlta);
                                buffer.Append("\t");//separador de columnas
                                break;
                            case 2:
                                buffer.Append(articulos[f].Codigo);
                                buffer.Append("\t");//separador de columnas
                                break;
                            case 3:
                                buffer.Append(articulos[f].Denominacion);
                                buffer.Append("\t");//separador de columnas
                                break;
                            case 4:
                                buffer.Append(articulos[f].Precio);
                                buffer.Append("\t");//separador de columnas
                                break;
                            case 5:
                                buffer.Append(articulos[f].Publicado);
                                buffer.Append("\t");//separador de columnas
                                break;
                            default:
                                break;
                        }
                        
                    }
                    buffer.Append("\n");//separador de filas
                    //Console.WriteLine("Articulo " + f + "Agregado");
                }
                writer.WriteLine(buffer.ToString());
                //Console.WriteLine("Ejecución");
                writer.Close();
                /*
                 La diferencia entre el método Write y WriteLine, es que el segundo inserta un salto de línea 
                 * al final de los datos ingresados, haciendo que la próxima vez que se quiera insertar, 
                 * se hará en la siguiente línea.
                 */
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
