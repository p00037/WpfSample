using System;
using System.Windows;
using Reactive.Bindings;
using WpfSample.Common;
using WpfSample.Dao;
using WpfSample.Framework;
using WpfSample.Models.DBEntity;

namespace WpfSample.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private Window window;

        public LoginViewModel(Window window)
        {
            this.window = window;

            LoadedCommand.Subscribe(_ => LoadAction());

            BtnLoginCommand.Subscribe(_ => BtnLoginAction());

            BtnCancelCommand.Subscribe(_ => CancelAction());
        }

        public ReactiveProperty<string> ErrorMessage { get; set; } = new ReactiveProperty<string>();

        public ReactiveProperty<M_AccountEntity> Account { get; private set; } = new ReactiveProperty<M_AccountEntity>(new M_AccountEntity());

        public ReactiveCommand LoadedCommand { get; } = new ReactiveCommand();

        public ReactiveCommand BtnLoginCommand { get; } = new ReactiveCommand();

        public ReactiveCommand BtnCancelCommand { get; } = new ReactiveCommand();

        public void LoadAction()
        {
            TryCatch.ShowMassage(() =>
            {
                LoadEvent();
            });
        }

        public void BtnLoginAction()
        {
            TryCatch.ShowMassage(() =>
            {
                LoginEvent();
            });
        }

        private void CancelAction()
        {
            TryCatch.ShowMassage(() =>
            {
                Application.Current.Shutdown();
            });
        }

        private void LoginEvent()
        {
            var dao = new DaoM_Account();
            if (dao.CheckLogin(Account.Value) == false)
            {
                ErrorMessage.Value = "ログインIDまたはパスワードが違います。";
                return;
            }

            OpenMenu();
            window.Close();
        }

        private void LoadEvent()
        {
            var dao = new DaoM_Account();
            if (dao.GetM_AccountList().Count == 0)
            {
                OpenMenu();
            }
        }

        private void OpenMenu()
        {
            var w = new Views.Menu();
            w.Show();
        }
    }
}
