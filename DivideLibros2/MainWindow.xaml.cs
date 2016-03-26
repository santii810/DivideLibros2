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
        public List<string> lineasLibro = new List<string>();
        public List<Capitulo> capitulos = new List<Capitulo>();
        GestorFicheros gestor = new GestorFicheros();
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
                        Thread buscarCapitulos = new Thread(delegate () { GestorCapitulos.obtenerCapitulosAlgoritmo1(this); });
                        GestorCapitulos.obtenerCapitulosAlgoritmo1(this);
                        break;
                }




                //Añade capitulos
                panelCapitulos.Children.Clear();
                foreach (Capitulo item in capitulos)
                {
                    panelCapitulos.Children.Add(addCapituloALista(item));
                }
            }
        }

        private static StackPanel addCapituloALista(Capitulo capitulo)
        {

            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Orientation = Orientation.Horizontal;
            tmpPanel.HorizontalAlignment = HorizontalAlignment.Center;


            Label tmpLabel = new Label();
            tmpLabel.Content = capitulo.nombre;
            tmpLabel.Style = (Style)Application.Current.Resources["Label2"];
            tmpLabel.Width = 50;
            tmpPanel.Children.Add(tmpLabel);

            Label tmpLabelInicio = new Label();
            tmpLabelInicio.Content = capitulo.lineaInicio.ToString();
            tmpLabelInicio.Style = (Style)Application.Current.Resources["Label2"];
            tmpLabelInicio.Width = 50;

            tmpPanel.Children.Add(tmpLabelInicio);


            Label tmpLabelFin = new Label();
            tmpLabelFin.Content = capitulo.lineaFin.ToString();
            tmpLabelFin.Style = (Style)Application.Current.Resources["Label2"];
            tmpLabelFin.Width = 50;

            tmpPanel.Children.Add(tmpLabelFin);



            Button tmpButon = new Button();
            tmpButon.Content = "Leer";
            tmpButon.Style = (Style)Application.Current.Resources["Button"];
            tmpButon.Click += delegate { leerCapitulo(capitulo); };
            tmpPanel.Children.Add(tmpButon);

            return tmpPanel;

        }

        private static void leerCapitulo(Capitulo capitulo)
        {
            throw new NotImplementedException();
        }

        private void Save_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Directory.CreateDirectory(fichero.Directory + @"\capitulos_" + fichero.Name);
            string directorio = fichero.Directory + @"\capitulos_" + fichero.Name + @"\";
            string prefijo = textBoxNombre.Text;
            foreach (Capitulo item in capitulos)
            {
                gestor.agregar(directorio + prefijo + "_" + item.nombre + ".txt", lineasLibro.GetRange(item.lineaInicio, (item.lineaFin - item.lineaInicio)));
            }
            MessageBox.Show("Ficheros generados");
        }
    }
}
