using BackEnd.Models.Clases;

namespace BackEnd.Models.DataTypes
{
    public class DtSuperAdmin
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public float Economia { get; set; }

        public string Password { get; set; }

        public Tipo_Rol tipo_rol { get; set; }
    }
}
