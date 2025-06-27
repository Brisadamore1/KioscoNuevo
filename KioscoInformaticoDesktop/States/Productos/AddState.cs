using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.States.Productos
{
    public class AddState : IFormState
    {
        private ProductosView _form;
        public AddState(ProductosView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");
        }
        
        public void OnCancelar()
        {
            //esto es para limpiar los campos del formulario cuando se cancela la operación de agregar un producto.
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
            decimal precio = _form.numericPrecio.Value;
            var producto = new Producto
            {
                Nombre = _form.txtNombre.Text,
                Precio = precio
            };
            await _form.productoService.AddAsync(producto);
            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();

            //if (string.IsNullOrEmpty(txtNombre.Text))
            //{
            //    MessageBox.Show("El nombre del producto es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (productoCurrent != null)
            //{
            //    productoCurrent.Nombre = txtNombre.Text;
            //    productoCurrent.Precio = numericPrecio.Value;
            //    await productoService.UpdateAsync(productoCurrent);
            //    productoCurrent = null;
            //}
            //else
            //{
            //    decimal precio = numericPrecio.Value;
            //    var producto = new Producto
            //    {
            //        Nombre = txtNombre.Text,
            //        Precio = precio
            //    };
            //    await productoService.AddAsync(producto);

            //}
            //await CargarGrilla();
            //txtNombre.Text = string.Empty;
            //numericPrecio.Value = 0;
            //tabControl.SelectTab(tabPageLista);
        }

        public Task UpdateUI()
        {
            _form.txtNombre.Clear();
            _form.numericPrecio.Value = 0;
            _form.tabControl.SelectTab(_form.tabPageAgregarEditar);
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
