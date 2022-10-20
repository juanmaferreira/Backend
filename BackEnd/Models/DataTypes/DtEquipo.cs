using BackEnd.Models.Clases;

namespace BackEnd.Models.DataTypes
{
    public class DtEquipo
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public Tipo_Deporte Deporte { get; set; }
    }
}
