using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Desktop.States.Clientes
{
    public class AddState : IFormState
    {
        private ClientesView _form;
        public AddState(ClientesView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");
        }

        public void OnCancelar()
        {
            //esto es para limpiar los campos del formulario cuando se cancela la operación de agregar un cliente.

            _form.txtNombre.Clear();
            _form.txtDireccion.Clear();
            _form.txtTelefonos.Clear();
            _form.comboLocalidades.SelectedIndex = 0;
            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }

        public async void OnGuardar()
        {
             var cliente = new Cliente
             {
                Nombre = _form.txtNombre.Text,
                Direccion = _form.txtDireccion.Text,
                Telefonos = _form.txtTelefonos.Text,
                LocalidadId = (int)_form.comboLocalidades.SelectedValue,
                FechaNacimiento = _form.dateTimeFechaNacimiento.Value
             };
            await _form.clienteService.AddAsync(cliente);
            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
        }

        public Task UpdateUI()
        {
            //Esto es para limpiar los campos del formulario cuando se agrega un cliente.
            _form.txtNombre.Clear();
            _form.txtDireccion.Clear();
            _form.txtTelefonos.Clear();
            _form.tabControl.SelectTab(_form.tabPageAgregarEditar);
            return Task.CompletedTask;
        }
        public void OnAgregar()
        {
            UpdateUI();
        }

        public void OnSalir() { }
        public void OnModificar() { }
        public void OnEliminar(){}
        public void OnBuscar(){}
       
    }
}
