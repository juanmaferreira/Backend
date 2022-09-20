using System;
using System.Runtime.InteropServices;

namespace BackEnd.Models.Clases
{
    public enum Tipo_Penca
    {
        Compartida,
        Empresa
    }

    public enum Tipo_Deporte
    {
        Futbol,
        Basquetball
    }

    public enum Tipo_Plan
    {
        Basico,
        Premium
    }
    public enum Tipo_Liga
    {
        Equipo,
        Individual
    }

    public class Penca
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public bool estado { get; set; }

        public DateTime fecha_Creacion { get; set; }

        public Tipo_Penca tipo_Penca { get; set; }

        public Tipo_Deporte tipo_Deporte { get; set; }

        public List<Mensaje> foro { get; set; }

        //Empresa
        public float? premio_empresa { get; set; }

        public Tipo_Plan? tipo_Plan { get; set; }

        //Compartido
        public float? entrada { get; set; }

        public float? pozo { get; set; }

        //Relaciones
        public Liga_Equipo liga_Equipo { get; set; }

        public List<Puntuacion> participantes_puntos { get; set; } // Aca guarda todas las puntuaciones de cada participante para luego ranquearlo y entregar el premio

        public Tipo_Liga tipo_Liga { get; set; }

        public string color { get; set; }


        //Constructores
        public Penca() {
            foro = new List<Mensaje>();
            participantes_puntos = new List<Puntuacion>();
        }
    }
}




