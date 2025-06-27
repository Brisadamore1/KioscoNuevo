using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.States.Proveedores
{
    public class InitialDisplayState : IFormState
    {
        private ProveedoresView _form;
        public InitialDisplayState(ProveedoresView form)
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
            _form.ListProveedores.DataSource = await _form.proveedorService.GetAllAsync(_form.txtFiltro.Text);
            _form.dataGridProveedoresView.DataSource = _form.ListProveedores;

            //Esto es para cargar el dataGrid de proveedores
            _form.tabControl1.SelectTab(_form.tabPageLista);

            _form.tabControl1.Selecting += (sender, e) =>
            {
                if (e.TabPage == _form.tabPageEditarAgregar)
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
            _form.comboLocalidades.SelectedIndex = -1;
        }

        public void OnAgregar() { }
        public void OnCancelar() { }
        public void OnGuardar() { }
        public void OnModificar() { }
        public void OnEliminar() { }
    }
}
