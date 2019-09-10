using GrapeCity.ActiveReports;
using System.Windows;

namespace AcReportBase
{
    /// <summary>
    /// ReportViewer.xaml の相互作用ロジック
    /// </summary>
    public partial class ReportViewer : Window
    {
        private ReportViewerViewModel viewModel = new ReportViewerViewModel();

        public ReportViewer(SectionReport report)
        {
            InitializeComponent();

            this.viewModel.Report = report;
            DataContext = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Viewer.LoadDocument(this.viewModel.Report);
            Viewer.Zoom = -2;
        }
    }
}
