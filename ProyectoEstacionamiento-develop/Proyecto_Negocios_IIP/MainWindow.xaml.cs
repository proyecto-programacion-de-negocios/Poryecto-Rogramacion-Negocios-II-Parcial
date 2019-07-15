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
        public MainWindow()
        {
            InitializeComponent();
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
}
