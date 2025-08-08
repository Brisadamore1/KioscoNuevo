using CommunityToolkit.Mvvm.Messaging;
using KioscoInformaticoApp.Class;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KioscoInformaticoApp.ViewModels
{
    public class ProductosViewModel : ObjectNotification
    {
		private GenericService<Producto> productoService= new GenericService<Producto>();

        private string filterProducts;

		public string FilterProducts
		{
			get { return filterProducts; }
			set { filterProducts = value;
                OnPropertyChanged();
                _ = FiltrarProductos();
            }
		}
        //porque hacemos esto, porque lo dijo Gabriel
        //https://chatgpt.com/share/9ab527ab-34a6-426c-b7a7-362c38e460a7
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Producto> productos;

		public ObservableCollection<Producto> Productos
		{
			get { return productos; }
			set { productos = value; 
			OnPropertyChanged();
            }
		}

        private List<Producto>? productosListToFilter;

        private Producto? selectedProduct;

        public Producto? SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value;
                OnPropertyChanged();
                EditarProductoCommand.ChangeCanExecute();
            }
        }

        public Command ObtenerProductosCommand { get; }
        public Command FiltrarProductosCommand { get; }
        public Command AgregarProductoCommand { get; }
        public Command EditarProductoCommand { get; }

        public ProductosViewModel()
        {
            ObtenerProductosCommand = new Command(async () => await ObtenerProductos());
            FiltrarProductosCommand = new Command(async () => await FiltrarProductos());
            AgregarProductoCommand = new Command(async () => await AgregarProducto());
            EditarProductoCommand = new Command(async (obj) => await EditarProducto(),PermitirEditar);
            ObtenerProductos();

        }

        private bool PermitirEditar(object arg)
        {
            return SelectedProduct!=null;
        }

        private async Task EditarProducto()
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "ProductToEdit", SelectedProduct }
            };
            await Shell.Current.GoToAsync("//AgregarEditarProducto", navigationParameter);
            //WeakReferenceMessenger.Default.Send(new Message("EditarProducto") { ProductoAEditar=SelectedProduct });
        }

        private async Task AgregarProducto()
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "ProductToEdit", null }
            };
            await Shell.Current.GoToAsync("//AgregarEditarProducto", navigationParameter);

            //WeakReferenceMessenger.Default.Send(new Message("AgregarProducto"));
        }

        public async Task FiltrarProductos()
        {
            var productosFiltrados = productosListToFilter.Where(p => p.Nombre.ToUpper().Contains(filterProducts.ToUpper()));
            Productos = new ObservableCollection<Producto>(productosFiltrados);
        }

        public async Task ObtenerProductos()
        {
            FilterProducts = string.Empty;
            IsRefreshing = true; // Asegúrate de que IsRefreshing se establezca en true al inicio
            productosListToFilter = await productoService.GetAllAsync();
            Productos = new ObservableCollection<Producto>(productosListToFilter);
            IsRefreshing = false; // Establece IsRefreshing en false al final cuando llegan los profesionales
        }
    }
}
