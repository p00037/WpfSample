using GrapeCity.ActiveReports;
using System.Collections.Generic;

namespace AcReportBase
{
    public class ReportingServices
    {
        public static void Print<T>(SectionReport report, IList<T> data) where T : new()
        {
            report.DataSource = data;

            var viewer = new ReportViewer(report);
            viewer.ShowDialog();
        }
    }
}
