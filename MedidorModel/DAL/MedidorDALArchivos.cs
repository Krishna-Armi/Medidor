using MedidorModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public class MedidorDALArchivos : IMedidorDAL
    {
        
            private MedidorDALArchivos()
            {

            }
            private static MedidorDALArchivos instancia;
            public static IMedidorDAL GetInstancia()
            {
                if (instancia == null)
                {
                    instancia = new MedidorDALArchivos();
                }
                return instancia;

            }
            private static string url = Directory.GetCurrentDirectory();
            private static string archivo = url + "/lectura.txt";




            public void AgregarMedidor(Medido medidor)
            {
                try
                {
                    using (StreamWriter write = new StreamWriter(archivo, true))
                    {
                        write.WriteLine(medidor.NroMedidor + "|" + medidor.FechaMedidor + "|" + medidor.ValorConsumoMedidor);
                        write.Flush();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            List<Medido> IMedidorDAL.ObtenerMedidor()
            {
                List<Medido> lista = new List<Medido>();
                try
                {
                    using (StreamReader reader = new StreamReader(archivo))
                    {
                        string texto = "";
                        do
                        {
                            texto = reader.ReadLine();
                            if (texto != null)
                            {
                                string[] arr = texto.Trim().Split('|');
                                uint nroMedidor = Convert.ToUInt32(arr[0]);
                                string fechaMedidor = Convert.ToString(arr[1]);
                                double valorConsumoMedidor = Convert.ToDouble(arr[2]);

                                Medido medidor = new Medido()
                                {
                                    NroMedidor = nroMedidor,
                                    FechaMedidor = fechaMedidor,
                                    ValorConsumoMedidor = valorConsumoMedidor
                                };
                                lista.Add(medidor);
                            }
                        } while (texto != null);
                    }
                }
                catch (Exception ex)
                {
                    lista = null;
                }
                return lista;
            }


        }


    }
    

    

