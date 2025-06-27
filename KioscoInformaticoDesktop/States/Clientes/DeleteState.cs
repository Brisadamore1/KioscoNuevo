using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.States.Clientes
{
    public class DeleteState : IFormState
    {
        private ClientesView _form;
        public DeleteState(ClientesView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");
        }

        public async void OnEliminar()
        { 
            _form.clienteCurrent = (Cliente)_form.ListClientes.Current;
            if (_form.clienteCurrent == null)
            {
                MessageBox.Show("Debe seleccionar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = MessageBox.Show($"¿Está seguro que desea eliminar el cliente {_form.clienteCurrent.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (_form.clienteCurrent != null)
                {
                    await _form.clienteService.DeleteAsync(_form.clienteCurrent.Id);
                    _form.SetState(_form.initialDisplayState);
                    _form.currentState.UpdateUI();
                }
            }
            else
            {
                _form.SetState(_form.initialDisplayState);
            }
            _form.clienteCurrent = null;
        }

        public void OnAgregar() {}

        public void OnBuscar() {}

        public void OnCancelar() {}
        public void OnGuardar() { }
        public void OnModificar() { }
        public void OnSalir() { }

        public Task UpdateUI(){ return Task.CompletedTask; }
    }
}
