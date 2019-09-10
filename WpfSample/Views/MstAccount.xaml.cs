using WpfSample.ViewModel;
using WpfControlLibrary.CustomContorol;

namespace WpfSample.Views
{
    /// <summary>
    /// MstAccount.xaml の相互作用ロジック
    /// </summary>
    public partial class MstAccount : WindowBase
    {
        private MstAccountViewModel mstAccountViewModel = new MstAccountViewModel();

        public MstAccount()
        {
            InitializeComponent();

            DataContext = this.mstAccountViewModel;
        }
    }
}
