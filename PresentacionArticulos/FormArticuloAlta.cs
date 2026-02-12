using DominioArticulos;
using NegocioArticulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionArticulos
{
    public partial class FormArticuloAlta : Form
    {
        Articulo articulo = null;

        public FormArticuloAlta()
        {
            InitializeComponent();
            Text = "Alta Articulo";
        }

        // Constructor pensado para el pasaje del articulo, para poder modificarlo con los campos ya cargados.
        public FormArticuloAlta(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Pokemon";
        }

        private void FormArticuloAlta_Load(object sender, EventArgs e)
        {
            MarcaNegocio negocioM = new MarcaNegocio();
            CategoriaNegocio negocioC = new CategoriaNegocio();

            try
            {
                cmbMarca.DataSource = negocioM.listar();
                cmbMarca.ValueMember = "Id";
                cmbMarca.DisplayMember = "Descripcion";
                cmbCategoria.DataSource = negocioC.listar();
                cmbCategoria.ValueMember = "Id";
                cmbCategoria.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null)
                {
                   articulo = new Articulo();
                }

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cmbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;
                articulo.UrlImagen = txtImagen.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                negocio.agregar(articulo);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
