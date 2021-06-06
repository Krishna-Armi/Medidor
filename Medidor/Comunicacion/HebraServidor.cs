using MedidorModel;
using MedidorModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medidor.Comunicacion
{
    public class HebraServidor
    {
        private IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();
        public void Ejecutar()
        {
            try
            {
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
                
                Console.WriteLine("Inniciando Servidor en puente {0}", puerto);
                ServerSocket servidor = new ServerSocket(puerto);
                if (servidor.Iniciar())
                {
                    Console.WriteLine("Servidor iniciado");
                    while (true)
                    {
                        Console.WriteLine("Esperando .....");
                        Socket cliente = servidor.ObtenerCliente();
                        Console.WriteLine("Cliente recibido");
                        ClienteCom clienteCom = new ClienteCom(cliente);

                        HebraCliente clienteThread = new HebraCliente(clienteCom);
                        Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                        t.IsBackground = true;
                        t.Start();

                    }

                }
                else
                {
                    Console.WriteLine("fallo de conexion, no es posible iniciar server en {0}", puerto);
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Este es el fallo", ex);
            }
           
        }
    }
}
