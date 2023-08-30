using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using Microsoft.SqlServer.Server;
using negocio;

namespace Discos_DB
{
    public partial class frmAltaDisco : Form
    {
        private Disco disco = null; //atributo privado Disco

        //constructor vacio 
        public frmAltaDisco()
        {
            InitializeComponent();
            
        }

        //constructor con parametros
        public frmAltaDisco(Disco disco)
        {
            InitializeComponent();
            this.disco = disco;
            Text= "Modificar Disco";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
          
            DiscoNegocio discoNegocio = new DiscoNegocio();

            try
            {
                if (disco == null)
                
                    disco = new Disco();

                    disco.Titulo = txtTitulo.Text;
                    disco.Fecha = dtpFecha.Value;
                    disco.CantidadCanciones = int.Parse(txtCanciones.Text);
                    disco.UrlTapa = txtUrl.Text;
                    disco.TipoEsti = (Estilo)cbestilos.SelectedItem;
                    disco.TipoEdi = (TipoEdicion)cbEdicion.SelectedItem;

                    if (disco.Id != 0)
                    {
                    discoNegocio.modificar(disco);
                    MessageBox.Show("MODIFICADO EXITOSAMENTE");
                    }
                    else
                    {
                        discoNegocio.agregar(disco);
                        MessageBox.Show("AGREGADO EXITOSAMENTE");
                    }
                    

                    


                Close();
                

                    
                    
                



            }
            catch (Exception)
            {

                MessageBox.Show("ERROR, VERIFIQUE LOS DATOS");
                
            }
        }

        private void frmAltaDisco_Load(object sender, EventArgs e)
        {
            DiscoNegocio discoNegocio = new DiscoNegocio();



            try
            {

                cbestilos.DataSource = discoNegocio.listarEstilos();
                cbestilos.ValueMember = "Id";
                cbestilos.DisplayMember = "Descripcion";

                cbEdicion.DataSource = discoNegocio.listarEdicion();
                cbEdicion.ValueMember = "Id";
                cbEdicion.DisplayMember = "Descripcion";


                if (disco != null)
                {
                    txtTitulo.Text = disco.Titulo;
                    dtpFecha.Value = disco.Fecha;
                    txtCanciones.Text = disco.CantidadCanciones.ToString();
                    txtUrl.Text = disco.UrlTapa;
                    cargarImagen(disco.UrlTapa);
                    cbestilos.SelectedValue = disco.TipoEsti.Id;
                    cbEdicion.SelectedValue = disco.TipoEdi.Id;
                    
                    
                };



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            
        }

        public void limpiar()
        {
            txtTitulo.Clear();
            txtCanciones.Clear();
            txtUrl.Clear();
            cbestilos.SelectedIndex = 0;
            cbEdicion.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;

        }

        private void txtUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrl.Text);
        }




        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxImagen.Load("https://www.globalsign.com/application/files/9516/0389/3750/What_Is_an_SSL_Common_Name_Mismatch_Error_-_Blog_Image.jpg");
            }
        }
    }
}
