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

		public List <Equipo> visitante_local { get; set; }
        
        public List<Prediccion> predicciones { get; set; }

        public bool enUso { get; set; }
	}

}