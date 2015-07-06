using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.LeavingTheMonad
{
    class ForEachAsync
    {
        public async void Example()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(5);
            var printing = source.ForEachAsync(i => Console.WriteLine("received {0} @ {1}", i, DateTime.Now));
            await printing; // Wait for every value to be printed
            Console.WriteLine("completed @ {0}", DateTime.Now);

            //received 0 @ 01/01/2012 12:00:01 a.m.
            //received 1 @ 01/01/2012 12:00:02 a.m.
            //received 2 @ 01/01/2012 12:00:03 a.m.
            //received 3 @ 01/01/2012 12:00:04 a.m.
            //received 4 @ 01/01/2012 12:00:05 a.m.
            //completed @ 01/01/2012 12:00:05 a.m.
        }

        public void ExampleSubscribe()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(5);
            source.Subscribe(i => Console.WriteLine("received {0} @ {1}", i, DateTime.Now));
            Console.WriteLine("completed @ {0}", DateTime.Now);

            //completed @ 01/01/2012 12:00:00 a.m.
            //received 0 @ 01/01/2012 12:00:01 a.m.
            //received 1 @ 01/01/2012 12:00:02 a.m.
            //received 2 @ 01/01/2012 12:00:03 a.m.
            //received 3 @ 01/01/2012 12:00:04 a.m.
            //received 4 @ 01/01/2012 12:00:05 a.m.
        }

        public async void ExampleOnError()
        {
            var source = Observable.Throw<int>(new Exception("Fail"));
            try
            {
                await source.ForEachAsync(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error @ {0} with {1}", DateTime.Now, ex.Message);
            }
            finally
            {
                Console.WriteLine("completed @ {0}", DateTime.Now);
            }

            //error @ 01/01/2012 12:00:00 a.m. with Fail
            //completed @ 01/01/2012 12:00:00 a.m.
        }
    }
}
