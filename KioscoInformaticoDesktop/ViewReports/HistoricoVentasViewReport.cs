using Microsoft.Reporting.WinForms;
using Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KioscoInformaticoDesktop.ViewReports
{
    public partial class HistoricoVentasViewReport : Form
    {
        ReportViewer reporte;
        private List<Venta> listaVentas;
        private string tituloReporte;

        public HistoricoVentasViewReport(List<Venta> ventas, string tituloReporte)
        {
            InitializeComponent();
            listaVentas = ventas;
            this.tituloReporte = tituloReporte;
            reporte = new ReportViewer();
            reporte.Dock = DockStyle.Fill;
            Controls.Add(reporte);
            
        }

        private void HistoricoVentasViewReport_Load(object sender, EventArgs e)
        {
            reporte.LocalReport.ReportEmbeddedResource = "Desktop.Reports.HistoricoVentasReport.rdlc";
            //construyo un Datatable con los datos de la venta

            //armamos la lista de ventas para el reporte


            var ventas= listaVentas.
                        OrderBy(venta => venta.FormaPago).
                        Select(venta => new { Id = venta.Id, Fecha = venta.Fecha, ClienteNombre = venta.Cliente.Nombre, Total = venta.Total, Iva = venta.Iva, FormaPago = venta.FormaPago.ToString() }).ToList();

            reporte.LocalReport.SetParameters(new ReportParameter("TituloReporte", tituloReporte));
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSVentas", ventas));
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.RefreshReport();
        }
    }
}
