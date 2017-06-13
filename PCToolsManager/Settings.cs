using System.IO;
using System.Reflection;
using File = Ini.File;

namespace PCToolsManager
{
    static class Settings
    {
        public static string IniFileName = "settings.ini";
        private static string _sCurrentDirectory;

        #region constants and default values

        private const int DefaultActivityTimerInterval = 60;

        #endregion

        private static readonly Ini.File _sIniFile;

        public static Ini.Param ActivityTimerInterval => _sIniFile["TIMING"][nameof(ActivityTimerInterval)];
        public static Ini.Param CheckedChangedTest    => _sIniFile["TEST"][nameof(CheckedChangedTest)];


        public static string CurrentDirectory
        {
            get
            {
                if (_sCurrentDirectory == null)
                {
                    _sCurrentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                }
                return _sCurrentDirectory;
            }
        }

        static Settings()
        {
            _sIniFile = new File($"{CurrentDirectory}/{IniFileName}");
        }

        public static void Save()
        {
            _sIniFile.Save();
        }
    }
}
