using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.States.Clientes
{
    public class InitialDisplayState : IFormState
    {
        private ClientesView _form;
        public InitialDisplayState(ClientesView form)
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
            await CargarCombo();
            var clientes = await _form.clienteService.GetAllAsync(_form.txtFiltro.Text);
            _form.ListClientes.DataSource = clientes;
            _form.dataGridClientesView.DataSource = _form.ListClientes;
            _form.dataGridClientesView.Columns[5].Visible = false;
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
        private async Task CargarCombo()
        {
            _form.comboLocalidades.DataSource = await _form.localidadService.GetAllAsync(string.Empty);
            _form.comboLocalidades.DisplayMember = "Nombre";
            _form.comboLocalidades.ValueMember = "Id";
        }


        public void OnAgregar() { }
        public void OnCancelar() { }
        public void OnGuardar() { }
        public void OnModificar() { }
        public void OnEliminar() { }
    }
}
