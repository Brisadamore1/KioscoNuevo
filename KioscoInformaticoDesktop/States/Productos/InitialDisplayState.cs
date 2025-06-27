using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.States.Productos
{
    public class InitialDisplayState : IFormState
    {
        private ProductosView _form;
        public InitialDisplayState(ProductosView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");

        }

        public async void OnBuscar()
        {
            await UpdateUI();
        }

        public void OnSalir()
        {
            _form.Close();

        }

        public async Task UpdateUI()
        {
            

            _form.ListProductos.DataSource = await _form.productoService.GetAllAsync(string.Empty);

            //Esta linea es para cargar el dataGrid de productos
            _form.dataGridProductosView.DataSource = _form.ListProductos;

            //Esta linea es para inicializar la lista que se va a filtrar
            _form.ListaAFiltrar = (List<Producto>)_form.ListProductos.DataSource;

            //Esto es para cargar el dataGrid de productos
            _form.tabControl.SelectTab(_form.tabPageLista);

            _form.tabControl.Selecting += (sender, e) =>
            {
                if (e.TabPage == _form.tabPageAgregarEditar)
                    if (_form.currentState == _form.initialDisplayState)
                    {
                        e.Cancel = true; // Evita que se cambie a la pestaña de agregar/editar directamente desde el estado inicial
                    }
                if (e.TabPage == _form.tabPageLista)
                    if (_form.currentState == _form.addState || _form.currentState == _form.editState)
                    {
                        e.Cancel = true; // Deshabilita la pestaña de agregar/editar si no está en el estado inicial
                    }
            };
        }

        public void OnAgregar() { }
        public void OnCancelar() { }
        public void OnGuardar() { }
        public void OnModificar() { }
        public void OnEliminar() { }

    }
}
