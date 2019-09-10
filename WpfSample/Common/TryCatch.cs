using System;
using System.Windows;

namespace WpfSample.Common
{
    public class TryCatch
    {
        public static void ShowMassage(Action action)
        {
            try
            {
                action();
            }
            catch (DoNothingException)
            {
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
