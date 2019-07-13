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

        // Variable miembro
        SqlConnection sqlconnection;

        public RegistroEntrada()
        {
            InitializeComponent();

            MostrarAutomovil();
        }

        private void LbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MostrarAutomovil()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Est.Automovil";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaAutomovil = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaAutomovil);

                    // Informacion la cuual se mostrara en nuestro listbox
                    LbAutomovil.DisplayMemberPath = "Automovil";
                    // El valor que se entrega en el listbox 
                    LbAutomovil.SelectedValuePath = "IdAutomovil";
                    // En esta parte hacermos referencia de Quién es la referencia de los datos para el ListBox 
                    LbAutomovil.ItemsSource = tablaAutomovil.DefaultView;
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
                // El query ha realizar en la BD
                string query = @"SELECT * FROM Est.Tipo a INNER JOIN Est.Tipo b
                                ON a.IdTipo = b.Id WHERE b.IdAutomovil = @IdTipo";

                // Comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    // Reemplazar el valor del parámetro del query con su valor correspondiente
                    sqlCommand.Parameters.AddWithValue("@EstIdTipo", LbTipoAutomovil.SelectedValue);

                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaTipo = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaTipo);

                    // Informacion la cuual se mostrara en nuestro listbox
                    LbTipoAutomovil.DisplayMemberPath = "nombreTipo";
                    // El valor que se entrega en el listbox 
                    LbTipoAutomovil.SelectedValuePath = "IdPlaca";
                    // En esta parte hacermos referencia de Quién es la referencia de los datos para el ListBox 
                    LbTipoAutomovil.ItemsSource = tablaTipo.DefaultView;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }



        private void LbAutomovil_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                // Llenar el ListBox de Animales en Automovil
                MostrarAutomovil();
            }


        private void LbTipoAutomovil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Llenar el ListBox de Animales en Automovil
            MostrarTipoAutomovil();
        }

    }
}
