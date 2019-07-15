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
    /// Lógica de interacción para TipoDeAutomovil.xaml
    /// </summary>
    public partial class TipoDeAutomovil : Window
    {
        public TipoDeAutomovil()
        {
            InitializeComponent();
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
        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Usted esta volviendo");

            menu.Show();
            this.Close();


        }
        

    }
}
