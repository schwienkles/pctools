using System;
using System.Collections.Generic;
using PCToolsManager.Activities;

namespace PCToolsManager
{
    public class ActivityManager
    {
        private List<TimedActivityBatch> _activities;

        public ActivityManager()
        {
            _activities = new List<TimedActivityBatch>();
        }

        public void CheckActivities()
        {
            foreach (TimedActivityBatch batch in _activities)
            {
                batch.Invoke();
            }
        }

        /// <summary>
        /// Adds a new activity
        /// </summary>
        /// <param name="batch">Activity batch to add</param>
        /// <returns>true on success (false if it already exists)</returns>
        public bool AddBatch(TimedActivityBatch batch)
        {
            if (batch == null)
            {
                throw new ArgumentNullException(nameof(batch));
            }

            if (_activities.Contains(batch))
            {
                return false;
            }

            _activities.Add(batch);
            return true;
        }
    }
}
