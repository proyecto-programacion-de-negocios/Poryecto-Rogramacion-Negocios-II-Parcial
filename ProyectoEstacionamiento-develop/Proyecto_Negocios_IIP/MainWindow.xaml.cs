using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Negocios_IIP
{
    // Agregando los namespaces necesarios para SQL Server
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Data;

    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-JDLKDN3\\SQLEXPRESS; Initial Catalog = Estacionamiento; Integrated Security = True");
        public MainWindow()
        {
            InitializeComponent();
            RegistroAutomovil registroAutomovil = new RegistroAutomovil();

            this.lbAutomoviles.ItemsSource = MostrarEntrada();
        }

        private void MostrarVehiculos()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Est.Estacionamiento";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);

                using (sqlDataAdapter)
                {
                    DataTable tablaZoologico = new DataTable();

                    //sqlDataAdapter.Fill(tablaRegistro);

                    lbAutomoviles.DisplayMemberPath = "ciudad";

                    lbAutomoviles.SelectedValuePath = "id";

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
                string query = "INSERT INTO Estacionamiento.Vehiculo VALUES (@placa,@tipovehiculo)";
                SqlCommand comando = new SqlCommand(query, con);
                comando.Parameters.AddWithValue("@placa", registroAutomovil.Placa);
                comando.Parameters.AddWithValue("@tipovehiculo", registroAutomovil.TipoAutomovil);
                comando.ExecuteNonQuery();
                MessageBox.Show("El vehiculo se ha agregado");

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


            private void btnHoraEntrada_Click(object sender, RoutedEventArgs e)
        {
            RegistroEntrada registroEntrada = new RegistroEntrada();
            registroEntrada.Show();
            this.Close();
        }

        private void btnHoraSalida_Click(object sender, RoutedEventArgs e)
        {
            RegistroSalida registroSalida = new RegistroSalida();
            registroSalida.Show();
            this.Close();
        }

        private void btnTipoAutomovil_Click(object sender, RoutedEventArgs e)
        {
            TipoDeAutomovil tipoautomovil = new TipoDeAutomovil();
            tipoautomovil.Show();
            this.Close();
        }

        private void btnRegistroAutomovil_Click(object sender, RoutedEventArgs e)
        {
            HistorialdeEstacionamiento historialdeEstacionamiento = new HistorialdeEstacionamiento();
            historialdeEstacionamiento.Show();
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

    public class RegistroAutomovil
    {
        public int IdAutomovil { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string IdTipo { get; set; }
        public string NombreTipo { get; set; }
        public int IdMonto { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public string IdTicket { get; set; }
        public string PlacaAutomovil { get; set; }
        public int TipoAutomovil { get; set; }
        public int TotalHoras { get; set; }
        public Decimal Monto { get; set; }
    }
}
