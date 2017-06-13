using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToolsManager.Activities
{
    class SuspendStateActivityBatch : ActivityBatch
    {
        /// <summary>
        /// Gets or Sets wether this activity batch should be invoked when the computer suspends
        /// </summary>
        public bool InvokeOnSuspend { get; set; }

        /// <summary>
        /// Gets or Sets wether this activity batch should be invoked when the computer wakes up (from suspend)
        /// </summary>
        public bool InvokeOnWake { get; set; }


        public SuspendStateActivityBatch(string name) : base(name)
        {
        }
    }
}
