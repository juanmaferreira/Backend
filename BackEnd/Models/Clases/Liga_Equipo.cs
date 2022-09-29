using System;
using System.Runtime.InteropServices;


namespace BackEnd.Models.Clases{

	public class Liga_Equipo{

        public int id { get; set; }
        public string nombreLiga { get; set; }
		public List<Partido> partidos { get; set; }
		public List<Penca> pencas { get; set; }
		public int topePartidos { get; set; }
        public bool activa { get; set; }

        public Liga_Equipo() { 
			partidos = new List<Partido>();
		}
        public void actualizarEstado()
        {
            this.activa = false;
            foreach (var partido in this.partidos)
            {
                if (partido.resultado == Tipo_Resultado.Indefinido)
                {
                    this.activa = true;
                    break;
                }
            }
        }
    }
}