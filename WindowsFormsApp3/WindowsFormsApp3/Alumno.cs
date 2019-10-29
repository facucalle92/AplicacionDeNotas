using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Alumno
    {
        #region PROPIEDADES
        private String nombre;
        private String apellido;
        private int nota;
        private String materias;
        #endregion

        #region METODOS

        //Metodos get set para las propiedades

        public String Materias
        {
            get
            {
                return materias;
            }
            set
            {
                materias = value;
            }
        }
        public String Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public String Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                apellido = value;
            }
        }
        public int Nota
        {
            get
            {
                return nota;
            }
            set
            {
                nota = value;
            }
        }

        //Metodo para verificar si el texto contiene numeros
        //retorna true si encuentra numeros y false sino encuentra

        public Boolean ComparasiHayNumero(String text)
        {
            Boolean haynumero = false;
            string[] arreglo = text.ToUpper().Trim().Split();
            foreach (String aux in arreglo)
            {
                for (int i = 0; i < aux.Length; i++)
                {
                    int number1;
                    if (int.TryParse(aux.Substring(i, 1), out number1))
                    {
                        haynumero = true;
                        return haynumero;
                    }
                }

            }
            return haynumero;
        }
        #endregion
    }
}
