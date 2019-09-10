using System.Windows;
using System.Windows.Controls;
using WpfSample.Common;
using WpfSample.Views;

namespace WpfSample.UserControls
{
    /// <summary>
    /// CustomMunu.xaml の相互作用ロジック
    /// </summary>
    public partial class CustomMenu : UserControl
    {
        public static readonly DependencyProperty ThisWindowProperty =
        DependencyProperty.Register("ThisWindow",
                                    typeof(object),
                                    typeof(CustomMenu),
                                    new FrameworkPropertyMetadata("Window", new PropertyChangedCallback(OnThisWindowChanged)));

        public CustomMenu()
        {
            InitializeComponent();
        }

        // 2. CLI用プロパティを提供するラッパー
        public Window ThisWindow
        {
            get { return (Window)GetValue(ThisWindowProperty); }
            set { SetValue(ThisWindowProperty, value); }
        }

        private static void OnThisWindowChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
        }

        private void MenuItemStartupScreen_Click(object sender, RoutedEventArgs e)
        {
            TryCatch.ShowMassage(() =>
            {
                OpenMenuEvent();
            });
        }

        private void MenuItemMstAccount_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TryCatch.ShowMassage(() =>
            {
                OpenMstAccountEvent();
            });
        }

        private void MenuItemMstOffice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TryCatch.ShowMassage(() =>
            {
                OpenMstOfficeEvent();
            });
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            TryCatch.ShowMassage(() =>
            {
                Application.Current.Shutdown();
            });
        }

        private void OpenMstAccountEvent()
        {
            WindowShow(new MstAccount());
        }

        private void OpenMstOfficeEvent()
        {
            WindowShow(new MstOffice());
        }

        private void OpenMenuEvent()
        {
            WindowShow(new Views.Menu());
        }

        private void WindowShow(Window w)
        {
            w.Show();
            ThisWindow.Close();
        }
    }
}
