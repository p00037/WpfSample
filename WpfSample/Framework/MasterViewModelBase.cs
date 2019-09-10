using Reactive.Bindings;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfSample.Common;
using WpfSample.Models;
using static WpfSample.Common.AppDbContextControl;
using static WpfSample.Common.DB;

namespace WpfSample.Framework
{
    public abstract class MasterViewModelBase<T> : ViewModelBase where T : new()
    {
        public MasterViewModelBase()
        {
            BtnRegistrationCommand.Subscribe(_ => BtnRegistrationAction());
            BtnNewCommand.Subscribe(_ => BtnNewAction());
            BtnDeleteCommand.Subscribe(_ => BtnDeleteAction());
            BtnReturnCommand.Subscribe(x => BtnReturnAction(x));
            LoadedCommand.Subscribe(_ => LoadAction());
            BtnSearchCommand.Subscribe(_ => SearchAction());
            DataGridMouseDoubleClickComand.Subscribe(_ => DataGridMouseDoubleClickAction());
        }

        public ReactiveProperty<string> ErrorMessage { get; set; } = new ReactiveProperty<string>("");

        public ReactiveProperty<int> ErrorMessageBorderThickness { get; set; } = new ReactiveProperty<int>(0);

        public ReactiveProperty<Brush> ErrorMessageBackColor { get; set; } = new ReactiveProperty<Brush>(new SolidColorBrush(Colors.Transparent));

        public ReactiveProperty<bool> IsPrimaryKeyEnabled { get; set; } = new ReactiveProperty<bool>(true);

        public ReactiveProperty<bool> IsBtnDeleteEnabled { get; set; } = new ReactiveProperty<bool>(true);

        public ReactiveProperty<T> SelectedItem { get; set; } = new ReactiveProperty<T>(new T());

        public ReactiveCommand BtnSearchCommand { get; } = new ReactiveCommand();

        public ReactiveCommand DataGridMouseDoubleClickComand { get; } = new ReactiveCommand();

        public ReactiveCommand LoadedCommand { get; } = new ReactiveCommand();

        public ReactiveCommand BtnRegistrationCommand { get; } = new ReactiveCommand();

        public ReactiveCommand BtnNewCommand { get; } = new ReactiveCommand();

        public ReactiveCommand BtnDeleteCommand { get; } = new ReactiveCommand();

        public ReactiveCommand<object> BtnReturnCommand { get; } = new ReactiveCommand<object>();

        protected ComEnum.EnmEditMode EditMode { get; set; } = ComEnum.EnmEditMode.Insert;

        private string JsonBeforeEditData { get; set; }

        public void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                var grd = (DataGrid)sender;
                grd.BeginEdit(e);
            }
        }

        public void LoadAction()
        {
            TryCatch.ShowMassage(() =>
            {
                LoadEvent();
            });
        }

        public void SearchAction()
        {
            TryCatch.ShowMassage(() =>
            {
                SetSearchResultEntitys();
            });
        }

        public void DataGridMouseDoubleClickAction()
        {
            TryCatch.ShowMassage(() =>
            {
                ChangeEditData();
            });
        }

        public void BtnRegistrationAction()
        {
            TryCatch.ShowMassage(() =>
            {
                SaveEvent();
            });
        }

        public void BtnNewAction()
        {
            TryCatch.ShowMassage(() =>
            {
                ChangeNewDataEvent();
            });
        }       

        public void BtnDeleteAction()
        {
            TryCatch.ShowMassage(() =>
            {
                DeleteEvent();
            });
        }
        
        public void BtnReturnAction(object view)
        {
            TryCatch.ShowMassage(() => 
            {
                ((Window)view).Close();
            });
        }

        protected void ChangeEditModeToInsert()
        {
            SetErrorMessage("");
            IsPrimaryKeyEnabled.Value = true;
            IsBtnDeleteEnabled.Value = false;
            this.EditMode = ComEnum.EnmEditMode.Insert;
            SetEditDataToInsert();
            SetBeforeEditData();
        }

        protected void ChangeEditModeToUpdate(T selectEntity)
        {
            SetErrorMessage("");
            IsPrimaryKeyEnabled.Value = false;
            IsBtnDeleteEnabled.Value = true;
            this.EditMode = ComEnum.EnmEditMode.Update;
            SetEditDataToUpdate(selectEntity);
            SetBeforeEditData();
        }

        protected void SetErrorMessage(string errorMessage)
        {
            if (errorMessage == "")
            {
                ErrorMessage.Value = "";
                ErrorMessageBorderThickness.Value = 0;
                ErrorMessageBackColor.Value = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                ErrorMessage.Value = errorMessage;
                ErrorMessageBorderThickness.Value = 1;
                ErrorMessageBackColor.Value = new SolidColorBrush(Colors.White);
            }
        }

        protected bool CheckChangeData()
        {
            if (this.JsonBeforeEditData == GetJsonEditData())
            {
                return false;
            }

            return true;
        }

        protected void SetBeforeEditData()
        {
            this.JsonBeforeEditData = GetJsonEditData();
        }

        protected abstract void Load();

        protected abstract void Save();

        protected abstract void Delete();

        protected abstract void SetEditDataToInsert();

        protected abstract void SetEditDataToUpdate(T selectEntity);

        protected abstract void SetSearchResultEntitys();

        protected abstract string GetJsonEditData();

        private void LoadEvent()
        {
            SetSearchResultEntitys();
            Load();
            ChangeEditModeToInsert();
        }

        private void SaveEvent()
        {
            try
            {
                SetErrorMessage("");

                ExecSaveChanges(() => 
                {
                    Save();
                });

                SetSearchResultEntitys();
                ChangeEditModeToInsert();
                MessageBox.Show(ComMessage.RESISTERED);
            }
            catch (SaveErrorMessageExcenption e)
            {
                SetErrorMessage(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteEvent()
        {
            using (AppDbContext contexForSave = CreateAppDbContextForSave(true))
            {
                Delete();
                contexForSave.SaveChanges();
            }

            SetSearchResultEntitys();
            ChangeEditModeToInsert();
            MessageBox.Show(ComMessage.DELETED);
        }

        private void ChangeNewDataEvent()
        {
            ChangeEditModeToInsert();
        }

        private void ChangeEditData()
        {
            if (SelectedItem.Value == null) return;

            if (ConfirmChangEditData() == false) return;

            ChangeEditModeToUpdate(SelectedItem.Value);
        }

        private bool ConfirmChangEditData()
        {
            if (CheckChangeData() == false) return true;

            if (MessageBox.Show(ComMessage.CONFIM_CHANGE_DATA, "変更確認", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                return true;
            }

            return false;
        }
    }
}
