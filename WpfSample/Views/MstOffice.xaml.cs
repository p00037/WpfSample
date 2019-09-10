
using System.Windows;
using WpfControlLibrary.CustomContorol;
using WpfSample.ViewModel;

namespace WpfSample.Views
{
    public partial class MstOffice : WindowBase
    {
        MstOfficeViewModel viewModel = new MstOfficeViewModel();
        public MstOffice()
        {
            InitializeComponent();

            DataContext = this.viewModel;
        }
    }
}