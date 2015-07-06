using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.LeavingTheMonad
{
    class AwaitAsync
    {
        public async void Example()
        {
            var interval = Observable.Interval(TimeSpan.FromSeconds(1));
            // Will block for 3s before returning
            Console.WriteLine(await interval.Take(3));
            Console.WriteLine("Completed");

            //2
            //Completed
        }

        public async void ExampleError()
        {
            var interval = Observable.Empty<int>();
            try
            {
                Console.WriteLine(await interval.Take(3));
                Console.WriteLine("Completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }

            //Error: System.InvalidOperationException: Sequence contains no elements.
        }
    }
}
