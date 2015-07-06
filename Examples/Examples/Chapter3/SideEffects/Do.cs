using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.SideEffects
{
    class Do
    {
        private static void Log(object onNextValue)
        {
            Console.WriteLine("Logging OnNext({0}) @ {1}", onNextValue, DateTime.Now);
        }
        private static void Log(Exception onErrorValue)
        {
            Console.WriteLine("Logging OnError({0}) @ {1}", onErrorValue, DateTime.Now);
        }
        private static void Log()
        {
            Console.WriteLine("Logging OnCompleted()@ {0}", DateTime.Now);
        }

        public void Example()
        {
            var source = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Take(3);
            var result = source.Do(
                i => Log(i),
                ex => Log(ex),
                () => Log());
            result.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("completed"));

            //Logging OnNext(0) @ 01/01/2012 12:00:00
            //0
            //Logging OnNext(1) @ 01/01/2012 12:00:01
            //1
            //Logging OnNext(2) @ 01/01/2012 12:00:02
            //2
            //Logging OnCompleted() @ 01/01/2012 12:00:02
            //completed
        }

        private static IObservable<long> GetNumbers()
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(250))
                .Do(i => Console.WriteLine("pushing {0} from GetNumbers", i));
        }

        public void ExampleService()
        {
            var source = GetNumbers();
            var result = source.Where(i => i % 3 == 0)
                .Take(3)
                .Select(i => (char)(i + 65));
            result.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("completed"));

            //pushing 0 from GetNumbers
            //A
            //pushing 1 from GetNumbers
            //pushing 2 from GetNumbers
            //pushing 3 from GetNumbers
            //D
            //pushing 4 from GetNumbers
            //pushing 5 from GetNumbers
            //pushing 6 from GetNumbers
            //G
            //completed

        }
    }
}
