using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3TP4
{
    class CtrlArticulo: Conexion
    {
        public List<Articulo> Consulta(int limite1, int limite2)
        {
            MySqlDataReader reader;
            List<Articulo> lista = new List<Articulo>();
            string sql;
            //Console.WriteLine(limite1 + ", " + limite2);
            sql = $"SELECT * FROM articulo ORDER BY id LIMIT {limite1}, {limite2};";
            /*sql = "SELECT id, fechaAlta, codigo, denominacion, precio, publicado " +
                "FROM articulo ORDER BY id";*/
            MySqlConnection conexionDB = null;

            try
            {
                conexionDB = base.ConexionDB();
                conexionDB.Open();
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.ID = long.Parse(reader.GetString("id"));
                    articulo.FechaAlta = DateTime.Parse(reader.GetString("fechaAlta"));
                    articulo.Codigo = reader.GetString("codigo");
                    articulo.Denominacion = reader.GetString("denominacion");
                    articulo.Precio = double.Parse(reader.GetString("precio"));
                    articulo.Publicado = char.Parse(reader.GetString("publicado"));

                    lista.Add(articulo);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                conexionDB.Close();
            }
            return lista;
        }

        public void InsertarTransaction(Articulo datos)
        {
            MySqlConnection conexionDB = ConexionDB();
            conexionDB.Open();
            MySqlCommand myCommand = conexionDB.CreateCommand();
            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = conexionDB.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = conexionDB;
            myCommand.Transaction = myTrans;
            
            string format = "yyyy-MM-dd HH:mm:ss";
            
            string sql = "INSERT INTO articulo_copy " +
                "(fechaAlta, codigo, denominacion, precio, publicado) VALUES (" +
                "'" + datos.FechaAlta.ToString(format) + "', " +
                "'" + datos.Codigo + "', " +
                "'" + datos.Denominacion + "', " +
                "'" + datos.Precio + "', " +
                "'" + datos.Publicado + "')";

            try
            {
                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                Console.WriteLine("Se insertó un registro en la base.");
            }
            catch (Exception e)
            {
                try
                {
                    sql = "UPDATE articulo_copy SET " +
                          "fechaAlta='" + datos.FechaAlta + "', " +
                          "codigo='" + datos.Codigo + "', " +
                          "denominacion='" + datos.Denominacion + "', " +
                          "precio='" + datos.Precio + "' " +
                          "publicado='" + datos.Publicado + "' " +
                          "WHERE ID='" + datos.ID + "'";

                    myCommand.CommandText = sql;
                    myCommand.ExecuteNonQuery();
                    myTrans.Commit();
                    Console.WriteLine("Se actualizó un registro en la base.");
                }
                catch (Exception ex)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (MySqlException exe)
                    {
                        if (myTrans.Connection != null)
                        {
                            Console.WriteLine("Exception de tipo " + exe.GetType() +
                            " al ejecutar el roll back de la transaction.");
                        }
                    }
                    Console.WriteLine("Exception de tipo " + ex.GetType() +
                    " mientras se insertaban los datos.");
                }
                Console.WriteLine("Exception de tipo " + e.GetType() +
                    " mientras se insertaban los datos.");
            }
                
            finally
            {
                conexionDB.Close();
            }
        }

        public int ObtenerTotalRegistros()
        {
            int totalRegistros = 0;
            string sql = "SELECT COUNT(*) as TotalRegistros FROM articulo";
            MySqlDataReader reader;

            try
            {
                MySqlConnection conexionDB = ConexionDB();
                conexionDB.Open();
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    totalRegistros = int.Parse(reader.GetString("TotalRegistros"));
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return totalRegistros;
        }
    }
}
