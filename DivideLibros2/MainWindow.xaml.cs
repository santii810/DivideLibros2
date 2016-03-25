using DivideLibros2.Modelo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DivideLibros2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        FileInfo fichero;
        List<string> lineasLibro = new List<string>();
        GestorFicheros gestor = new GestorFicheros();
        List<Capitulo> capitulos = new List<Capitulo>();


        public MainWindow()
        {
            InitializeComponent();
        }


        private void reset()
        {

            this.labelLibro.Content = "";
            fichero = null;
            lineasLibro.Clear();
            capitulos.Clear();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            reset();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (!String.IsNullOrEmpty(ofd.FileName))
            {
                fichero = new FileInfo(ofd.FileName);
                this.labelLibro.Content = fichero.Name;
                gestor.nombreFichero = fichero.FullName;
                //leer todas las lineas del fichero
                lineasLibro = gestor.leerLineas();

            }
        }
    }
}
