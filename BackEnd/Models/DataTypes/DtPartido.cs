using BackEnd.Models.Clases;

namespace BackEnd.Models.DataTypes
{
    public class DtPartido
    {
        public int Id { get; set; }
        public DateTime fecha   { get; set; }

        public int Idlocal { get; set; }

        public int Idvisitante { get; set; }

        public Tipo_Resultado resultado { get; set; }
    }
}
