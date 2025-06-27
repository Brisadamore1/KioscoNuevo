using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.States.Proveedores
{
    public class DeleteState : IFormState
    {
        private ProveedoresView _form;
        public DeleteState(ProveedoresView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");
        }
        public async void OnEliminar()
        {

            _form.proveedorCurrent = (Proveedor)_form.ListProveedores.Current;
            if (_form.proveedorCurrent == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = MessageBox.Show($"¿Está seguro que desea eliminar el proveedor {_form.proveedorCurrent.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _form.proveedorCurrent = (Proveedor)_form.ListProveedores.Current;
                if (_form.proveedorCurrent != null)
                {
                    await _form.proveedorService.DeleteAsync(_form.proveedorCurrent.Id);
                    _form.SetState(_form.initialDisplayState);
                    _form.currentState.UpdateUI();
                    // await CargarGrilla();
                }
            }
            else
            {
                _form.SetState(_form.initialDisplayState);
            }
            _form.proveedorCurrent = null;
        }

        public Task UpdateUI()
        {
            return Task.CompletedTask;
        }
        public void OnAgregar() { }
        public void OnBuscar() { }
        public void OnCancelar() { }
        public void OnGuardar() { }
        public void OnModificar() { }
        public void OnSalir() { }
    }
}
