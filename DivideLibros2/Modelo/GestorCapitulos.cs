using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DivideLibros2.Modelo
{
   static class GestorCapitulos
    {


        #region Algoritmo 1

        static public List<Capitulo> obtenerCapitulosAlgoritmo1(List<string> lineasLibro)
        {
            List<Capitulo> capitulos = new List<Capitulo>();

            buscarPrologo(lineasLibro, capitulos);

            for (int i = 0; i < lineasLibro.Count; i++)
            {
                int capi;
                if (int.TryParse(lineasLibro[i], out capi))
                {
                    if (capitulos.Count != 0) capitulos.Last().lineaFin = i - 1;
                    if (capi < 10) capitulos.Add(new Capitulo { nombre = "0" + lineasLibro[i], lineaInicio = i });
                    else capitulos.Add(new Capitulo { nombre = lineasLibro[i], lineaInicio = i });
                }
            }

            buscarEpilogo(lineasLibro, capitulos);

            return capitulos;
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
            if(capitulos.Count !=0)
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
