using BackEnd.Models.Clases;

namespace BackEnd.Models.DataTypes
{
    public class DtPartido
    {

        public DateTime fecha   { get; set; }

        public Equipo local { get; set; }

        public Equipo visitante { get; set; }
    }
}
