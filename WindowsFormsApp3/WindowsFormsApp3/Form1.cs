using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        #region PROPIEDADES
        Alumno[] alumnos = new Alumno[5];
        int contador = 0;
        Boolean estaAcomodado = false;
        #endregion
        public Form1()
        {
            InitializeComponent();

            //Estilo para que no se pueda modificar los datos de la lista de alumno

            ComboBoxMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #region METODOS

        //Método para agregar un nuevo alumno a la lista de alumnos

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            //Validaciones

            if (String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtAppelido.Text) || String.IsNullOrEmpty(txtNota.Text) || String.IsNullOrEmpty(ComboBoxMaterias.Text) )
            {
                MessageBox.Show("Por favor completar todos los datos");
                return;
            }
            int number;
            if (int.TryParse(txtNota.Text, out number) && number <= 10 && number >= 0)
            {
                //Agrega un nuevo cupo al arreglo

                Alumno nuevo = new Alumno();
                if (contador == alumnos.Length)
                {
                    Alumno[] temporal = new Alumno[contador + 1];
                    Array.Copy(alumnos, temporal, alumnos.Length);
                    alumnos = temporal;
                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Apellido = txtAppelido.Text;
                    nuevo.Nota = int.Parse(txtNota.Text);
                    nuevo.Materias = ComboBoxMaterias.SelectedItem.ToString();

                    //Validaciones

                    if (nuevo.ComparasiHayNumero(nuevo.Nombre))
                    {
                        MessageBox.Show("El nombre no puede contener numeros");
                        txtNombre.Text = "";
                        return;
                    }
                    if (nuevo.ComparasiHayNumero(nuevo.Apellido))
                    {
                        MessageBox.Show("El apellido no puede contener numeros");
                        txtAppelido.Text = "";
                        return;
                    }
                    if (nuevo.ComparasiHayNumero(nuevo.Materias))
                    {
                        MessageBox.Show("La materia no puede contener numeros");
                        ComboBoxMaterias.Text = "";
                        return;
                    }

                    //Agrega un nuevo alumno

                    alumnos[contador] = nuevo;
                    dataGridLista.Rows.Add(alumnos[contador].Nombre, alumnos[contador].Apellido, alumnos[contador].Materias, alumnos[contador].Nota);
                    contador++;
                    txtNombre.Text = "";
                    txtAppelido.Text = "";
                    txtNota.Text = "";
                    ComboBoxMaterias.SelectedIndex = -1;
                }
                else
                {
                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Apellido = txtAppelido.Text;
                    nuevo.Nota = int.Parse(txtNota.Text);
                    nuevo.Materias = ComboBoxMaterias.SelectedItem.ToString();

                    //Validaciones

                    if (nuevo.ComparasiHayNumero(nuevo.Nombre))
                    {
                        MessageBox.Show("El nombre no puede contener numeros");
                        txtNombre.Text = "";
                        return;
                    }
                    if (nuevo.ComparasiHayNumero(nuevo.Apellido))
                    {
                        MessageBox.Show("El apellido no puede contener numeros");
                        txtAppelido.Text = "";
                        return;
                    }
                    if (nuevo.ComparasiHayNumero(nuevo.Materias))
                    {
                        MessageBox.Show("La materia no puede contener numeros");
                        ComboBoxMaterias.Text = "";
                        return;
                    }

                    //Agrega un nuevo alumno

                    alumnos[contador] = nuevo;
                    dataGridLista.Rows.Add(alumnos[contador].Nombre, alumnos[contador].Apellido, alumnos[contador].Materias, alumnos[contador].Nota);
                    contador++;
                    txtNombre.Text = "";
                    txtAppelido.Text = "";
                    txtNota.Text = "";
                    ComboBoxMaterias.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("La nota ingresada no es correcta");
                txtNota.Text = "";
            }
        }

        //Método para buscar el alumno en la pocision de la lista

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Boolean bandera = false;

            //Validaciones

            if (String.IsNullOrEmpty(txtBuscar.Text))
            {
                MessageBox.Show("Por favor ingrese un nombre");
                txtBuscar.Text = "";
                return;
            }

            //Busca en la lista original

            for (int j = 0; j < dataGridLista.Rows.Count; j++)
            {
                if (dataGridLista[0, j].Value.ToString().Equals(txtBuscar.Text))
                {
                    MessageBox.Show("Lista Original: El nombre " + dataGridLista[0, j].Value.ToString() + " se encuentra en la pocision: " + (j + 1));
                    bandera = true;

                    //Busca en la lista ordenada

                    if (estaAcomodado == true)
                    {
                        for (int i = 0; i < contador; i++)
                        {
                            if (alumnos[i].Nombre.Equals(txtBuscar.Text))
                            {
                                MessageBox.Show("Lista Ordenada: El nombre " + alumnos[i].Nombre + " se encuentra en la pocision: " + (i + 1));
                                txtBuscar.Text = "";
                                return;
                            }
                        }
                    }

                }
            }
            txtBuscar.Text = "";
            if (bandera == false)
            {
                txtBuscar.Text = "";
                MessageBox.Show("Nombre incorrecto");
            }

        }

        //Método para eliminar el alumno ingresado por parametro

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            //Validaciones

            if (String.IsNullOrEmpty(txtBuscar.Text))
            {
                MessageBox.Show("Por favor ingrese un nombre");
                txtBuscar.Text = "";
                return;
            }

            //Elimina el alumnos de las listas

            for (int i = 0; i < contador; i++)
            {
                string compara = alumnos[i].Nombre;
                if (compara.Equals(txtBuscar.Text))
                {
                    //recorre grid 1
                    for (int p = 0; p < dataGridLista.RowCount; p++)
                    {
                        if (dataGridLista[0, p].Value.Equals(txtBuscar.Text))
                        {
                            dataGridLista.Rows.RemoveAt(p);
                        }
                    }
                    //recorre grid 2
                    for (int k = 0; k < dataGridAcomoda.RowCount; k++)
                    {
                        if (dataGridAcomoda[0, k].Value.Equals(txtBuscar.Text))
                        {
                            dataGridAcomoda.Rows.RemoveAt(k);
                        }
                    }
                    //Elimina al alumno
                    alumnos[i] = null;
                    for (int j = i; j < alumnos.Length - 1; j++)
                    {
                        alumnos[j] = alumnos[j + 1];
                    }
                    Alumno[] temporal = new Alumno[alumnos.Length - 1];
                    Array.Copy(alumnos, temporal, alumnos.Length - 1);
                    alumnos = temporal;
                    contador--;
                    txtBuscar.Text = "";
                    return;
                }
            }
            txtBuscar.Text = "";
            MessageBox.Show("Nombre incorrecto");
        }

        //Ordena los alumnos por nombre

        private void BtnOrdenNombre_Click(object sender, EventArgs e)
        {
            estaAcomodado = true;
            string aux4;
            string aux2;
            string aux1;
            int aux3;
            for (int a = 1; a < contador; a++)
                for (int b = contador - 1; b >= a; b--)
                {
                    aux2 = alumnos[b - 1].Nombre;
                    int aux = aux2.CompareTo(alumnos[b].Nombre);
                    if (aux > 0)
                    {
                        aux4 = alumnos[b - 1].Materias;
                        aux1 = alumnos[b - 1].Apellido;
                        aux2 = alumnos[b - 1].Nombre;
                        aux3 = alumnos[b - 1].Nota;
                        alumnos[b - 1].Materias = alumnos[b].Materias;
                        alumnos[b - 1].Nota = alumnos[b].Nota;
                        alumnos[b - 1].Apellido = alumnos[b].Apellido;
                        alumnos[b - 1].Nombre = alumnos[b].Nombre;
                        alumnos[b].Materias = aux4;
                        alumnos[b].Nota = aux3;
                        alumnos[b].Apellido = aux1;
                        alumnos[b].Nombre = aux2;
                    }
                }
            dataGridAcomoda.Rows.Clear();
            for (int i = 0; i < contador; i++)
            {
                dataGridAcomoda.Rows.Add(alumnos[i].Nombre, alumnos[i].Apellido, alumnos[i].Materias, alumnos[i].Nota);
            }
        }

        //Ordena los alumnos por apellido

        private void BtnOrdenApellido_Click(object sender, EventArgs e)
        {
            estaAcomodado = true;
            string aux4;
            string aux2;
            string aux1;
            int aux3;
            for (int a = 1; a < contador; a++)
                for (int b = contador - 1; b >= a; b--)
                {
                    aux1 = alumnos[b - 1].Apellido;
                    int aux = aux1.CompareTo(alumnos[b].Apellido);
                    if (aux > 0)
                    {
                        aux4 = alumnos[b - 1].Materias;
                        aux1 = alumnos[b - 1].Apellido;
                        aux2 = alumnos[b - 1].Nombre;
                        aux3 = alumnos[b - 1].Nota;
                        alumnos[b - 1].Materias = alumnos[b].Materias;
                        alumnos[b - 1].Nota = alumnos[b].Nota;
                        alumnos[b - 1].Apellido = alumnos[b].Apellido;
                        alumnos[b - 1].Nombre = alumnos[b].Nombre;
                        alumnos[b].Materias = aux4;
                        alumnos[b].Nota = aux3;
                        alumnos[b].Apellido = aux1;
                        alumnos[b].Nombre = aux2;
                    }
                }
            dataGridAcomoda.Rows.Clear();
            for (int i = 0; i < contador; i++)
            {
                dataGridAcomoda.Rows.Add(alumnos[i].Nombre, alumnos[i].Apellido, alumnos[i].Materias, alumnos[i].Nota);
            }
        }

        //Ordena los alumnos por nota

        private void BtnOrdenNota_Click(object sender, EventArgs e)
        {
            estaAcomodado = true;
            string aux4;
            string aux2;
            string aux1;
            int aux3;
            for (int a = 1; a < contador; a++)
                for (int b = contador - 1; b >= a; b--)
                {
                    aux3 = alumnos[b - 1].Nota;
                    int aux = aux3.CompareTo(alumnos[b].Nota);
                    if (aux < 0)
                    {
                        aux4 = alumnos[b - 1].Materias;
                        aux1 = alumnos[b - 1].Apellido;
                        aux2 = alumnos[b - 1].Nombre;
                        aux3 = alumnos[b - 1].Nota;
                        alumnos[b - 1].Materias = alumnos[b].Materias;
                        alumnos[b - 1].Nota = alumnos[b].Nota;
                        alumnos[b - 1].Apellido = alumnos[b].Apellido;
                        alumnos[b - 1].Nombre = alumnos[b].Nombre;
                        alumnos[b].Materias = aux4;
                        alumnos[b].Nota = aux3;
                        alumnos[b].Apellido = aux1;
                        alumnos[b].Nombre = aux2;
                    }
                }
            dataGridAcomoda.Rows.Clear();
            for (int i = 0; i < contador; i++)
            {
                dataGridAcomoda.Rows.Add(alumnos[i].Nombre, alumnos[i].Apellido, alumnos[i].Materias, alumnos[i].Nota);
            }
        }

        //Ordena los alumnos por materia

        private void BtnOrdenMateria_Click(object sender, EventArgs e)
        {
            estaAcomodado = true;
            string aux4;
            string aux2;
            string aux1;
            int aux3;
            for (int a = 1; a < contador; a++)
                for (int b = contador - 1; b >= a; b--)
                {
                    aux4 = alumnos[b - 1].Materias;
                    int aux = aux4.CompareTo(alumnos[b].Materias);
                    if (aux > 0)
                    {
                        aux4 = alumnos[b - 1].Materias;
                        aux1 = alumnos[b - 1].Apellido;
                        aux2 = alumnos[b - 1].Nombre;
                        aux3 = alumnos[b - 1].Nota;
                        alumnos[b - 1].Materias = alumnos[b].Materias;
                        alumnos[b - 1].Nota = alumnos[b].Nota;
                        alumnos[b - 1].Apellido = alumnos[b].Apellido;
                        alumnos[b - 1].Nombre = alumnos[b].Nombre;
                        alumnos[b].Materias = aux4;
                        alumnos[b].Nota = aux3;
                        alumnos[b].Apellido = aux1;
                        alumnos[b].Nombre = aux2;
                    }
                }
            dataGridAcomoda.Rows.Clear();
            for (int i = 0; i < contador; i++)
            {
                dataGridAcomoda.Rows.Add(alumnos[i].Nombre, alumnos[i].Apellido, alumnos[i].Materias, alumnos[i].Nota);
            }

        }

        //Agrega una materia a la lista de materias

        private void BtnAgregarMateria_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMateria.Text))
            {
                MessageBox.Show("Por favor introducir una materia");
            }
            else
            {
                if (ComparasiHayNumero(txtMateria.Text))
                {
                    MessageBox.Show("La materia no puede contener numeros");
                    txtMateria.Text = "";
                    return;
                }
                for (int i = 0; i < ComboBoxMaterias.Items.Count; i++)
                {
                    if (ComboBoxMaterias.Items.Contains(txtMateria.Text))
                    {
                        MessageBox.Show("La materia ya está en la lista");
                        txtMateria.Text = "";
                        return;
                    }
                }
                ComboBoxMaterias.Items.Add(txtMateria.Text);
                MessageBox.Show("Materia agregada");
                txtMateria.Text = "";
            }
        }

        //Verifica si hay numero en el texto
        
        private Boolean ComparasiHayNumero(String text)
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
