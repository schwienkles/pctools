using System.Reflection;
using System.Windows;

namespace PCToolsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = Assembly.GetEntryAssembly().GetName().Name;
            iniCheckField.IniParam = Settings.CheckedChangedTest;
            IniTextField.IniParam = Settings.ActivityTimerInterval;

            CodeConstructor();
        }
    }
}
