using DivideLibros2.Modelo;
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

namespace DivideLibros2
{
    /// <summary>
    /// Lógica de interacción para Leer.xaml
    /// </summary>
    public partial class Leer : Window
    {
        public Leer(Capitulo capitulo, MainWindow mainWindow)
        {
            InitializeComponent();
            for (int i = capitulo.lineaInicio; i <= capitulo.lineaFin; i++)
                panelLineas.Children.Add(addLabel(mainWindow.lineasLibro[i]));
        }

        private Label addLabel(string v)
        {
            Label retorno = new Label();
            retorno.Content = v;
            retorno.FontFamily = new FontFamily("Tempus Sans ITC");

            return retorno;
        }
    }
}
