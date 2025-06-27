using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Interfaces
{
    public interface IFormState
    {
        // Las interfaces son como contratos que determinan los métodos y propiedades que va a tener el objeto que las implementa.
        // En nuestro caso seria las operaciones que el formulario puede realizar.
        // Métodos que implementa los estados. Interfaz permite que los estados sean cambiables. 
        void OnBuscar();
        void OnAgregar();
        void OnModificar();
        void OnEliminar();
        void OnGuardar();
        void OnCancelar();
        void OnSalir();

        // Método para actualizar la UI, se usa en los estados que necesitan actualizar la interfaz de usuario, como el estado inicial que muestra la lista de localidades.
        // Método que comparten todos los estados para ponerle ahi todos los cambios esteticos que hagan sobre la UI. Si tienen que cambiar algo en el formulario, lo ponen en este método.
        Task UpdateUI();

    }
}
