using MedidorModel;
using MedidorModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidor.Comunicacion
{
     class HebraCliente
    {
        private ClienteCom clienteCom;
        private IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }
        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese Datos Medidor");
            string datosMedido = Console.ReadLine().Trim();

            string[] datoMedidor = datosMedido.Split('|', '|', '|');
            uint nroMedidor = Convert.ToUInt32(datoMedidor[0]);
            string fechaMedidor = Convert.ToString(datoMedidor[1]);
            double valorConsumoMedidor = Convert.ToDouble(datoMedidor[2]);
            string datosMedidor = clienteCom.Leer();
            Medido medidor = new Medido()
            {
                NroMedidor = nroMedidor,
                FechaMedidor = fechaMedidor,
                ValorConsumoMedidor = valorConsumoMedidor
            };
            lock (medidorDAL)
            {
                medidorDAL.AgregarMedidor(medidor);
            }

            clienteCom.Desconectar();
        }
    }
}
