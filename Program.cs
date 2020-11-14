using System;
using System.Collections.Generic;
using System.IO;
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
            var logBuffer = new LogBuffer(3, 2000);
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    logBuffer.Add(i.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }
    }
}
