using System;
using System.Runtime.InteropServices;


namespace BackEnd.Models.Clases{

	public class Liga_Equipo{

        public int id { get; set; }
        public string nombreLiga { get; set; }
		public List<Partido> partidos { get; set; }

		public bool disponible { get; set; }

        public Liga_Equipo() { 
			partidos = new List<Partido>();
		}
	}
}