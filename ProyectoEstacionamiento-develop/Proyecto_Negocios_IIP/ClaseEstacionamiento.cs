using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Proyecto_Negocios_IIP
{
    class ClaseEstacionamiento
    {
        private DateTime horaEntrada;
        private string placa;
        private string tipoAutomovil;
        private decimal costo;
        SqlConnection cn = new SqlConnection("Data Source = EstacionamientoConnectionString; Initial Catalog = Proyecto_Negocios_IIP; Integrated Security = True");
        public ClaseEstacionamiento()
        {
            placa = "AñadidaPorDefecto";
            tipoAutomovil = "AñadidaPorDefecto";
        }

        public string Placa
        {
            get { return placa; }
            set { placa = value; }
        }
        public string TipoAutomovil
        {
            get { return tipoAutomovil; }
            set { tipoAutomovil = value; }
        }
        public DateTime HoraEntrada
        {
            get { return horaEntrada; }
            set { horaEntrada = value; }
        }
        public decimal Monto
        {
            get { return Monto; }
            set { Monto = value; }
        }


        //verifica si la placa ya existe o se debe insertar la placa del automovil
        public Boolean Verificar()
        {
            cn.Open();
            string query = "SELECT COUNT(*) FROM Est.Automovil WHERE Placa=@placa";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placa", Placa);

            int cantidad = Convert.ToInt32(comando.ExecuteScalar());
            cn.Close();
            if (cantidad == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        //Verificar si el vehiculo es igual al que se inserto con su placa
        public Boolean VerificarAutomovil()
        {
            cn.Open();
            string query = "SELECT COUNT(*) FROM Est.Automovil WHERE Placa=@placa AND Tipo=@IdPlaca";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placa", Placa);
            comando.Parameters.AddWithValue("@IdPlaca", TipoAutomovil);
            int cantidad = Convert.ToInt32(comando.ExecuteScalar());
            cn.Close();
            if (cantidad == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        //Insertar el Automovil en la base de datos
        public void InsertarAutomovil()
        {
            //Si la placa no existe se guarda en la base de datos
            if (Verificar() == false)
            {
                try
                {
                    cn.Open();
                    string query = "INSERT INTO Est.Automovil VALUES (@placa,@tipoAutomovil)";
                    SqlCommand comando = new SqlCommand(query, cn);

                    comando.Parameters.AddWithValue("@placa", Placa);

                    comando.Parameters.AddWithValue("@tipoAutomovil", TipoAutomovil);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("El Automovil se a Insertado en la base de datos");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }
            //Funcion para ver si el Automovil esta dentro de el estacionamiento
            if (VerificarAutomovil() == true)
            {
                try
                {
                    cn.Open();
                    string query = "INSERT INTO Est.Mostrar VALUES (GETDATE(),GEtDATE(),@placa)";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placa", Placa);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("A INGRESADO CORRECTAMENTE");
                    cn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("El Automovil se encuentra dentro del Estacionamiento");
                }
            }
            else
            {
                MessageBox.Show("El Automovil no se encontro en el Estacionamiento");
            }

        }

        //Aqui verificamos la salida del Automovil
        public void SalidaAutomovil()
        {
            try
            {
                cn.Open();
                string query = "UPDATE Est.Mostrar SET horaSalida=GETDATE() WHERE placaAutomovil =@placa";
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.AddWithValue("@placa", Placa);
                comando.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrido un error");
            }
            finally
            {
                try
                {
                    cn.Open();
                    string query = "DELETE FROM Est.Mostrar where placaAutomovil = @placa";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placa", Placa);
                    comando.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocurrido un error");

                }
            }
        }

    }
}
