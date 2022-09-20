using System;
using System.Runtime.InteropServices;

namespace BackEnd.Models.Clases{

    public enum Tipo_Resultado
    {
        Local,
        Visitante,
        Empate
    }

    public class Partido{

		public int id { get; set; }

		public DateTime fechaPartido { get; set; }

		public Tipo_Resultado resultado { get; set; }

		public Equipo[] visitante_local = new Equipo[2];

        public int idPenca { get; set; }


		public Partido() { }

	}

}