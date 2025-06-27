using Desktop.ExtensionMethods;
using KioscoInformaticoDesktop.ViewReports;
using Service.Enums;
using Service.Interfaces;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KioscoInformaticoDesktop.Views
{
    public partial class VentasView : Form
    {
        IGenericService<Venta> clienteService = new GenericService<Venta>();

        ProductoService productoService = new ProductoService();
        GenericService<Venta> ventaService = new GenericService<Venta>();
        Venta venta = new Venta();

        public VentasView()
        {
            InitializeComponent();
            AjustePantalla();
        }

        private async void AjustePantalla()
        {
            #region carga de combos
            await Task.WhenAll(
                Task.Run(async () => comboBoxClientes.DataSource = await clienteService.GetAllAsync(string.Empty)),
                Task.Run(async () => comboBoxProductos.DataSource = await productoService.GetAllAsync())
            );
            comboBoxClientes.DisplayMember = "Nombre";
            comboBoxClientes.ValueMember = "Id";

            comboBoxProductos.DisplayMember = "Nombre";
            comboBoxProductos.ValueMember = "Id";
            comboBoxProductos.SelectedIndex = -1;

            comboBoxFormasDePago.DataSource = Enum.GetValues(typeof(FormaDePagoEnum));
            #endregion
            numericPrecio.Value = 0;
            numericCantidad.Value = 1;
            gridDetallesVenta.DataSource = venta.DetallesVenta.ToList();

        }

        private void comboBoxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProductos.SelectedIndex != -1)
            {
                Producto producto = (Producto)comboBoxProductos.SelectedItem;
                numericPrecio.Value = producto.Precio;
            }
            BtnAgregar.Enabled = comboBoxProductos.SelectedIndex != -1;
        }

        private void numericCantidad_ValueChanged(object sender, EventArgs e)
        {
            numericSubtotal.Value = numericPrecio.Value * numericCantidad.Value;
        }

        private void numericPrecio_ValueChanged(object sender, EventArgs e)
        {
            numericSubtotal.Value = numericPrecio.Value * numericCantidad.Value;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            var detalleVenta = new DetalleVenta
            {
                Producto = (Producto)comboBoxProductos.SelectedItem,
                ProductoId = (int)comboBoxProductos.SelectedValue,
                Cantidad = (int)numericCantidad.Value,
                PrecioUnitario = numericPrecio.Value
            };
            venta.DetallesVenta.Add(detalleVenta);
            gridDetallesVenta.DataSource = venta.DetallesVenta.ToList();
            comboBoxProductos.SelectedIndex = -1;
            comboBoxProductos.Focus();
            ActualizarTotalFactura();

        }

        private void ActualizarTotalFactura()
        {
            //calculamos el total de la venta sumando los subtotales de los detalles
            numericTotal.Value = venta.DetallesVenta.Sum(dv => dv.Cantidad * dv.PrecioUnitario);
        }

        private void gridDetallesVenta_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gridDetallesVenta.OcultarColumnas(new string[] { "Id", "Venta", "VentaId", "ProductoId", "Eliminado" });
            BtnQuitar.Enabled = gridDetallesVenta.Rows.Count > 0;
        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            if (gridDetallesVenta.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un detalle de venta");
                return;
            }
            var detalleVenta = (DetalleVenta)gridDetallesVenta.CurrentRow.DataBoundItem;
            venta.DetallesVenta.Remove(detalleVenta);
            gridDetallesVenta.DataSource = venta.DetallesVenta.ToList();
            ActualizarTotalFactura();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            //cargamos los datos de la venta
            venta.ClienteId = (int)comboBoxClientes.SelectedValue;
            venta.Cliente = (Cliente)comboBoxClientes.SelectedItem;
            venta.FormaPago = (FormaDePagoEnum)comboBoxFormasDePago.SelectedValue;
            venta.Fecha = DateTime.Now;

            venta.Total = numericTotal.Value;
            venta.Iva = venta.Total * 0.21m;
            var nuevaVenta=await ventaService.AddAsync(venta);
            var facturaVentaViewReport = new FacturaVentaViewReport(nuevaVenta);
            facturaVentaViewReport.ShowDialog();

        }
    }
}
