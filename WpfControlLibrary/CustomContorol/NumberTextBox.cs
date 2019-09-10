using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfControlLibrary.CustomContorol
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfSample.CustomContorol"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfSample.CustomContorol;assembly=WpfSample.CustomContorol"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:NumberTextBox/>
    ///
    /// </summary>
    public class NumberTextBox : TextBox
    {
        public decimal MaxValue { get; set; }

        public decimal MinValue { get; set; }

        public int DecimalPart { get; set; }

        private string beforeText { get; set; }

        static NumberTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberTextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
            
        }

        public NumberTextBox()
        {
            PreviewLostKeyboardFocus += PreviewLostKeyboardFocusAction;
            PreviewTextInput += PreviewTextInputAction;
            TextChanged += TextChangedAction;
        }

        private void PreviewTextInputAction(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == null) return;

            beforeText = textBox.Text;
        }

        private void PreviewLostKeyboardFocusAction(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == null || textBox.Text == null) return;
            if (decimal.TryParse(textBox.Text.TrimEnd('.'), out decimal num) == false) textBox.Text = null;
        }

        private void TextChangedAction(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == null) return;
            if (textBox.Text == "") textBox.Text = null;

            bool yes_parse = IsConfigDataMatch(textBox.Text);

            if (yes_parse == false) textBox.Text = beforeText;
        }

        private bool IsConfigDataMatch(string textBoxText)
        {
            if (textBoxText is null || textBoxText == "-" || textBoxText == "") return true;

            decimal num;
            if (decimal.TryParse(textBoxText.TrimEnd('.'), out num) == false) return false;

            if (num < MinValue) return false;

            if (num > MaxValue) return false;

            var part = textBoxText.Split('.');
            var decimalPart = "";

            if (part.Count() >= 2) decimalPart = part[1];

            if (decimalPart.Length > DecimalPart) return false;

            return true;
        }
    }
}
