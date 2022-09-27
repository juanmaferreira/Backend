using BackEnd.Models.Clases;

namespace BackEnd.Models.DataTypes
{
    public class DtPencasCompartida
    {
        public string nombre { get; set; }

        public Tipo_Penca tipoPenca { get; set; }

        public Tipo_Deporte tipoDeporte { get; set; }

        public float entrada { get; set; }

        public float pozo { get; set; }

    }
}
