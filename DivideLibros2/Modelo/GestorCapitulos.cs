using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DivideLibros2.Modelo
{
    static class GestorCapitulos
    {


        #region Algoritmo 1

        internal static void obtenerCapitulosAlgoritmo1(MainWindow mainWindow)
        {
            mainWindow.capitulos = new List<Capitulo>();

            buscarPrologo(mainWindow.lineasLibro, mainWindow.capitulos);



            for (int i = 0; i < mainWindow.lineasLibro.Count; i++)
            {
                int capi;
                if (int.TryParse(mainWindow.lineasLibro[i], out capi))
                {
                    if (mainWindow.capitulos.Count != 0) mainWindow.capitulos.Last().lineaFin = i - 1;
                    if (capi < 10) mainWindow.capitulos.Add(new Capitulo { nombre = "0" + mainWindow.lineasLibro[i], lineaInicio = i });
                    else mainWindow.capitulos.Add(new Capitulo { nombre = mainWindow.lineasLibro[i], lineaInicio = i });
                }
            }

            buscarEpilogo(mainWindow.lineasLibro, mainWindow.capitulos);

            foreach (Capitulo item in mainWindow.capitulos)
            {
                mainWindow.panelCapitulos.Children.Add(addCapituloALista(item));
            }
        }

        private static Label addCapituloALista(Capitulo capitulo)
        {
            Label tmp = new Label();

            tmp.Content = capitulo.nombre;
            tmp.Style = (Style)Application.Current.Resources["Label2"];

            return tmp;

        }


        private static void buscarPrologo(List<string> lineasLibro, List<Capitulo> retorno)
        {
            //Buscar prologo
            for (int i = 0; i < lineasLibro.Count; i++)
            {
                if (prepareToCompareString(lineasLibro[i]).Equals("PROLOGO"))
                {
                    lineasLibro.RemoveRange(0, i - 1);
                    retorno.Add(new Capitulo { nombre = "00_PROLOGO", lineaInicio = 0 });

                }
            }
        }

        private static void buscarEpilogo(List<string> lineasLibro, List<Capitulo> capitulos)
        {
            if (capitulos.Count != 0)
                for (int i = capitulos.Last().lineaInicio; i < lineasLibro.Count; i++)
                {
                    if (prepareToCompareString(lineasLibro[i]).Equals("EPILOGO"))
                    {
                        if (capitulos.Count != 0) capitulos.Last().lineaFin = i - 1;
                        capitulos.Add(new Capitulo { nombre = capitulos.Count + "_EPILOGO", lineaInicio = i, lineaFin = lineasLibro.Count });

                    }
                }
        }

        #endregion


        #region Algoritmo2

        #endregion

        #region Algoritmo3

        #endregion

        #region Algoritmo4

        #endregion

        #region Algoritmo5

        #endregion







        static private string prepareToCompareString(string s)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            s = replace_a_Accents.Replace(s, "a");
            s = replace_e_Accents.Replace(s, "e");
            s = replace_i_Accents.Replace(s, "i");
            s = replace_o_Accents.Replace(s, "o");
            s = replace_u_Accents.Replace(s, "u");
            s = s.ToUpper().Replace(" ", "");
            return s;
        }


    }
}
