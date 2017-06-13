using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToolsManager.Activities
{
    public class TimedActivityBatch : ActivityBatch
    {
        /// <summary>
        /// Time to execute at
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Day(s) to execute on
        /// </summary>
        public DayOfWeek Days { get; set; }

        public override void Invoke()
        {
            //activity is not set to launch today
            if (((int) DateTime.Now.DayOfWeek & (int) Days) < 1)
            {
                return;
            }

            DateTime now = DateTime.Now;

            if (now.Hour == Time.Hours && now.Minute == Time.Minutes)
            {
                base.Invoke();
            }
        }

        public TimedActivityBatch(string name) : base(name)
        {
        }
    }
}
