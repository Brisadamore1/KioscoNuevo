using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.States.Proveedores
{
    public class EditState : IFormState
    {
        private ProveedoresView _form;
        public EditState(ProveedoresView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");

        }
        public void OnCancelar()
        {
            _form.txtNombre.Clear();
            _form.txtDireccion.Clear();
            _form.txtTelefonos.Clear();
            _form.txtCbu.Clear();
            _form.txtCondicionIVA.Clear();
            _form.comboLocalidades.SelectedIndex = -1;

            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }
        public async void OnGuardar()
        {
            if (string.IsNullOrEmpty(_form.txtNombre.Text))
            {
                MessageBox.Show("El nombre del proveedor es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_form.proveedorCurrent != null)
            {
                _form.proveedorCurrent.Nombre = _form.txtNombre.Text;
                _form.proveedorCurrent.Direccion = _form.txtDireccion.Text;
                _form.proveedorCurrent.Telefonos = _form.txtTelefonos.Text;
                _form.proveedorCurrent.Cbu = _form.txtCbu.Text;
                _form.proveedorCurrent.LocalidadId = (int)_form.comboLocalidades.SelectedValue;
                await _form.proveedorService.UpdateAsync(_form.proveedorCurrent);
                _form.proveedorCurrent = null;

                _form.SetState(_form.initialDisplayState);
                await _form.currentState.UpdateUI();
            }
        }
        public Task UpdateUI()
        {
            _form.proveedorCurrent = _form.dataGridProveedoresView.CurrentRow.DataBoundItem as Proveedor;
            _form.txtNombre.Text = _form.proveedorCurrent.Nombre;
            _form.txtDireccion.Text = _form.proveedorCurrent.Direccion;
            _form.txtTelefonos.Text = _form.proveedorCurrent.Telefonos;
            _form.txtCbu.Text = _form.proveedorCurrent.Cbu;
            _form.comboLocalidades.SelectedValue = _form.proveedorCurrent.LocalidadId;
            _form.tabControl1.SelectTab(_form.tabPageEditarAgregar);
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
