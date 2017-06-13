using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Converters;

namespace PCToolsManager.Activities
{
    public class ActivityBatch
    {
        /// <summary>
        /// The friendly name of the current batch
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets wether to actually invoke the batch
        /// </summary>
        public bool Enabled { get; set; }



        /// <summary>
        /// The activities that reside in the current batch
        /// </summary>
        public List<Activity> Activities { get; }

        public ActivityBatch(string name)
        {
            Activities = new List<Activity>();
            Name = name;
        }

        public virtual void Invoke()
        {
            foreach (Activity activity in Activities)
            {
                activity.Invoke();
            }
        }
    }
}
