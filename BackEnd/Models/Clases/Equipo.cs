using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace BackEnd.Models.Clases{

    

    public class Equipo{

        public int id { get; set; }

        public string nombreEquipo { get; set; }

        public List<Historial> historiales { get; set; }
    }
}
