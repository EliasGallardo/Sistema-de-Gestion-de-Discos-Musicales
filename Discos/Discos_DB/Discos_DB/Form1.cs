using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;
using System.Drawing.Text;

namespace Discos_DB
{
    public partial class Form1 : Form
    {
        private List<Disco> listaDisco;

        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            cargarGrilla();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaDisco altaDisco = new frmAltaDisco();
            altaDisco.ShowDialog();
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            //invoco al metodo listar
            DiscoNegocio negocio = new DiscoNegocio();

            listaDisco = negocio.Listar();
            GRILLA.DataSource = listaDisco;
            // GRILLA.DataSource = negocio.Listar();
            
            GRILLA.Columns[4].Visible = false; //oculto la columna url tapa
            

        }

        private void GRILLA_SelectionChanged(object sender, EventArgs e)
        {
            Disco seleccionado = (Disco)GRILLA.CurrentRow.DataBoundItem;

            try
            {

                pbxImagen.Load(seleccionado.UrlTapa);
            }
            catch (Exception)
            {

                pbxImagen.Load("https://www.globalsign.com/application/files/9516/0389/3750/What_Is_an_SSL_Common_Name_Mismatch_Error_-_Blog_Image.jpg");
            }
            

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            

            if (GRILLA.Rows.Count > 0)
            {
                Disco seleccionado;
                seleccionado = (Disco)GRILLA.CurrentRow.DataBoundItem;

                frmAltaDisco ModificarDisco = new frmAltaDisco(seleccionado);
                ModificarDisco.ShowDialog();
                cargarGrilla();
            }
            else
            {
                MessageBox.Show("NO HAY DATOS SELECCIONADOS");
            }
            

           

        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {

            if (GRILLA.Rows.Count > 0)
            {
                DiscoNegocio negocio = new DiscoNegocio();
                Disco seleccionado;
                seleccionado = (Disco)GRILLA.CurrentRow.DataBoundItem;

                try
                {
                    DialogResult respuesta = MessageBox.Show("¿ DE VERDAD QUERES ELIMINARLO ?", "ELIMINANDO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.Yes)
                    {
                        negocio.eliminar(seleccionado);
                        cargarGrilla();
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                
            }



           
        }

        private void btnEliminarLogica_Click(object sender, EventArgs e)
        {
            if (GRILLA.Rows.Count > 0)
            {
                DiscoNegocio negocio = new DiscoNegocio();
                Disco seleccionado;
                seleccionado = (Disco)GRILLA.CurrentRow.DataBoundItem;

                try
                {
                    DialogResult respuesta = MessageBox.Show("¿ DE VERDAD QUERES ELIMINARLO ?", "ELIMINANDO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respuesta == DialogResult.Yes)
                    {
                        negocio.eliminarLogico(seleccionado.Id);
                        cargarGrilla();
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            else
            {
                MessageBox.Show("NO HAY DATOS SELECCIONADOS");
            }

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            

           
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Disco> listaFiltrada;
            string filtro = txtFiltro.Text;


            if (filtro.Length >= 3)
            {

                listaFiltrada = listaDisco.FindAll(x => x.Titulo.ToUpper().Contains(filtro.ToUpper()));

            }
            else
            {
                listaFiltrada = listaDisco;
            }



            GRILLA.DataSource = null;
            GRILLA.DataSource = listaFiltrada;
            GRILLA.Columns[4].Visible = false;
        }

    }
}
