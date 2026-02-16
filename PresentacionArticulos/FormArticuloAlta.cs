using DominioArticulos;
using NegocioArticulos;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace PresentacionArticulos
{
    public partial class FormArticuloAlta : Form
    {
        Articulo articulo = null;
        private OpenFileDialog archivo = null;

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
            Text = "Modificar Articulo";
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

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo.ToString();
                    txtNombre.Text = articulo.Nombre.ToString();
                    txtDescripcion.Text = articulo.Descripcion.ToString();
                    cmbMarca.SelectedValue = articulo.Marca.Id;
                    cmbCategoria.SelectedValue = articulo.Categoria.Id;
                    txtImagen.Text = articulo.UrlImagen.ToString();
                    cargarImagen(articulo.UrlImagen);
                    txtPrecio.Text = articulo.Precio.ToString();
                } 
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

                if (validarCampos())
                {
                    return;
                }

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cmbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;
                articulo.UrlImagen = txtImagen.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado Exitosamente!");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado Exitosamente!");
                }

                if (archivo != null && !(txtImagen.Text.ToUpper().Contains("HTTP")))
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["ArticulosImagenes"] + archivo.SafeFileName);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pxbAltaArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pxbAltaArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png";
            if (archivo.ShowDialog() == DialogResult.OK)
            {

                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }

        // Validar campos obligatorios.
        private bool validarCampos()
        {
            bool hayErrores = false;

            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                errorProvider1.SetError(txtCodigo, "El código es obligatorio");
                hayErrores = true;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, "El nombre es obligatorio");
                hayErrores = true;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                errorProvider1.SetError(txtPrecio, "El precio es obligatorio");
                hayErrores = true;
            }
            else if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                errorProvider1.SetError(txtPrecio, "Debe ser numérico");
                hayErrores = true;
            }
            else if (precio <= 0)
            {
                errorProvider1.SetError(txtPrecio, "Debe ser mayor a 0");
                hayErrores = true;
            }

            return hayErrores;
        }

    }
}
