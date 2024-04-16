namespace ClienteMVC.Models
{
    public class ClienteModel
    {

        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public int Edad { get; set; }
    }
}
