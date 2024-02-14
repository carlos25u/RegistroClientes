using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace PabloNet.Utils
{
    internal class DataAcces
    {

        // Default


        public static string strconexionCentral = @"Data Source=181.49.117.104;Initial Catalog=PabloNet;Persist Security Info=True;User ID=ti;Password=Ati5020";
        public static string strconexion = @"Data Source=181.49.117.104;Initial Catalog=PabloNet;Persist Security Info=True;User ID=ti;Password=Ati5020";


        public DataAcces(string con)
        {
            strconexionCentral = con;
            strconexion = con;


        }


        public static SqlDataReader ConnectDBnExecuteSelectScript(string Script)
        {


            SqlDataReader dataReader = null;
            SqlConnection conn = new SqlConnection(strconexion);
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(Script, conn);
                dataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error opening connection" + ex.Message);
            }

            return dataReader;
        }
        public bool Bulk(ref DataTable dt)
        {
            try
            {


                using (SqlConnection dbConnection = new SqlConnection(strconexion))
                {
                    dbConnection.Open();
                    using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                    {
                        s.DestinationTableName = dt.TableName;


                        s.BulkCopyTimeout = 0;
                        s.WriteToServer(dt);
                    }
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return false;
            }
            return true;
        }
        public bool TestCone(string con)
        {
            SqlConnection cn = new SqlConnection(con);
            bool retorno = false;

            try
            {
                cn.Open();
                cn.Close();
                retorno = true;

                cn.Dispose();
            }
            catch (Exception)
            {
                retorno = false;

            }


            return retorno;
        }

        public bool Test(string con)
        {
            SqlConnection cn = new SqlConnection(con);
            bool retorno = false;

            try
            {
                cn.Open();
                cn.Close();
                retorno = true;

                cn.Dispose();
            }
            catch (Exception)
            {
                retorno = false;

            }


            return retorno;
        }
        /// <summary>
        /// Funcion que ejecuta un SQLCommand Tipo StoredProcedure el cual Retorna un Valor de tipo Int.
        /// </summary>
        /// <param name="valretorno"></param>
        /// <param name="cmd">SQLCommand a Ejecutar.</param>
        /// <returns>Retorna verdadero o false segun el valor retornado por el StoredProcedure.</returns>       
        public static bool SaveSPIntValueReturn(string valretorno, ref SqlCommand cmd)
        {
            bool valor = false;
            SqlConnection conexion = new SqlConnection(strconexion);
            cmd.Connection = conexion;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters[valretorno].Value != null)
                {
                    int v = 0;
                    int.TryParse(cmd.Parameters[valretorno].Value.ToString(), out v);
                    if (v > 0)
                        valor = true;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                valor = false;
            }
            finally
            {
                conexion.Close();
                cmd.Dispose();
            }

            return valor;

        }

        /// <summary>
        /// Funcion que ejecuta un SQLCommand Tipo StoredProcedure el cual Retorna un Valor de tipo Int.
        /// </summary>
        /// <param name="valretorno"></param>
        /// <param name="cmd">SQLCommand a Ejecutar.</param>
        /// <returns>Retorna verdadero o false segun el valor retornado por el StoredProcedure.</returns>       
        public bool SaveSPIntValueReturn(string valretorno, ref SqlCommand cmd, string conect)
        {
            bool valor = false;
            SqlConnection conexion = new SqlConnection(conect);
            cmd.Connection = conexion;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters[valretorno].Value != null)
                {
                    int v = 0;
                    int.TryParse(cmd.Parameters[valretorno].Value.ToString(), out v);
                    if (v == 100)
                        valor = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());

                valor = false;
            }
            finally
            {
                conexion.Close();
                cmd.Dispose();
            }

            return valor;

        }

        public decimal SaveSPIntValueReturn(string valretorno, ref SqlCommand cmd, bool numer)
        {
            decimal valor = 0;
            SqlConnection conexion = new SqlConnection(strconexion);
            cmd.CommandTimeout = 0;
            cmd.Connection = conexion;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters[valretorno].Value != null)
                {
                    decimal v = 0;
                    decimal.TryParse(cmd.Parameters[valretorno].Value.ToString(), out v);

                    if (v > 0)
                        valor = v;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                valor = 0;
            }
            finally
            {
                conexion.Close();
                cmd.Dispose();
            }

            return valor;

        }

        public decimal SaveSPIntValueReturn(string valretorno, ref SqlCommand cmd, bool numer, string conection)
        {
            decimal valor = 0;
            SqlConnection conexion = new SqlConnection(conection);
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters[valretorno].Value != null)
                {
                    decimal v = 0;
                    decimal.TryParse(cmd.Parameters[valretorno].Value.ToString(), out v);
                    if (v > 0)
                        valor = v;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                valor = 0;
            }
            finally
            {
                conexion.Close();
                cmd.Dispose();
            }

            return valor;

        }
        /// <summary>
        /// Funcion que ejecuta un comando sql para retorno de datos.
        /// </summary>
        /// <param name="Query"> Comando SQL a Ejecutar </param>
        /// <returns>Retorna un DataTable con los datos obtenidos.</returns>
        public static DataTable GetValuesInDataTable(string Query, int TipoCon = 0, int time = 30)
        {
            DataTable ret = new DataTable();
            SqlConnection con;
            if (TipoCon == 0)
                con = new SqlConnection(strconexion);
            else
                con = new SqlConnection(strconexionCentral);


            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            adapter.SelectCommand.CommandTimeout = time;
            try
            {
                con.Open();
                adapter.Fill(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ret = new DataTable();
            }
            finally
            {
                adapter.Dispose();
                con.Close();
            }

            return ret;
        }
        /// <summary>
        /// Funcion que ejecuta un comando sql para retorno de datos.
        /// </summary>
        /// <param name="Query"> Comando SQL a Ejecutar </param>
        /// <returns>Retorna un DataTable con los datos obtenidos.</returns>
        public static DataTable GetValuesInDataTableD(string Query, int TipoCon = 0, int time = 30)
        {
            DataTable ret = new DataTable();
            SqlConnection con;
            if (TipoCon == 0)
                con = new SqlConnection(strconexion);
            else
                con = new SqlConnection(strconexionCentral);


            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            adapter.SelectCommand.CommandTimeout = time;
            try
            {
                con.Open();
                adapter.Fill(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ret = new DataTable();
            }
            finally
            {
                adapter.Dispose();
                con.Close();
            }

            return ret;
        }

        public static DataTable GetValuesInDataTable(string Query, string cont)
        {
            DataTable ret = new DataTable();
            SqlConnection con;

            con = new SqlConnection(cont);


            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            adapter.SelectCommand.CommandTimeout = 0;
            try
            {
                con.Open();
                adapter.Fill(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ret = new DataTable();
            }
            finally
            {
                adapter.Dispose();
                con.Close();
            }

            return ret;
        }
        /// <summary>
        /// Funcion para ejucutar Comandos que Devuelben un valor.
        /// </summary>
        /// <param name="Query"> Comando SQl que se va ejecutar. </param>
        /// <returns> Valor entero retornado por el comando. </returns>
        public static string GetValue(string Query)
        {
            string ret = "";
            SqlConnection con = new SqlConnection(strconexion);
            SqlCommand command = new SqlCommand(Query, con);
            try
            {
                con.Open();
                ret = Convert.ToString(command.ExecuteScalar());
            }
            catch { ret = ""; }
            finally
            {
                command.Dispose();
                con.Close();
            }

            return ret;
        }
        public static double GetDoubleValue(string Query, int time = 30)
        {
            double ret = 0;
            SqlConnection con = new SqlConnection(strconexion);
            SqlCommand command = new SqlCommand(Query, con);
            command.CommandTimeout = time;
            try
            {
                con.Open();
                ret = Convert.ToDouble(command.ExecuteScalar());
            }
            catch { ret = 0; }
            finally
            {
                command.Dispose();
                con.Close();
            }

            return ret;
        }
        /// <summary>
        /// Funcion que ejecuta sentencias SQL.
        /// </summary>
        /// <param name="Query">Sentencia SQL que se va ejecutar. </param>
        /// <returns> Retorna True si el se ejecuto sastifactoriamente. </returns>
        public static bool ExQuery(string Query, int tipoConec = 0, int timeOut = 30)
        {
            bool ret = true;
            SqlConnection con;

            if (tipoConec == 0)
                con = new SqlConnection(strconexion);
            else
                con = new SqlConnection(strconexionCentral);

            SqlCommand command = new SqlCommand(Query, con);
            command.CommandTimeout = timeOut;
            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ret = false;


            }
            finally
            {
                con.Close();
                command.Dispose();
            }

            return ret;
        }
        public static bool TestConexion()
        {
            bool ret = true;

            try
            {
                SqlConnection con = new SqlConnection(strconexion);
                con.Open();

                con.Close();
            }
            catch
            {


                ret = false;
            }

            return ret;

        }

        /// <summary>
        /// Funcion que ejecuta sentencias SQL.
        /// </summary>
        /// <param name="Query"> Comando SQl que se va ejecutar. </param>
        /// <returns> Retorna la Cantidad de registros afectados. </returns>
        public int rExQuery(string Query)
        {
            int ret = 0;
            SqlConnection con = new SqlConnection(strconexion);
            SqlCommand command = new SqlCommand(Query, con);
            try
            {
                con.Open();
                ret = command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                con.Close();
                command.Dispose();
            }

            return ret;
        }
    }
}
