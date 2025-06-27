using Desktop.Interfaces;
using Desktop.States.Clientes;
using KioscoInformaticoDesktop.ViewReports;
using Microsoft.Reporting.WinForms;
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
    public partial class ClientesView : Form
    {
        //hacer los campos de cada uno de los estados
        public IFormState initialDisplayState;
        public IFormState addState;
        public IFormState editState;
        public IFormState deleteState;

        //estado que respeta esta interfaz
        public IFormState currentState;

       
        public IGenericService<Cliente> clienteService = new GenericService<Cliente>();
        public IGenericService<Localidad> localidadService = new GenericService<Localidad>();
        public BindingSource ListClientes = new BindingSource();
        public Cliente clienteCurrent;

        public ClientesView()
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

            // Suscribimos el evento para controlar el cambio de pestaña
            // Esto se hace para evitar que el usuario cambie de pestaña mientras está en un estado de edición o agregado
            // tabControl.Selecting += tabControl_Selecting;

            //esto es para que el formulario sepa que cliente esta editando
            // dataGridClientesView.DataSource = ListClientes;
        }

        //private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        //{
        //    // Si se intenta acceder a Agregar/Editar manualmente
        //    if (e.TabPage == tabPageAgregarEditar)
        //    {
        //        if (!(currentState is AddState) && !(currentState is EditState))
        //        {
        //            e.Cancel = true;
        //        }
        //    }

        //    // Si se intenta volver a la lista estando en modo edición o agregado
        //    if (e.TabPage == tabPageLista)
        //    {
        //        if (currentState is AddState || currentState is EditState)
        //        {
        //            MessageBox.Show("Debe cancelar la operación actual antes de volver a la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            e.Cancel = true;
        //        }
        //    }
        //}



        //método nuevo del formulario. Cambia el estado el set. Se usa para cambiar el estado del formulario a través de los botones o acciones del usuario.

        public void SetState(IFormState state)
        {
            currentState = state ?? throw new ArgumentNullException(nameof(state), "El estado no puede ser nulo.");
        }

        private void iconButtonAgregar_Click(object sender, EventArgs e)
        {
            SetState(addState);
            currentState.OnAgregar();
            //tabControl.SelectTab(tabPageAgregarEditar);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            currentState.OnGuardar();
            //if (clienteCurrent != null)
            //{
            //    clienteCurrent.Nombre = txtNombre.Text;
            //    clienteCurrent.Direccion = txtDireccion.Text;
            //    clienteCurrent.Telefonos = txtTelefonos.Text;
            //    clienteCurrent.LocalidadId = (int)comboLocalidades.SelectedValue;
            //    clienteCurrent.FechaNacimiento = dateTimeFechaNacimiento.Value;
            //    await clienteService.UpdateAsync(clienteCurrent);
            //    clienteCurrent = null;
            //}
            //else
            //{
            //    var cliente = new Cliente
            //    {
            //        Nombre = txtNombre.Text,
            //        Direccion = txtDireccion.Text,
            //        Telefonos = txtTelefonos.Text,
            //        LocalidadId = (int)comboLocalidades.SelectedValue,
            //        FechaNacimiento = dateTimeFechaNacimiento.Value
            //    };
            //    await clienteService.AddAsync(cliente);
            //}
            //await CargarGrilla();
            //txtNombre.Text = string.Empty;
            //txtDireccion.Text = string.Empty;
            //txtTelefonos.Text = string.Empty;
            //tabControl.SelectTab(tabPageLista);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            currentState.OnCancelar(); // esto limpia campos y demás lógica del estado
            //clienteCurrent = null;
            //txtNombre.Text = string.Empty;
            //txtDireccion.Text = string.Empty;
            //txtTelefonos.Text = string.Empty;
            //comboLocalidades.SelectedIndex = 0;
            //tabControl.SelectTab(tabPageLista);
        }

        private void iconButtonEditar_Click(object sender, EventArgs e)
        {
            SetState(editState);
            currentState.OnModificar();
            //clienteCurrent = (Cliente)ListClientes.Current;
            //txtNombre.Text = clienteCurrent.Nombre;
            //txtDireccion.Text = clienteCurrent.Direccion;
            //txtTelefonos.Text = clienteCurrent.Telefonos;
            //comboLocalidades.SelectedValue = clienteCurrent.LocalidadId;
            //dateTimeFechaNacimiento.Value = clienteCurrent.FechaNacimiento;
            //tabControl.SelectTab(tabPageAgregarEditar);
        }

        private async void iconButtonEliminar_Click(object sender, EventArgs e)
        {
            SetState(deleteState);
            currentState.OnEliminar();
            //clienteCurrent = (Cliente)ListClientes.Current;
            //if (clienteCurrent == null)
            //{
            //    MessageBox.Show("Debe seleccionar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //var result = MessageBox.Show($"¿Está seguro que desea eliminar el cliente {clienteCurrent.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    clienteCurrent = (Cliente)ListClientes.Current;
            //    if (clienteCurrent != null)
            //    {
            //        await clienteService.DeleteAsync(clienteCurrent.Id);
            //        await CargarGrilla();
            //    }
            //}
            //clienteCurrent = null;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            currentState.OnBuscar();
        }

        private void iconButtonSalir_Click(object sender, EventArgs e)
        {
            currentState.OnSalir();
        }
    }
}
