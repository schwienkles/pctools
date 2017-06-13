using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PCToolsManager.UserControls
{
    class IniCheckField : CheckBox
    {
        private Ini.Param _iniParam;

        public Ini.Param IniParam
        {
            get { return _iniParam; }
            set
            {
                _iniParam = value;
                Content = _iniParam.Name;
                IsChecked = _iniParam.Cast<bool>();
            }
        }

        public IniCheckField()
        {
            Checked += CheckedChanged;
            Unchecked += CheckedChanged;
        }

        private void CheckedChanged(object sender, RoutedEventArgs e)
        {
            _iniParam.Value = (sender as CheckBox)?.IsChecked?.ToString() ?? false.ToString();
        }
    }
}
