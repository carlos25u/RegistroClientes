using PabloNet.Utils;
using RegistroClientes.Model;
using System.Data.SqlClient;
using System.Data;

namespace RegistroClientes.BLL
{
    public class ClientesBLL
    {
        public async Task<bool> Guardar(Clientes cliente)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SPSaveClientes";
            cmd.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@Balance", cliente.Balance);
            cmd.Parameters.Add("@ValorRetorno", SqlDbType.Int);
            cmd.Parameters["@ValorRetorno"].Direction = ParameterDirection.Output; 
            return DataAcces.SaveSPIntValueReturn("@ValorRetorno", ref cmd);
        }

        public async Task<List<Clientes>> ListadoAsync()
        {
            List<Clientes> cliente = new List<Clientes>();
            string sql = "SELECT * FROM Clientes";
            DataTable dt = DataAcces.GetValuesInDataTable(sql);

            foreach (DataRow item in dt.Rows)
            {
                cliente.Add(new Clientes()
                {
                    ClienteId = (int)item["ClienteId"],
                    Nombre = item["Nombre"].ToString(),
                    Balance = (Double)item["Balance"]
                });
            }

            return cliente;

        }

        public async Task<bool> Eliminar(int id)
        {
            return DataAcces.ExQuery("DELETE FROM Clientes WHERE ClienteId = " + id);
        }

        public async Task<Clientes> Buscar(int id)
        {
            string sql = "SELECT * FROM Clientes WHERE ClienteId = " + id;
            DataTable dt = DataAcces.GetValuesInDataTable(sql);
            Clientes cliente = new Clientes();
            foreach (DataRow item in dt.Rows)
            {
                cliente.ClienteId = (int)item["ClienteId"];
                cliente.Nombre = item["Nombre"].ToString();
                cliente.Balance = (Double)item["Balance"];
            }
            return cliente;
        }
    }
}
