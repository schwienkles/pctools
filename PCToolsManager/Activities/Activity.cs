using System;
using System.Diagnostics;

namespace PCToolsManager.Activities
{
    /// <summary>
    /// An executable activity for the ActivityManager
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Friendly name of the current activity
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// path to the executable to invoke
        /// </summary>
        public string ExecutablePath { get; set; }

        /// <summary>
        /// Arguments to pass to the invoked executable
        /// </summary>
        public string ExecutionArguments { get; set; }

        

        public Activity(string name)
        {
            Name = name;
        }


        public void Invoke()
        {
            Process process = Process.Start(ExecutablePath, ExecutionArguments);
            process.WaitForExit(Int32.MaxValue);
        }
    }
}