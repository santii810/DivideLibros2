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



        internal static void obtenerCapitulosAlgoritmo1(MainWindow mainWindow)
        {
            mainWindow.capitulos = new List<Capitulo>();

            buscarPrologo(mainWindow.lineasLibro, mainWindow.capitulos);
            for (int i = 0; i < mainWindow.lineasLibro.Count; i++)
            {
                string linea = mainWindow.lineasLibro[i].Trim();
                if (linea.Length < 30)
                {
                    int capi;
                    if (int.TryParse(linea, out capi))
                        nuevoCapitulo(mainWindow, i, capi);
                }
            }
            buscarEpilogo(mainWindow.lineasLibro, mainWindow.capitulos);
        }



        public static void obtenerCapitulosAlgoritmo2(MainWindow mainWindow)
        {
            for (int i = 0; i < mainWindow.lineasLibro.Count; i++)
            {
                int capi;
                string linea = mainWindow.lineasLibro[i].Trim();
                if (linea.Length < 30)
                {
                    string[] splitLinea = mainWindow.lineasLibro[i].Split(' ');
                    if (splitLinea.Length == 3 && int.TryParse(splitLinea[1], out capi))
                    {
                        nuevoCapitulo(mainWindow, i, capi);
                    }
                }
            }
            mainWindow.capitulos.Last().lineaFin = mainWindow.lineasLibro.Count;
        }



        internal static void obtenerCapitulosAlgoritmo3(MainWindow mainWindow)
        {
            mainWindow.capitulos = new List<Capitulo>();

            int capi = 0;
            for (int i = 0; i < mainWindow.lineasLibro.Count; i++)
            {
                string linea = mainWindow.lineasLibro[i].Trim();
                if (linea.Length < 30 && linea.ToUpper() == linea && linea != "")
                {
                    if (capi < 10) mainWindow.lineasLibro[i] =  "0" + capi.ToString() +" "+ mainWindow.lineasLibro[i];
                    else mainWindow.lineasLibro[i] = capi.ToString() + " " + mainWindow.lineasLibro[i];
                    nuevoCapitulo(mainWindow, i, capi++);
                }
            }
            mainWindow.capitulos.Last().lineaFin = mainWindow.lineasLibro.Count;

        }



        private static void buscarPrologo(List<string> lineasLibro, List<Capitulo> retorno)
        {
            //Buscar prologo
            for (int i = 0; i < lineasLibro.Count; i++)
            {
                if (prepareToCompareString(lineasLibro[i]).Equals("PROLOGO"))
                {
                    if (i != 0)
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

        private static void nuevoCapitulo(MainWindow mainWindow, int i, int capi)
        {
            if (mainWindow.capitulos.Count != 0) mainWindow.capitulos.Last().lineaFin = i - 1;
            if (capi < 10) mainWindow.capitulos.Add(new Capitulo { nombre =  mainWindow.lineasLibro[i], lineaInicio = i });
            else mainWindow.capitulos.Add(new Capitulo { nombre = mainWindow.lineasLibro[i], lineaInicio = i });
        }

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
