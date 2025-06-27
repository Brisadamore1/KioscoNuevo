using Desktop.Interfaces;
using Desktop.States.Localidades;
using KioscoInformaticoDesktop.ViewReports;
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
    public partial class LocalidadesView : Form
    {
        //hacer los campos de cada uno de los estados
        public IFormState initialDisplayState;
        public IFormState addState;
        public IFormState editState;
        public IFormState deleteState;

        //estado que respeta esta interfaz
        public IFormState currentState;


        public IGenericService<Localidad> localidadService = new GenericService<Localidad>();
        public BindingSource listaLocalidades = new BindingSource();
        public Localidad localidadCurrent;

        public LocalidadesView()
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
            //tabControl.SelectTab(tabPageAgregarEditar);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            currentState.OnGuardar();
            //if (string.IsNullOrEmpty(txtNombre.Text))
            //{
            //    MessageBox.Show("El nombre de la localidad es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if (localidadCurrent != null)
            //{
            //    localidadCurrent.Nombre = txtNombre.Text;
            //    await localidadService.UpdateAsync(localidadCurrent);
            //    localidadCurrent = null;
            //}
            //else
            //{
            //    var localidad = new Localidad
            //    {
            //        Nombre = txtNombre.Text
            //    };
            //    await localidadService.AddAsync(localidad);
            //}
            //await CargarGrilla();
            //txtNombre.Text = string.Empty;
            //tabControl.SelectTab(tabPageLista);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetState(editState);
            currentState.OnModificar();

            //localidadCurrent = (Localidad)listaLocalidades.Current;
            //txtNombre.Text = localidadCurrent.Nombre;
            //tabControl.SelectTab(tabPageAgregarEditar);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            SetState(deleteState);
            currentState.OnEliminar();
            //localidadCurrent = (Localidad)listaLocalidades.Current;
            //var result = MessageBox.Show($"¿Está seguro que desea eliminar la localidad {localidadCurrent.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    await localidadService.DeleteAsync(localidadCurrent.Id);
            //    await CargarGrilla();
            //}
            //localidadCurrent = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            currentState.OnCancelar();
            //localidadCurrent = null;
            //txtNombre.Text = string.Empty;
            //tabControl.SelectTab(tabPageLista);

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            currentState.OnBuscar();
        }

        private void iconButtonSalir_Click(object sender, EventArgs e)
        {
            currentState.OnSalir();
        }
    }
}
