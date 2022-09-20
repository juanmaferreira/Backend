using System;
using System.Runtime.InteropServices;

namespace BackEnd.Models.Clases
{
   public enum Tipo_Rol
    {
        Admin,
        Comun
    }

    public class Usuario
    {
        public int id { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public float billetera { get; set; }
        public Tipo_Rol tipoRol { get; set; }
        public List<Puntuacion> puntos_por_penca { get; set; } //Son todas los puntos por pencas en las que el usuario esta registrado
        
        public List<Prediccion> predicciones { get; set; } // Equipos
        public List<Apuesta>  apuestas { get; set; } // individual

        public List<Chat> chats { get; set; }

        public Usuario() {
            puntos_por_penca = new List<Puntuacion>();
            predicciones = new List<Prediccion>();
            chats = new List<Chat>();
            apuestas = new List<Apuesta>();
        }
       
    }
}
