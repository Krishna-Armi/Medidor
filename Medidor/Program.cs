using MedidorModel.DAL;
using MedidorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ServidorSocketUtils;
using System.Net.Sockets;
using Medidor.Comunicacion;
using System.Threading;

namespace Medidor
{
     class Program
    {

        private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();
        static void Main(string[] args)
        {

            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            while (Menu()) ;

        }
        static bool Menu()
        {

           

            bool continuar = true;
            Console.WriteLine("Bienvenido Al Medidor");
            Console.WriteLine(" 1.Ingresar \n 2.Mostrar \n 0.Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1": Ingresar();
                    break;
                case "2": Mostrar();
                    break;
                case "0": continuar = false;
                    break;
                default: Console.WriteLine("Ingresar de nuevo");
                    break;


            }
            return continuar;
        }

        
        private static void Mostrar()
        {
            List<Medido> medidors = null;
            lock (medidorDAL)
            {
                medidors = medidorDAL.ObtenerMedidor();
            }
            foreach (Medido medidor in medidors)
            {
                Console.WriteLine(medidor);
            }
        }

        private static void Ingresar()
        {
            Console.WriteLine("Ingrese Datos Del Medidor");
            string datosMedidor = Console.ReadLine().Trim();

            string[] datoMedidor = datosMedidor.Split('|', '|', '|');
            uint nroMedidor = Convert.ToUInt32(datoMedidor[0]);
            string fechaMedidor = Convert.ToString(datoMedidor[1]);
            double valorConsumoMedidor = Convert.ToDouble(datoMedidor[2]);

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
            
        }

    }
}
