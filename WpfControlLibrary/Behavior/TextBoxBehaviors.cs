using ExtensionsLibrary;
using Newtonsoft.Json;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WpfControlLibrary.Behavior
{
    public class TextBoxBehaviors : Behavior<TextBox>
    {
        private class Config
        {
            public decimal  MinValue { get; set; }

            public decimal MaxValue { get; set; }

            public int DecimalPart { get; set; }
        }

        private static string beforeText { get; set; }

        private static Config ConfigData { get; set; } = new Config();

        /// <summary>
        /// True なら入力を数字のみに制限します。
        /// </summary>
        public static readonly DependencyProperty IsNumConfigProperty =
                    DependencyProperty.RegisterAttached(
                        "NumConfig", typeof(string),
                        typeof(TextBoxBehaviors),
                        new UIPropertyMetadata("", NumConfigChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static string GetNumConfig(DependencyObject obj)
        {
            return (string)obj.GetValue(IsNumConfigProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetNumConfig(DependencyObject obj, string value)
        {
            obj.SetValue(IsNumConfigProperty, value);
        }

        private static void NumConfigChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // イベントを登録・削除 
            textBox.PreviewTextInput -= PreviewTextInput;
            textBox.TextChanged -= TextChanged;
            textBox.PreviewLostKeyboardFocus -= PreviewLostKeyboardFocus;
            var newValue = (string)e.NewValue;

            if (newValue != "")
            {
                var array = newValue.Split(',');
                var configJson = string.Format("{{\"MinValue\":{0},\"MaxValue\":{1},\"DecimalPart\":{2}}}"
                    , array[0].Trim().NullOrEmptyToValue("0")
                    , array[1].Trim().NullOrEmptyToValue("0")
                    , array[2].Trim().NullOrEmptyToValue("0"));
                ConfigData = JsonConvert.DeserializeObject<Config>(configJson);
                
                textBox.PreviewTextInput += PreviewTextInput;
                textBox.TextChanged += TextChanged;
                textBox.PreviewLostKeyboardFocus += PreviewLostKeyboardFocus;
            }
        }

        private static void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == null) return;

            beforeText = textBox.Text;
        }

        private static void TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == null) return;
            if (textBox.Text == "") textBox.Text = null;

            bool yes_parse = IsConfigDataMatch(textBox.Text);

            if (yes_parse == false) textBox.Text = beforeText;
        }

        private static void PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == null || textBox.Text == null) return;
            if (decimal.TryParse(textBox.Text.TrimEnd('.'), out decimal num) == false) textBox.Text = null;
        }

        private static bool IsConfigDataMatch(string textBoxText)
        {
            if (textBoxText is null || textBoxText == "-" || textBoxText == "") return true;

            decimal num;
            if (decimal.TryParse(textBoxText.TrimEnd('.'), out num) == false) return false;

            if (num < ConfigData.MinValue) return false;

            if (num > ConfigData.MaxValue) return false;

            var part = textBoxText.Split('.');
            var decimalPart = "";
            if (decimalPart.Count() >= 2) decimalPart = part[1];

            if (decimalPart.Length > ConfigData.DecimalPart) return false;

            return true;
        }
    }
}
