using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Utilities.Custom
{
    internal class Task
    {
        WaitCallback dlg_callback;
        object arg;
    }
    public class CoroutinePool
    {
        private const string TAG = "CoroutinePool";
        
        //private List<Task> tasks = new List<Task>();

        //private int max_running = 0, running = 0;

        private static readonly CoroutinePool instance = new CoroutinePool();
        public static CoroutinePool Instance
        {
            get
            {
                return instance;
            }
        }

        public void InitPool(int max_thread_count)
        {
            //max_running = max_thread_count;
        }

        public void QueueTask(WaitCallback callback, object arg)
        {

        }
    }
}
