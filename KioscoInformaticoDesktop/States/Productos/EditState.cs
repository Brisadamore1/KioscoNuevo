using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.States.Productos
{
    public class EditState : IFormState
    {
        private ProductosView _form;
        public EditState(ProductosView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");
        }
        
        public void OnCancelar()
        {
            _form.txtNombre.Clear();
            _form.numericPrecio.Value = 0;
            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }


        public async void OnGuardar()
        {
            if (string.IsNullOrEmpty(_form.txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _form.productoCurrent.Nombre = _form.txtNombre.Text;
            _form.productoCurrent.Precio = _form.numericPrecio.Value;

            await _form.productoService.UpdateAsync(_form.productoCurrent);
            _form.productoCurrent = null;
            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();

        }
        public Task UpdateUI()
        {
            //Esto es para cargar el dataGrid de productos
            _form.productoCurrent = _form.dataGridProductosView.CurrentRow.DataBoundItem as Producto;
            _form.txtNombre.Text = _form.productoCurrent.Nombre;
            _form.numericPrecio.Value = _form.productoCurrent.Precio;

            _form.tabControl.SelectTab(_form.tabPageAgregarEditar);
            return Task.CompletedTask;
        }

        public void OnModificar()
        {
            UpdateUI();
        }
        public void OnSalir() { }
        public void OnEliminar() { }
        public void OnBuscar() { }
        public void OnAgregar() { }


    }
}
