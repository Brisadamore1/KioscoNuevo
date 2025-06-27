using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;

namespace Desktop.States.Localidades
{
    public class DeleteState : IFormState
    {
        private LocalidadesView _form;
        public DeleteState(LocalidadesView form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form), "El formulario no puede ser nulo.");
        }
        
        public async void OnEliminar() {
            var localidad = (Localidad)_form.listaLocalidades.Current;
            var result = MessageBox.Show($"¿Está seguro que desea eliminar la localidad {localidad.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await _form.localidadService.DeleteAsync(localidad.Id);
                _form.SetState(_form.initialDisplayState);
                await _form.currentState.UpdateUI();

            }
            else
            {
                _form.SetState(_form.initialDisplayState);
     
            }
        }
        public void OnAgregar() { }
        public void OnBuscar() { }
        public void OnCancelar() { }
        public void OnGuardar() { }
        public void OnModificar() { }
        public void OnSalir() { }
        public Task UpdateUI() { return Task.CompletedTask; }
    }
}
