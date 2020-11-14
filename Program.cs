using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало Main");
            Action[] delegates = new Action[] { MyAction, MyAction, MyAction, MyAction, MyAction };
            Parallel.WaitAll(delegates);
            Console.WriteLine("Конец Main");
            Console.ReadLine();
        }

        public static void MyAction()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Сейчас работает поток {threadId}");
            Thread.Sleep(3000);
        }
    }
}
