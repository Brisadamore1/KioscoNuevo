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
    public class AddState : IFormState
    {
        private ProveedoresView _form;

        public AddState(ProveedoresView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");

        }
       
        public void OnCancelar()
        {
            //esto es para limpiar los campos del formulario cuando se cancela la operación de agregar un Proveedor.
            _form.txtNombre.Clear();
            _form.txtDireccion.Clear();
            _form.txtTelefonos.Clear();
            _form.txtCbu.Clear();
            _form.txtCondicionIVA.Clear();
            _form.comboLocalidades.SelectedIndex = -1; // Limpia la selección del combo de localidades

            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }


        public async void OnGuardar()
        {
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
            if (string.IsNullOrEmpty(_form.txtNombre.Text))
            {
                MessageBox.Show("El nombre del proveedor es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var proveedor = new Proveedor
            {
                Nombre = _form.txtNombre.Text,
                Direccion = _form.txtDireccion.Text,
                Telefonos = _form.txtTelefonos.Text,
                Cbu = _form.txtCbu.Text,
                //CondicionIva = _form.txtCondicionIVA.Text
                LocalidadId = (int)_form.comboLocalidades.SelectedValue,
            };
            await _form.proveedorService.AddAsync(proveedor);
            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
            //}
            //await CargarGrilla();
            //txtNombre.Text = string.Empty;
            //txtDireccion.Text = string.Empty;
            //txtTelefonos.Text = string.Empty;
            //txtCbu.Text = string.Empty;
            //tabControl1.SelectTab(tabPageLista);
        }



        public Task UpdateUI()
        {
            _form.txtNombre.Clear();
            _form.txtDireccion.Clear();
            _form.txtTelefonos.Clear();
            _form.txtCbu.Clear();
            _form.txtCondicionIVA.Clear();
            _form.comboLocalidades.SelectedIndex = -1;
            _form.tabControl1.SelectTab(_form.tabPageEditarAgregar);
            return Task.CompletedTask;

        }
        public void OnAgregar()
        {
            UpdateUI();
        }
        public void OnSalir() { }
        public void OnModificar() { }
        public void OnEliminar() { }
        public void OnBuscar() { }
    }
}
