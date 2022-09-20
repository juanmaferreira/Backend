namespace BackEnd.Models.Clases
{
    public class Liga_Individual
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public List<Competencia> competencias { get; set; }

        public Liga_Individual()
        {
            competencias = new List<Competencia>();
        }
    }
}
