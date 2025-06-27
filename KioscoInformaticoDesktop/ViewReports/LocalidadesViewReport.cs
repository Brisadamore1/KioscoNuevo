using Microsoft.Reporting.WinForms;
using Service.Interfaces;
using Service.Models;
using Service.Services;
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
    public partial class LocalidadesViewReport : Form
    {
        ReportViewer reporte;
        IGenericService<Localidad> localidadService = new GenericService<Localidad>();
        public LocalidadesViewReport()
        {
            InitializeComponent();
            reporte = new ReportViewer();

            reporte.Dock = DockStyle.Fill;
            
            Controls.Add(reporte);
        }

        private async void LocalidadesViewReport_Load(object sender, EventArgs e)
        {
            reporte.LocalReport.ReportEmbeddedResource = "Desktop.Reports.LocalidadesReport.rdlc";
            var localidades = await localidadService.GetAllAsync(string.Empty);
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSLocalidades", localidades));
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.RefreshReport();
        }
    }
}
