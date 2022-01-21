using System;
using System.Threading;

namespace TestingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleOrderProcessor p = new SampleOrderProcessor();
            for (int i =0; i < 5000; i++)
            {
                p.execute(i);
                Thread.Sleep(1000);
            }
        }

    }
}
