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
    public class EditState : IFormState
    {
        private ClientesView _form;
        public EditState(ClientesView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");
        }
        public void OnCancelar()
        {

            _form.txtNombre.Text = string.Empty;
            _form.txtDireccion.Text = string.Empty;
            _form.txtTelefonos.Text = string.Empty;
            _form.comboLocalidades.SelectedIndex = 0;
            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }

        public async void OnGuardar()
        {
            _form.clienteCurrent.Nombre = _form.txtNombre.Text;
            _form.clienteCurrent.Direccion = _form.txtDireccion.Text;
            _form.clienteCurrent.Telefonos = _form.txtTelefonos.Text;
            _form.clienteCurrent.LocalidadId = (int)_form.comboLocalidades.SelectedValue;
            _form.clienteCurrent.FechaNacimiento = _form.dateTimeFechaNacimiento.Value;

            await _form.clienteService.UpdateAsync(_form.clienteCurrent);
            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
        }

        public Task UpdateUI()
        {
            // _form.clienteCurrent = (Cliente)_form.ListClientes.Current;
       
            _form.clienteCurrent = _form.dataGridClientesView.CurrentRow.DataBoundItem as Cliente;
            _form.txtNombre.Text = _form.clienteCurrent.Nombre;
            _form.txtDireccion.Text = _form.clienteCurrent.Direccion;
            _form.txtTelefonos.Text = _form.clienteCurrent.Telefonos;
            _form.comboLocalidades.SelectedValue = _form.clienteCurrent.LocalidadId;
            _form.dateTimeFechaNacimiento.Value = _form.clienteCurrent.FechaNacimiento;

            _form.tabControl.SelectTab(_form.tabPageAgregarEditar);
            return Task.CompletedTask;
        }
        public void OnModificar()
        {
            UpdateUI();
        }

        public void OnSalir() { }
        public void OnAgregar() { }
        public void OnEliminar() { }
        public void OnBuscar() { }
    }
}
