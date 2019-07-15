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

namespace Proyecto_Negocios_IIP
{
    /// <summary>
    /// Lógica de interacción para RegistroSalida.xaml
    /// </summary>
    public partial class RegistroSalida : Window
    {
        public RegistroSalida()
        {
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
