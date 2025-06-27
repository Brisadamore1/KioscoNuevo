using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Interfaces;
using Service.Services;

namespace Desktop.States.Localidades
{
    public class InitialDisplayState : IFormState
    {
        //Este es el estado inicial del formulario LocalidadesView, donde se muestra la lista de localidades. Muestra la grilla de localidades.
        //los estados necesitan tener una referencia al formulario que los contiene

        //Referencia al formulario LocalidadesView, campo que recibe la referencia al formulario en el constructor
        private LocalidadesView _form;
        public InitialDisplayState(LocalidadesView form)
        {
            //necesitamos que se almacene la referencia. Esto es para que el estado pueda interactuar con el formulario y sus controles.
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

            _form.listaLocalidades.DataSource = await _form.localidadService.GetAllAsync(_form.txtFiltro.Text);
            _form.dataGridLocalidades.DataSource = _form.listaLocalidades;
            _form.tabControl.SelectTab(_form.tabPageLista);

            _form.tabControl.Selecting += (sender, e) =>
            { 
                if (e.TabPage == _form.tabPageAgregarEditar)
                    if (_form.currentState==_form.initialDisplayState)
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
