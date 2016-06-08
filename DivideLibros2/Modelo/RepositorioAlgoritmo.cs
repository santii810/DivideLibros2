using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivideLibros2.Modelo
{
    static public class RepositorioAlgoritmo
    {
        static public List<Algoritmo> getAlgoritmos()
        {
            List<Algoritmo> retorno = new List<Algoritmo>();
            retorno.Add(new Algoritmo
            {
                id = 1,
                nombre = "PROLOGO + Solo numeros",
                descripcion = "Esta detecta el progolo y el epilogo si estan situados en el inicio y el final respectivamente. " +
                "Por otro lado detectara como capitulos las lineas que tengan solamente un  numero." +
                "Las lineas anteriores a la deteccion del prologo quedaran descartadas."
            });

            retorno.Add(new Algoritmo
            {
                id = 2,
                nombre = "__ numero __",
                descripcion =
                "Detecta como capitulos las lineas que tengan un numero entre varios '_' con espacios separando." +
                "Las lineas anteriores a la deteccion del capitulo quedaran descartadas."
            });

            retorno.Add(new Algoritmo
            {
                id = 3,
                nombre = "Pregunta"
            });


            return retorno;


        }

    }
}
