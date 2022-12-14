namespace BackEnd.Models.Clases
{
    public class Administrador
    {
        public int Id { get; set; }
        public string email { get; set; }

        public string password { get; set; }

        public string nombre { get; set; }

        public float? billetera { get; set; }

        public Tipo_Rol Tipo_Rol { get; set; }

        public List<Penca> pencas { get; set; }
    }
}
