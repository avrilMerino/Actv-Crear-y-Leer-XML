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
        }

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

        private void btnLeerXML_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
