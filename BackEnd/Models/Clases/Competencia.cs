namespace BackEnd.Models.Clases
{
    public enum Tipo_Area
    {
        Natacion,
        Ciclismo,
        Atletismo
    }
    public class Competencia
    {
        public int Id { get; set; }
        public Tipo_Area Area { get; set; }

        public DateTime fecha_competencia { get; set; }

        public string nombre { get; set; }

        public List<Nombre> posiciones { get; set; }
        public List<Participante> participantes { get; set; }

        public int idPenca { get; set; }

        public Competencia()
        {
            posiciones = new List<Nombre>();
            participantes = new List<Participante>();
        }

    }
}
