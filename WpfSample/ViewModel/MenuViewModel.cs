using Reactive.Bindings;
using System;
using System.Windows;
using WpfSample.Common;
using WpfSample.Dao;
using WpfSample.Framework;
using WpfSample.Views;

namespace WpfSample.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private Window window;

        public MenuViewModel(Window window)
        {
            this.window = window;

            //LoadedCommand.Subscribe(_ => LoadAction());
            BtnEndCommand.Subscribe(_ => BtnEndAction());
            BtnOpenMstAccountCommand.Subscribe(_ => BtnOpenMstAccountAction());
        }

        public ReactiveCommand BtnOpenMstAccountCommand { get; } = new ReactiveCommand();

        public ReactiveCommand BtnEndCommand { get; } = new ReactiveCommand();

        public ReactiveCommand LoadedCommand { get; } = new ReactiveCommand();

        public void LoadAction()
        {
            TryCatch.ShowMassage(() =>
            {
                LoadEvent();
            });
        }

        public void BtnEndAction()
        {
            TryCatch.ShowMassage(() =>
            {
                Application.Current.Shutdown();
            });
        }

        public void BtnOpenMstAccountAction()
        {
            TryCatch.ShowMassage(() =>
            {
                OpenMstAccountEvent();
            });
        }

        private void LoadEvent()
        {
            var dao = new DaoM_Account();
            if (dao.GetM_AccountList().Count > 0)
            {
                var w = new Login();
                w.Owner = this.window;
                w.ShowDialog();
            }
        }

        private void OpenMstAccountEvent()
        {
            var w = new MstAccount();
            w.Owner = this.window;
            w.ShowDialog();
        }
    }
}
