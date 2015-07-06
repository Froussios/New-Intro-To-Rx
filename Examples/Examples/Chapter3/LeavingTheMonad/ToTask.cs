using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.LeavingTheMonad
{
    class ToTask
    {
        public void Example()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(5);
            var result = source.ToTask(); //Will arrive in 5 seconds.
            Console.WriteLine(result.Result);

            //4
        }

        public void ExampleOnError()
        {
            var source = Observable.Throw<long>(new Exception("Fail!"));
            var result = source.ToTask();
            try
            {
                Console.WriteLine(result.Result);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerException.Message);
            }

            //Fail!
        }
    }
}
