using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominioArticulos;
using NegocioArticulos;

namespace PresentacionArticulos
{
    public partial class FormArticulosDetalle : Form
    {
        private Articulo articulo;
        public FormArticulosDetalle()
        {
            InitializeComponent();
        }

        // Constructor que recibe un articulo seleccionado.
        // el this.articulo, es para poder hacer un pasaje entre ventanas, y preparar el articulo con los campos ya cargados!.
        public FormArticulosDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Detalles";
        }

        private void FormArticulosDetalle_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString();
            txtMarca.Text = articulo.Marca.Descripcion;
            txtCategoria.Text = articulo.Categoria.Descripcion;

            cargarImagen(articulo.UrlImagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pxbArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pxbArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
