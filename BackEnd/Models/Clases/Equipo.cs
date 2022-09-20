using System;
using System.Runtime.InteropServices;

namespace BackEnd.Models.Clases{

    public enum Historial { 
        Gano,
        Perdio,
        Empato
    }

    public class Equipo{

        public int id { get; set; }

        public string nombreEquipo { get; set; }

        public Historial[] historiales = new Historial[5];

    }
}
