using Desktop.Interfaces;
using Desktop.States.Proveedores;
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
using System.Windows.Forms;

namespace KioscoInformaticoDesktop.Views
{
    public partial class ProveedoresView : Form
    {
        //hacer los campos de cada uno de los estados
        public IFormState initialDisplayState;
        public IFormState addState;
        public IFormState editState;
        public IFormState deleteState;

        //estado que respeta esta interfaz
        public IFormState currentState;


        public IGenericService<Proveedor> proveedorService = new GenericService<Proveedor>();
        public IGenericService<Localidad> localidadService = new GenericService<Localidad>();

        public BindingSource ListProveedores = new BindingSource();
        public Proveedor proveedorCurrent;
        public ProveedoresView()
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

            //dataGridProveedoresView.DataSource = ListProveedores;
            //CargarGrilla();
            _ = CargarCombo();

        }

        private async Task CargarCombo()
        {
            comboLocalidades.DataSource = await localidadService.GetAllAsync(string.Empty);
            comboLocalidades.DisplayMember = "Nombre";
            comboLocalidades.ValueMember = "Id";
            comboLocalidades.SelectedIndex = -1;

        }

        private async Task CargarGrilla()
        {
            var proveedores = await proveedorService.GetAllAsync(null);
            ListProveedores.DataSource = proveedores;
            dataGridProveedoresView.Columns[6].Visible = false;
        }

        //método nuevo del formulario. Cambia el estado el set

        public void SetState(IFormState state)
        {
            currentState = state ?? throw new ArgumentNullException(nameof(state), "El estado no puede ser nulo.");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SetState(addState);
            currentState.OnAgregar();
            //tabControl1.SelectTab(tabPageEditarAgregar);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            currentState.OnGuardar();
            //if (proveedorCurrent != null)
            //{
            //    proveedorCurrent.Nombre = txtNombre.Text;
            //    proveedorCurrent.Direccion = txtDireccion.Text;
            //    proveedorCurrent.Telefonos = txtTelefonos.Text;
            //    proveedorCurrent.Cbu = txtCbu.Text;
            //    proveedorCurrent.LocalidadId = (int)comboLocalidades.SelectedValue;
            //    await proveedorService.UpdateAsync(proveedorCurrent);
            //    proveedorCurrent = null;
            //}
            //else
            //{
            //    var proveedor = new Proveedor
            //    {
            //        Nombre = txtNombre.Text,
            //        Direccion = txtDireccion.Text,
            //        Telefonos = txtTelefonos.Text,
            //        Cbu = txtCbu.Text,
            //        LocalidadId = (int)comboLocalidades.SelectedValue,
            //    };
            //    await proveedorService.AddAsync(proveedor);
            //}
            //await CargarGrilla();
            //txtNombre.Text = string.Empty;
            //txtDireccion.Text = string.Empty;
            //txtTelefonos.Text = string.Empty;
            //txtCbu.Text = string.Empty;
            //tabControl1.SelectTab(tabPageLista);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            currentState.OnCancelar();

            //proveedorCurrent = null;
            //txtNombre.Text = string.Empty;
            //txtDireccion.Text = string.Empty;
            //txtTelefonos.Text = string.Empty;
            //txtCbu.Text = string.Empty;
            //tabControl1.SelectTab(tabPageLista);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SetState(editState);
            currentState.OnModificar();
            //    proveedorCurrent = (Proveedor)ListProveedores.Current;
            //    txtNombre.Text = proveedorCurrent.Nombre;
            //    txtDireccion.Text = proveedorCurrent.Direccion;
            //    txtTelefonos.Text = proveedorCurrent.Telefonos;
            //    txtCbu.Text = proveedorCurrent.Cbu;
            //    comboLocalidades.SelectedValue = proveedorCurrent.LocalidadId;
            //    tabControl1.SelectTab(tabPageEditarAgregar);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            SetState(deleteState);
            currentState.OnEliminar();
            //proveedorCurrent = (Proveedor)ListProveedores.Current;
            //if (proveedorCurrent == null)
            //{
            //    MessageBox.Show("Debe seleccionar un proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //var result = MessageBox.Show($"¿Está seguro que desea eliminar el proveedor {proveedorCurrent.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    proveedorCurrent = (Proveedor)ListProveedores.Current;
            //    if (proveedorCurrent != null)
            //    {
            //        await proveedorService.DeleteAsync(proveedorCurrent.Id);
            //        await CargarGrilla();
            //    }
            //}
            //proveedorCurrent = null;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            currentState.OnBuscar();
            //FiltrarProveedor();
        }

        private async void FiltrarProveedor()
        {
            var proveedoresFiltrados = await proveedorService.GetAllAsync(txtFiltro.Text);
            ListProveedores.DataSource = proveedoresFiltrados;
        }

        private void iconButtonSalir_Click(object sender, EventArgs e)
        {
            currentState.OnSalir();
        }
    }
}
