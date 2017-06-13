using System.Timers;
using Timer = System.Timers.Timer;

namespace PCToolsManager
{
    partial class MainWindow
    {
        private Timer _activityTimer;
        private ActivityManager _activityManager;

        private void CodeConstructor()
        {
            _activityManager = new ActivityManager();

            //fire "Settings.ActivityTimerInterval" seconds
            _activityTimer = new Timer(1000 * Settings.ActivityTimerInterval.Cast<int>());
            _activityTimer.AutoReset = true;
            _activityTimer.Elapsed  += ActivityTimerTick;
            
            _activityTimer.Start();
        }

        private  void ActivityTimerTick(object sender, ElapsedEventArgs e)
        {
            _activityManager.CheckActivities();
        }
    }
}
