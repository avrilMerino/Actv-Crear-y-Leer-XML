using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace Actividad_crear_y_leer_xml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Enganchar eventos por si no están en el diseñador
            this.btnLeerXML.Click += btnLeerXML_Click;
            this.flechaizq.Click += flechaizq_Click;
            this.fechaderech.Click += flechaderech_Click;
        }
        // Campos para navegación
        private List<XmlElement> _registros = new List<XmlElement>();
        private int _pos = -1;

        //CREAR XML
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                //Crear el documento XML
                XmlDocument doc = new XmlDocument();

                //Crear el elemento raiz
                String cad1 = this.tb1.Text;
                XmlElement raiz = doc.CreateElement(cad1);
                doc.AppendChild(raiz);

                //crear el primer elemento hijo
                String cad2 = this.tb2.Text;

                XmlElement hijo2 = doc.CreateElement(cad2);
                raiz.AppendChild(hijo2);


                //crear el segundo elemento hijo
                String cad3 = this.tb3.Text;

                XmlElement hijo3 = doc.CreateElement(cad3);
                hijo2.AppendChild(hijo3);

                //crear el tercer elemento hijo
                String cad4 = this.tb4.Text;
                XmlElement hijo4 = doc.CreateElement(cad4);
                hijo3.AppendChild(hijo4);

                //crear el cuarto elemento hijo
                String cad5 = this.tb5.Text;
                XmlElement hijo5 = doc.CreateElement(cad5);
                hijo4.AppendChild(hijo5);


                //Guardar el documento XML en el escritorio del usuario
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string ruta = Path.Combine(escritorio, "datos.xml");

                doc.Save(ruta);


                //Guardar en el Escritorio del usuario

                MessageBox.Show($"XML guardado en el escritorio:\n{ruta}");
                MessageBox.Show($"XML guardado en:\n{ruta}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el XML: {ex.Message}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //LEER XML
        private void btnLeerXML_Click(object sender, EventArgs e)
        {
            try
            {
               //ruta:
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string ruta = Path.Combine(escritorio, "datos.xml");

                if (!File.Exists(ruta))
                {
                    MessageBox.Show("No existe datos.xml en el Escritorio.");
                    return;
                }
                //Abrir y cargar el XML
                var doc = new XmlDocument();
                doc.Load(ruta);

                var raiz = doc.DocumentElement;
                if (raiz == null)
                {
                    MessageBox.Show("El XML no tiene elemento raíz.");
                    return;
                }

                // a TODOS los elementos del documento en recorrido preorden
                // Así podrás moverte con las flechas por cada nodo
                _registros = raiz.SelectNodes(".//*/self::*")
                                 .OfType<XmlElement>()
                                 .Prepend(raiz) // incluye también la raíz
                                 .ToList();

                if (_registros.Count == 0)
                {
                    MessageBox.Show("No se encontraron elementos que mostrar.");
                    return;
                }

                _pos = 0;
                PintarRegistroActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el XML: {ex.Message}");
            }
        }
        private void PintarRegistroActual()
        {
            //si le dan a la izq mucho no hacemos nada
            if (_pos < 0 || _pos >= _registros.Count) return;

            // Limpia
            tb1.Clear(); tb2.Clear(); tb3.Clear(); tb4.Clear(); tb5.Clear();

            //Tomamos el nodo actual
            var actual = _registros[_pos];

            // Queremos rellenar tb1..tb5 con la "cadena" desde el actual hacia abajo
            // siguiendo el primer hijo elemento en cada nivel (si existe)
            var cajas = new[] { tb1, tb2, tb3, tb4, tb5 };
            var nodo = actual;

            for (int i = 0; i < cajas.Length && nodo != null; i++)
            {
                //Si el nodo tiene texto útil, lo mostramos; si no, mostramos su nombre
                var valor = nodo.InnerText?.Trim();
                if (string.IsNullOrEmpty(valor))
                    cajas[i].Text = nodo.Name;     //solo nombre
                else
                    cajas[i].Text = valor;         //si alguna vez metes texto, se verá

                //bajar al primer hijo que sea elemento
                nodo = nodo.ChildNodes
                           .OfType<XmlElement>()
                           .FirstOrDefault();
            }
        }


        private void flechaizq_Click(object sender, EventArgs e)
        {
            if (_registros == null || _registros.Count == 0) return;
            if (_pos > 0)
            {
                _pos--;
                PintarRegistroActual();
            }
        }

        private void flechaderech_Click(object sender, EventArgs e)
        {
            if (_registros == null || _registros.Count == 0) return;
            if (_pos < _registros.Count - 1)
            {
                _pos++;
                PintarRegistroActual();
            }
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
