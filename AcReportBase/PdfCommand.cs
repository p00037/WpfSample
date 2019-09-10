using GrapeCity.ActiveReports.Export.Pdf.Section;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace AcReportBase
{
    public class PdfCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var window = (Window)parameter;

            var viewModel = (ReportViewerViewModel)window.DataContext;

            var export = new PdfExport();
            var dialog = new SaveFileDialog
            {
                Filter = "PDFファイル(*.pdf)|*.pdf",
                FileName = viewModel.Report.Document.Name + ".pdf"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                export.Export(viewModel.Report.Document, dialog.FileName);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
