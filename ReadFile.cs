using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3TP4
{
    class ReadFile
    {
        public static void LeerArchivo()
        {
            CtrlArticulo ctrlArticulo = new CtrlArticulo();
            try
            {
                StreamReader reader = new StreamReader(@"C:\Users\choco\Desktop\temp.txt");
                string linea = reader.ReadLine();//leo hasta el salto de linea
                int fila = 0;
                int columna = 0;
                List<Articulo> articulos = new List<Articulo>();
                while (linea != null)// Lee líneas mientras haya lineas (mientras sean !=null)
                {
                    if (fila == 50)
                    {
                        foreach (Articulo articulo1 in articulos)
                        {
                            ctrlArticulo.InsertarTransaction(articulo1);
                        }
                        Console.WriteLine(fila);
                        articulos.Clear();
                        fila = 0;
                        Console.WriteLine(fila);
                    }
                    //Console.WriteLine(linea);
                    string[] split = linea.Split('\t');//separo las columnas de la fila
                    Articulo articulo = new Articulo();
                    foreach (string s in split)
                    {
                        if (s.Trim() != "")
                        {
                            switch (columna)
                            {
                                case 0:
                                    articulo.ID = long.Parse(s);
                                    break;
                                case 1:
                                    articulo.FechaAlta = DateTime.Parse(s);
                                    break;
                                case 2:
                                    articulo.Codigo = s;
                                    break;
                                case 3:
                                    articulo.Denominacion = s;
                                    break;
                                case 4:
                                    articulo.Precio = double.Parse(s);
                                    break;
                                case 5:
                                    articulo.Publicado = char.Parse(s);
                                    break;
                                default:

                                    break;
                            }
                            
                        }
                        columna++;
                        //Console.WriteLine(s);
                        //Console.WriteLine(columna);
                        
                    }
                    columna = 0;
                    articulos.Add(articulo);
                    fila++;
                    linea = reader.ReadLine();//leo siguiente linea si existe
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
