namespace BackEnd.Models.Clases
{
    public class Liga_Individual
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public List<Competencia> competencias { get; set; }
        public List<Penca> pencas { get; set; }
        public int topeCompetencias { get; set; }
        public bool activa { get; set; }
        public Liga_Individual()
        {
            competencias = new List<Competencia>();
            pencas = new List<Penca>();
        }

        public static implicit operator Liga_Individual(Liga_Equipo v)
        {
            throw new NotImplementedException();
        }
        public void actualizarEstado()
        {
            this.activa = false;
            foreach (var competencia in this.competencias)
            {
                if (competencia.posiciones.Count == 0)
                {
                    this.activa = true;
                    break;
                }
            }
        }
    }
}
