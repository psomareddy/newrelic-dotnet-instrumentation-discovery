using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestingConsoleApp
{
    class SampleOrderProcessor
    {

        public int MyIntProperty { get; set; }

        public void execute(int i)
        {
            MyIntProperty++;
            Console.WriteLine("Execute method called! " + MyIntProperty);
            Thread.Sleep(200 + (MyIntProperty % 10));
        }

    }
}
