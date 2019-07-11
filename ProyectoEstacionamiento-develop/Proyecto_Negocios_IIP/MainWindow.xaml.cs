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
            /*RegistroSalida registroSalida = new RegistroSalida();
            registroSalida.Show();
            this.Close();*/
        }

        private void btnRegistroAutomovil_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
