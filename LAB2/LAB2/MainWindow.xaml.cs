using System.IO;
using System.Windows;
using System.Windows.Input;

namespace LAB2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save, CanSave));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, Clear));
        }

        private void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(inputTextBox.Text);
        }

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {
            File.WriteAllText("text.txt", inputTextBox.Text);
        }

        private void Open(object sender, ExecutedRoutedEventArgs e)
        {
            if (File.Exists("text.txt"))
                inputTextBox.Text = File.ReadAllText("text.txt");
        }

        private void Clear(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Clear();
        }
    }
}