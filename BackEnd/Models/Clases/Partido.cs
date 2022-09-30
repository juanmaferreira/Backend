using System;
using System.Runtime.InteropServices;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models.Clases{

    public enum Tipo_Resultado
    {
        Local,
        Visitante,
        Empate,
        Indefinido
    }

    public class Partido
    {

        public int id { get; set; }

        public DateTime fechaPartido { get; set; }

        public Tipo_Resultado resultado { get; set; }

        public List<Equipo> visitante_local { get; set; }

        public List<Prediccion> predicciones { get; set; }

        public bool enUso { get; set; }

        public DtEstadisticas Estadisticas { get; set; }

        public void actualizarEstadisticas(Tipo_Resultado prediccion)
        {
            if (prediccion == Tipo_Resultado.Local)
            {
                this.Estadisticas.local++;
            }
            else if (prediccion == Tipo_Resultado.Visitante)
            {
                this.Estadisticas.visitante++;
            }
            else if (prediccion == Tipo_Resultado.Empate)
            {
                this.Estadisticas.empate++;
            }
        }
    }
}
