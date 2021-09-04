using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lab3TP4
{
    class Program
    {
        static void Main(string[] args)
        {
            //4 - Escribir esos datos en la DB
            Console.WriteLine("Iniciando la carga de archivos de la vieja tabla al txt");
            CtrlArticulo ctrlArticulo = new CtrlArticulo();
            int contador = 0;
            double totalRegistros = ctrlArticulo.ObtenerTotalRegistros() / 50.0;
            int limite1 = 0, limite2 = 50;
            List<Articulo> articulos = new List<Articulo>();
            while (contador < totalRegistros)
            {
                List<Articulo> articulosConsulta = ctrlArticulo.Consulta(limite1, limite2);
                Console.WriteLine(articulosConsulta.Count);
                foreach (Articulo articulo in articulosConsulta)
                {
                    articulos.Add(articulo);
                }
                articulosConsulta = null;
                limite1 += 50;
                contador++;
            }
            WriteFile.EscribirArchivo(articulos);
            Console.WriteLine("Finalizada la carga de archivos al txt");
            Console.WriteLine("Iniciando la carga de archivos del txt a la nueva tabla");
            List<Articulo> articulosParaCargar = new List<Articulo>();
            ReadFile.LeerArchivo();

            /*foreach (Articulo articulo1 in articulosParaCargar)
            {
                
            }*/
            Console.WriteLine("Fin de la carga de archivos del txt a la nueva tabla");
        }

        

        

    }
}
