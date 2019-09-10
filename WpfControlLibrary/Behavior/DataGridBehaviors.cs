using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WpfControlLibrary.Behavior
{
    public class DataGridBehaviors : Behavior<DataGrid>
    {
        /// <summary>
        /// Trueなら即入力
        /// </summary>
        public static readonly DependencyProperty IsInputProperty =
                    DependencyProperty.RegisterAttached(
                        "IsInput", typeof(bool),
                        typeof(DataGridBehaviors),
                        new UIPropertyMetadata(false, IsInputChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static bool GetIsInput(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsInputProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static void SetIsInput(DependencyObject obj, bool value)
        {
            obj.SetValue(IsInputProperty, value);
        }

        private static void IsInputChanged
            (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null) return;

            // イベントを登録・削除 
            dataGrid.GotFocus -= DataGrid_GotFocus;
            var newValue = (bool)e.NewValue;
            if (newValue)
            {
                dataGrid.GotFocus += DataGrid_GotFocus;
            }
        }

        static void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                var grd = (DataGrid)sender;
                grd.BeginEdit(e);
                //grd.SelectedItem = grd.DataContext; 
            }
        }
    }
}
