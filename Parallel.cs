using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    public class Parallel
    {        
        public static void WaitAll(Action[] delegates)
        {
            List<Task> tasks = new List<Task>();
            Task task;
            foreach (Action del in delegates)
            {
                task = new Task(del);
                tasks.Add(task);
                task.Start();               
            }
            Task.WaitAll(tasks.ToArray());            
        }                
    }
}
