using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_Negocios_IIP
{
    /// <summary>
    /// Lógica de interacción para HistorialdeEstacionamiento.xaml
    /// </summary>
    public partial class HistorialdeEstacionamiento : Window
    {
        ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-JDLKDN3\\SQLEXPRESS; Initial Catalog = Estacionamiento; Integrated Security = True");

        public HistorialdeEstacionamiento()
        {
            InitializeComponent();
            MostrarAutomoviles();
        }


        private void MostrarAutomoviles()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Est.Estacionamiento";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);

                using (sqlDataAdapter)
                {
                    DataTable tablaAutomovil = new DataTable();

                    //sqlDataAdapter.Fill(tablaRegistro);

                    lbAutomoviles.DisplayMemberPath = "Placa";
                    lbAutomoviles.DisplayMemberPath = "Tipo";
                    lbAutomoviles.SelectedValuePath = "IdAutomovil";

                    //lbAutomoviles.ItemsSource = tablaRegistro.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void AgregarAutomovil()
        {

            try
            {
                RegistroAutomovil registroAutomovil = new RegistroAutomovil();
                con.Open();
                string query = "INSERT INTO Est.Automovil VALUES (@placa,@tipoAutomovil)";
                SqlCommand comando = new SqlCommand(query, con);
                comando.Parameters.AddWithValue("@placa", registroAutomovil.Placa);
                comando.Parameters.AddWithValue("@tipoAutomovil", registroAutomovil.TipoAutomovil);
                comando.ExecuteNonQuery();
                MessageBox.Show("El Automovil se ha agregado");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public List<RegistroAutomovil> MostrarEntrada()
        {
            con.Open();
            String query = @"SELECT Placa,TipoAutomovil,HoraEntrada FROM Est.Automovil  INNER JOIN Est.Registro ge
                                ON Placa = ge.PlacaAutomovil WHERE Placa = Placa";
            SqlCommand comando = new SqlCommand(query, con);
            List<RegistroAutomovil> Lista = new List<RegistroAutomovil>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                RegistroAutomovil registroAutomovil = new RegistroAutomovil();
                registroAutomovil.Placa = reder.GetString(0);
                registroAutomovil.TipoAutomovil = reder.GetInt32(1);
                registroAutomovil.HoraEntrada = reder.GetDateTime(2);
                //lbVehiculosDentroEstacionamiento.SelectedValuePath = "Placa";
                Lista.Add(registroAutomovil);
            }
            reder.Close();
            con.Close();
            return Lista;
        }




        private void BtnHistorial_Click(object sender, RoutedEventArgs e)
        {
            if (lbAutomoviles.SelectedItem == null && lbAutomoviles.SelectedItem == null)
                MessageBox.Show("Seleccione su Placa.");
            else
            {
                try
                {
                    string query = "Select FROM Est.Regitro WHERE IdTicket = @IdTicket AND IdAutomovil = @IdAutomovil";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                    sqlconnection.Open();

                    sqlCommand.Parameters.AddWithValue("@IsTicket", lbAutomoviles.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@IdAutomovil", lbAutomoviles.SelectedValue);

                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection.Close();
                    MostrarAutomoviles();
                }
            }
        }


        MainWindow menu = new MainWindow();
        private SqlConnection sqlconnection;

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Usted esta volviendo");

            menu.Show();
            this.Close();


        }
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Realmente desea salir?", "Consulta", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }
    }
}
