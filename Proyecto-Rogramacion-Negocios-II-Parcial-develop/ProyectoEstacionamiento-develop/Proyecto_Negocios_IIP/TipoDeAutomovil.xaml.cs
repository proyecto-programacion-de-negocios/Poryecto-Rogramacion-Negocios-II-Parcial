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
    /// Lógica de interacción para TipoDeAutomovil.xaml
    /// </summary>
    public partial class TipoDeAutomovil : Window
    {
        ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-JDLKDN3\\SQLEXPRESS; Initial Catalog = Estacionamiento; Integrated Security = True");
        public TipoDeAutomovil()
        {
            InitializeComponent();
            RegistroAutomovil registroAutomovil = new RegistroAutomovil();

            MostrarAutomoviles();

            MostrarTipoAutomovil();
        }

        private void LbAutomoviles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAgregarAutomoviles_Click(object sender, RoutedEventArgs e)
        {
            TipoDeAutomovil tipoDeAutomovil = new TipoDeAutomovil();
            tipoDeAutomovil.Show();
            this.Close();
        }

        private void BtnEliminarAUtomovilEstacionamiento_Click(object sender, RoutedEventArgs e)
        {
            TipoDeAutomovil eliminarAutomoviles = new TipoDeAutomovil();
            eliminarAutomoviles.Show();
            this.Close();
        }

        private void BtnActualizarlistaautomovil_Click(object sender, RoutedEventArgs e)
        {
            TipoDeAutomovil actializarautomovil = new TipoDeAutomovil();
            actializarautomovil.Show();
            this.Close();
        }

        MainWindow menu = new MainWindow();
        private string sqlconnection;

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Usted esta volviendo");

            menu.Show();
            this.Close();


        }


        private void MostrarAutomoviles()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Est.Estacionamiento";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en con el cual se refleja una tabla de una BD
                    DataTable tablaAutomoviles = new DataTable();

                    sqlDataAdapter.Fill(tablaAutomoviles);

                    lbAutomoviles.DisplayMemberPath = "IdAutomovil";
          
                    lbAutomoviles.SelectedValuePath = "Placa";
                    lbAutomoviles.SelectedValuePath = "Tipo";
                    lbAutomoviles.ItemsSource = tablaAutomoviles.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void MostrarAutomovilesEstacionamiento()
        {
            try
            {
                // El query ha realizar en la BD
                string query = @"SELECT * FROM Est.Automovil a INNER JOIN Est.Registro b
                                ON a.Id = b.IdAutomovil WHERE b.IdTipo = @AutId";

                // Comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, con);

                // SqlDataAdapter para las tablas y los objetos utilizables
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    // Reemplazar el valor del parámetro del query con su valor correspondiente
                    sqlCommand.Parameters.AddWithValue("@AutId", lbAutomovilesEstacionamiento.SelectedValue);

                    // Objeto de tabla de una BaseDeDatos
                    DataTable tablaAutomovilEstacionamiento = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaAutomovilEstacionamiento);

                    // informacion del datatable en nuestro listbox
                    lbAutomovilesEstacionamiento.DisplayMemberPath = "nombre";
                    // Valor que se mostrara en el listbox seleccionado
                    lbAutomovilesEstacionamiento.SelectedValuePath = "id";
                    // hace la referencia
                    lbAutomovilesEstacionamiento.ItemsSource = tablaAutomovilEstacionamiento.DefaultView;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void MostrarTipoAutomovil()
        {
            try
            {
                string query = "SELECT * FROM Est.Tipo";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    DataTable tablaTipo = new DataTable();
                    sqlDataAdapter.Fill(tablaTipo);

                    lbTipoAutomoviles.DisplayMemberPath = "NombreTipo";
                    lbTipoAutomoviles.SelectedValuePath = "IdTipo";
                    lbTipoAutomoviles.ItemsSource = tablaTipo.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

    }
}
