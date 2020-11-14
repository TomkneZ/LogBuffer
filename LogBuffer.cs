using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;

namespace Lab4
{
    public class LogBuffer
    {
        public ConcurrentQueue<string> MessagesQueue;

        public string LogFileName;

        public int BufferLength;

        public int LogIntervalLength;

        private int IsFlushing;

        private object locker = new object();

        private TimerCallback timeCB;

        private Timer time;

        public LogBuffer(int bufferLength, int intervalLength)
        {
            DateTime now = DateTime.Now;
            LogFileName = $"Logs.txt";
            MessagesQueue = new ConcurrentQueue<string>();
            BufferLength = bufferLength;
            LogIntervalLength = intervalLength;
            timeCB = new TimerCallback(this.AutoFlush);
            time = new Timer(timeCB, null, intervalLength, intervalLength);            
            Add($"Начало логирования {now}");
        }

        public void Add(string item)
        {
            MessagesQueue.Enqueue(item);
            var currentLength = MessagesQueue.Count;
            if (currentLength >= BufferLength)
            {
                Flush();
            }
        }

        public async void Flush()
        {
            if (Interlocked.CompareExchange(ref IsFlushing, 1, 0) == 0)
            {
                DateTime now = DateTime.Now;
                Add($"Сброс логов из буфера в файл {now}");
                var emptyQueue = new ConcurrentQueue<string>();
                var lines = new List<string>();
                lines.AddRange(MessagesQueue);
                MessagesQueue = emptyQueue;
                using (StreamWriter sw = new StreamWriter(LogFileName, true))
                {
                    foreach (string str in lines)
                    {
                        await sw.WriteLineAsync(str);
                    }
                }                
                IsFlushing = 0;
            }            
        }        

        private void AutoFlush(object state)
        {           
            Flush();
        }
    }
}
