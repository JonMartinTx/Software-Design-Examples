using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Software_Design_Examples.View_Model;
using Software_Design_Examples.View_Model.UsefulExtensions;

namespace Software_Design_Examples.Views
{
    /// <summary>
    /// Interaction logic for WpfMessageBox.xaml
    /// </summary>
    public partial class WpfMessageBox : Window
    {
        public WpfMessageBox()
        {
            InitializeComponent();
        }

        private static PurchaseViewModel? _vm;
        private static BitmapImage _background;
        static WpfMessageBox _messageBox;
        static MessageBoxResult _result = MessageBoxResult.No;
        public static MessageBoxResult Show
        (string caption, string msg, UsefulExtensions.MessageBoxType type)
        {
            switch (type)
            {
                case UsefulExtensions.MessageBoxType.ConfirmationWithYesNo:
                    return Show(caption, msg, MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                case UsefulExtensions.MessageBoxType.ConfirmationWithYesNoCancel:
                    return Show(caption, msg, MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);
                case UsefulExtensions.MessageBoxType.Information:
                    return Show(caption, msg, MessageBoxButton.OK,
                    MessageBoxImage.Information);
                case UsefulExtensions.MessageBoxType.Error:
                    return Show(caption, msg, MessageBoxButton.OK,
                    MessageBoxImage.Error);
                case UsefulExtensions.MessageBoxType.Warning:
                    return Show(caption, msg, MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                default:
                    return MessageBoxResult.No;
            }
        }
        public static MessageBoxResult Show(string msg, UsefulExtensions.MessageBoxType type)
        {
            return Show(string.Empty, msg, type);
        }
        public static MessageBoxResult Show(string msg)
        {
            return Show(string.Empty, msg,
            MessageBoxButton.OK, MessageBoxImage.None);
        }
        public static MessageBoxResult Show
        (string caption, string text)
        {
            return Show(caption, text,
            MessageBoxButton.OK, MessageBoxImage.None);
        }
        public static MessageBoxResult Show
        (string caption, string text, MessageBoxButton button)
        {
            return Show(caption, text, button,
            MessageBoxImage.None);
        }
        public static MessageBoxResult Show
        (string caption, string text,
        MessageBoxButton button, MessageBoxImage image)
        {
            _messageBox = new WpfMessageBox
            { txtMsg = { Text = text }, MessageTitle = { Text = caption } };
            SetVisibilityOfButtons(button);
            SetImageOfMessageBox(image);
            _messageBox.ShowDialog();
            return _result;
        }

        public static MessageBoxResult Show
        (string caption, string text,
            MessageBoxButton button, MessageBoxImage image, BitmapImage background, ref PurchaseViewModel? vm)
        {
            _vm = vm;
            _background = background;
            _messageBox = new WpfMessageBox
                { txtMsg = { Text = text }, MessageTitle = { Text = caption } };
            SetVisibilityOfButtons(button);
            _messageBox.BackgroundImage.ImageSource = _background;
            _messageBox.ShowDialog();
            return _result;
        }
        private static void SetVisibilityOfButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    _messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    _messageBox.btnNo.Visibility = Visibility.Collapsed;
                    _messageBox.btnYes.Visibility = Visibility.Collapsed;
                    _messageBox.btnOk.Focus();
                    break;
                case MessageBoxButton.OKCancel:
                    _messageBox.btnNo.Visibility = Visibility.Collapsed;
                    _messageBox.btnYes.Visibility = Visibility.Collapsed;
                    _messageBox.btnCancel.Focus();
                    break;
                case MessageBoxButton.YesNo:
                    _messageBox.btnOk.Visibility = Visibility.Collapsed;
                    _messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    _messageBox.btnNo.Focus();
                    break;
                case MessageBoxButton.YesNoCancel:
                    _messageBox.btnOk.Visibility = Visibility.Collapsed;
                    _messageBox.btnCancel.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private static void SetImageOfMessageBox(MessageBoxImage image)
        {
            switch (image)
            {
                case MessageBoxImage.Warning:
                    _messageBox.SetImage("Warning.png");
                    break;
                case MessageBoxImage.Question:
                    _messageBox.SetImage("Question.png");
                    break;
                case MessageBoxImage.Information:
                    _messageBox.SetImage("Information.png");
                    break;
                case MessageBoxImage.Error:
                    _messageBox.SetImage("Error.png");
                    break;
                default:
                    _messageBox.img.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnOk)
                _result = MessageBoxResult.OK;
            else if (sender == btnYes)
                _result = MessageBoxResult.Yes;
            else if (sender == btnNo)
                _result = MessageBoxResult.No;
            else if (sender == btnCancel)
                _result = MessageBoxResult.Cancel;
            else
                _result = MessageBoxResult.None;
            _messageBox.Close();
            _messageBox = null;
        }
        private void SetImage(string imageName)
        {
            string uri = string.Format("/Resources/images/{0}", imageName);
            var uriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            img.Source = new BitmapImage(uriSource);
        }
    }
}
