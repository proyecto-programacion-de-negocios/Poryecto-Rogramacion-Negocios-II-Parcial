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
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Proyecto_Negocios_IIP
{
    /// <summary>
    /// Lógica de interacción para RegistroEntrada.xaml
    /// </summary>
    public partial class RegistroEntrada : Window
    {
        ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-JDLKDN3\\SQLEXPRESS; Initial Catalog = Estacionamiento; Integrated Security = True");
        public RegistroEntrada()
        {
            InitializeComponent();
            RegistroAutomovil registroAutomovil = new RegistroAutomovil();

            this.lbAutomovil.ItemsSource = MostrarEntrada();
        }
        
        MainWindow menu = new MainWindow();
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

        private void MostrarAutomovil()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Est.Automovil";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);

                using (sqlDataAdapter)
                {
                    DataTable tablaAutomovil = new DataTable();

                    sqlDataAdapter.Fill(tablaAutomovil);

                    lbAutomovil.DisplayMemberPath = "Placa";
                    lbAutomovil.DisplayMemberPath = "Tipo";
                    lbAutomovil.SelectedValuePath = "IdAutomovil";

                    lbAutomovil.ItemsSource = tablaAutomovil.DefaultView;
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

        private void MostrarTipoAutomovil()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Est.Tipo";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);

                using (sqlDataAdapter)
                {
                    DataTable tablaTipo = new DataTable();

                    sqlDataAdapter.Fill(tablaTipo);

                    lbTipo.DisplayMemberPath = "NombreTipo";
                    lbTipo.SelectedValuePath = "IdTipo";

                    lbTipo.ItemsSource = tablaTipo.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void AgregarTipo()
        {

            try
            {
                RegistroAutomovil registroAutomovil = new RegistroAutomovil();
                con.Open();
                string query = "INSERT INTO Est.Tipo VALUES (@NombreTipo)";
                SqlCommand comando = new SqlCommand(query, con);
                comando.Parameters.AddWithValue("@IdTipo", registroAutomovil.IdTipo);
                comando.Parameters.AddWithValue("@NombreTipo", registroAutomovil.NombreTipo);
                comando.ExecuteNonQuery();
                MessageBox.Show("El Tipo se ha agregado");

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

        public List<RegistroAutomovil> MostrarTipo()
        {
            con.Open();
            String query = @"SELECT IdTipo,NombreTipo FROM Est.Tipo  INNER JOIN Est.Tipo ge
                                ON IdTipo = ge.NombreTipo WHERE IdTipo = NombreTipo";
            SqlCommand comando = new SqlCommand(query, con);
            List<RegistroAutomovil> Lista = new List<RegistroAutomovil>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                RegistroAutomovil registroAutomovil = new RegistroAutomovil();
                registroAutomovil.IdTipo = reder.GetString(0);
                registroAutomovil.NombreTipo = reder.GetString(1);
               // registroAutomovil.HoraEntrada = reder.GetDateTime(2);
                //lbVehiculosDentroEstacionamiento.SelectedValuePath = "Placa";
                Lista.Add(registroAutomovil);
            }
            reder.Close();
            con.Close();
            return Lista;
        }

    }
}
