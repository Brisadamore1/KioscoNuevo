using Desktop.Interfaces;
using Desktop.States.Productos;
using Service.Interfaces;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace KioscoInformaticoDesktop.Views
{
    public partial class ProductosView : Form
    {
        //hacer los campos de cada uno de los estados
        public IFormState initialDisplayState;
        public IFormState addState;
        public IFormState editState;
        public IFormState deleteState;

        //estado que respeta esta interfaz
        public IFormState currentState;

        public IGenericService<Producto> productoService = new GenericService<Producto>();
        public BindingSource ListProductos = new BindingSource();
        public List<Producto> ListaAFiltrar = new List<Producto>();
        public Producto productoCurrent;

        public ProductosView()
        {
            InitializeComponent();
            //this es la referencia al formulario localidades
            initialDisplayState = new InitialDisplayState(this);
            addState = new AddState(this);
            editState = new EditState(this);
            deleteState = new DeleteState(this);

            //inicializamos el estado actual con el estado inicial
            currentState = initialDisplayState;
            currentState.UpdateUI();

            //dataGridProductosView.DataSource = ListProductos;
            //CargarGrilla();
        }
        public void SetState(IFormState state)
        {
            currentState = state ?? throw new ArgumentNullException(nameof(state), "El estado no puede ser nulo.");
        }
        private async Task CargarGrilla()
        {
            ListProductos.DataSource = await productoService.GetAllAsync(string.Empty);
            ListaAFiltrar = (List<Producto>)ListProductos.DataSource;

        }

        private void iconButtonAgregar_Click(object sender, EventArgs e)
        {
            SetState(addState);
            currentState.OnAgregar();
            //tabControl.SelectTab(tabPageAgregarEditar);
        }

        private void iconButtonEditar_Click(object sender, EventArgs e)
        {
            SetState(editState);
            currentState.OnModificar();
            //productoCurrent = (Producto)ListProductos.Current;
            //txtNombre.Text = productoCurrent.Nombre;
            //numericPrecio.Value = productoCurrent.Precio;
            //tabControl.SelectTab(tabPageAgregarEditar);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            currentState.OnGuardar();
            //if (string.IsNullOrEmpty(txtNombre.Text))
            //{
            //    MessageBox.Show("El nombre del producto es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (productoCurrent != null)
            //{
            //    productoCurrent.Nombre = txtNombre.Text;
            //    productoCurrent.Precio = numericPrecio.Value;
            //    await productoService.UpdateAsync(productoCurrent);
            //    productoCurrent = null;
            //}
            //else
            //{
            //    decimal precio = numericPrecio.Value;
            //    var producto = new Producto
            //    {
            //        Nombre = txtNombre.Text,
            //        Precio = precio
            //    };
            //    await productoService.AddAsync(producto);

            //}
            //await CargarGrilla();
            //txtNombre.Text = string.Empty;
            //numericPrecio.Value = 0;
            //tabControl.SelectTab(tabPageLista);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            currentState.OnCancelar();
            //this.Close();
        }

        private async void iconButtonEliminar_Click(object sender, EventArgs e)
        {
            SetState(deleteState);
            currentState.OnEliminar();
            //var result = MessageBox.Show("¿Está seguro que desea eliminar el producto?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    productoCurrent = ListProductos.Current as Producto;
            //    if (productoCurrent != null)
            //    {
            //        await productoService.DeleteAsync(productoCurrent.Id);
            //        await CargarGrilla();
            //    }
            //}
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            currentState.OnBuscar();
            //FiltrarProductos();
        }

        //private void FiltrarProductos()
        //{
        //    var productosFiltrados = ListaAFiltrar.Where(p => p.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper())).ToList();
        //    ListProductos.DataSource = productosFiltrados;
        //}

        //private void txtFiltro_TextChanged(object sender, EventArgs e)
        //{
        //    FiltrarProductos();
        //}

        private void iconButtonSalir_Click(object sender, EventArgs e)
        {
            currentState.OnSalir();
        }
    }
}
