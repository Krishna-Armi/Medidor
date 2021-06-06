using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel
{
    public class Medido
    {
        private uint nroMedidor;
        private string fechaMedidor;
        private double valorConsumoMedidor;

        public uint NroMedidor { get => nroMedidor; set => nroMedidor = value; }
        public string FechaMedidor { get => fechaMedidor; set => fechaMedidor = value; }
        public double ValorConsumoMedidor { get => valorConsumoMedidor; set => valorConsumoMedidor = value; }

        public override string ToString()
        {
            return nroMedidor
                + "|"
                + fechaMedidor + "|"
                + valorConsumoMedidor;
        }
    }
}
