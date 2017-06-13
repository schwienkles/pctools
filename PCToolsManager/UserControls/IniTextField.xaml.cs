using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PCToolsManager.UserControls
{
    /// <summary>
    /// Interaction logic for IniField.xaml
    /// </summary>
    public partial class IniTextField : UserControl
    {
        private Ini.Param _iniParam;

        public string Title
        {
            get { return Label.Content as string; }
            private set { Label.Content = value; }
        }

        public Ini.Param IniParam
        {
            get { return _iniParam; }
            set
            {
                _iniParam = value;
                Title = value.Name;
                TextBox.Text = value.Value;
            }
        }

        public IniTextField()
        {
            InitializeComponent();
            TextBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _iniParam.Value = (sender as TextBox)?.Text ?? string.Empty;
        }
    }
}
