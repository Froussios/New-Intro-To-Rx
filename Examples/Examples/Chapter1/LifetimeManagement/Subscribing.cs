using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class Subscribing
    {
        public void StructuredExceptionHandling()
        {
            var values = new Subject<int>();
            try
            {
                values.Subscribe(value => Console.WriteLine("1st subscription received {0}", value));
            }
            catch (Exception)
            {
                Console.WriteLine("Won't catch anything here!");
            }
            values.OnNext(0);
            //Exception will be thrown here causing the app to fail.
            values.OnError(new Exception("Dummy exception"));
        }

        public void RxExceptionHandling()
        {
            var values = new Subject<int>();
            values.Subscribe(
                value => Console.WriteLine("1st subscription received {0}", value),
                ex => Console.WriteLine("Caught an exception : {0}", ex));
            values.OnNext(0);
            values.OnError(new Exception("Dummy exception"));
        }
    }
}
