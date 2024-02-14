using System.ComponentModel.DataAnnotations;

namespace RegistroClientes.Model
{
    public class Clientes
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public Double Balance { get; set; }

        public Clientes()
        {
            ClienteId = 0;
            Nombre = string.Empty;
            Balance = 0;
        }

    }
}
