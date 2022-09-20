namespace BackEnd.Models.Clases
{
    public class Apuesta
    {
        public string id { get; set; }
        public Competencia competencia { get; set; }
        public Usuario usuario { get; set; }

        public int idGanador { get; set; }

        
    }
}
