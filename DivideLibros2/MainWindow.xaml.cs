using DivideLibros2.Modelo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        List<Algoritmo> algoritmos = new List<Algoritmo>();
        Algoritmo algoritmo = new Algoritmo();



        public MainWindow()
        {
            InitializeComponent();
            algoritmos = RepositorioAlgoritmo.getAlgoritmos();
            foreach (Algoritmo item in algoritmos)
            {
                comboBoxSeleccionarAlgoritmo.Items.Add(item.nombre);
            }
        }


        private void reset()
        {

            this.labelLibro.Content = "";
            fichero = null;
            lineasLibro.Clear();
            capitulos.Clear();
        }



        private void leerLineas()
        {
            lineasLibro = gestor.leerLineas();
        }

        private void ButtonSeleccionarLibro_Click(object sender, RoutedEventArgs e)
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
                Thread t = new Thread(leerLineas);
                t.Start();
            }
        }

        private void comboBoxSeleccionarAlgoritmo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Rellena el tipo de deteccion
            foreach (Algoritmo item in algoritmos)
            {
                if (item.nombre.Equals(comboBoxSeleccionarAlgoritmo.SelectedItem.ToString()))
                {
                    algoritmo = item;
                }
            }
        }

        private void ButtonDetectar_Click(object sender, RoutedEventArgs e)
        {
            if (labelLibro.Content.ToString() != "")
            {
                switch (algoritmo.id)
                {
                    case 1:
                        //PROLOGO + Solo numeros
                        capitulos = GestorCapitulos.obtenerCapitulosAlgoritmo1(lineasLibro);
                        break;



                }

            }
        }
    }
}
