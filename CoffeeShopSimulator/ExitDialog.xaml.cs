using System.Windows;

namespace CoffeeShopSimulator
{
    public partial class ExitDialog : Window
    {
        public ExitDialog()
        {
            InitializeComponent();
        }

        private void ConfirmExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; // The user decided to exit
            this.Close();
        }

        private void CancelExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // The user canceled the exit
            this.Close();
        }
    }
}

