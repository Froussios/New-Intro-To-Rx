using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    /// <summary>
    /// A provider for a simple example case
    /// </summary>
    static class Provider
    {
        public static IEnumerable<IObservable<long>> GetSequences()
        {
            Console.WriteLine("GetSequences() called");
            Console.WriteLine("Yield 1st sequence");
            yield return Observable.Create<long>(o =>
            {
                Console.WriteLine("1st subscribed to");
                return Observable.Timer(TimeSpan.FromMilliseconds(500))
                    .Select(i => 1L)
                    .Subscribe(o);
            });
            Console.WriteLine("Yield 2nd sequence");
            yield return Observable.Create<long>(o =>
            {
                Console.WriteLine("2nd subscribed to");
                return Observable.Timer(TimeSpan.FromMilliseconds(300))
                    .Select(i => 2L)
                    .Subscribe(o);
            });
            Thread.Sleep(1000); //Force a delay
            Console.WriteLine("Yield 3rd sequence");
            yield return Observable.Create<long>(o =>
            {
                Console.WriteLine("3rd subscribed to");
                return Observable.Timer(TimeSpan.FromMilliseconds(100))
                    .Select(i => 3L)
                    .Subscribe(o);
            });
            Console.WriteLine("GetSequences() complete");
        }
    }
}
