using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.SideEffects
{
    class ExternalState
    {
        public void ExampleWrong()
        {
            var letters = Observable.Range(0, 3)
                .Select(i => (char)(i + 65));
            var index = -1;
            var result = letters.Select(
                c =>
                {
                    index++;
                    return c;
                });
            result.Subscribe(
                c => Console.WriteLine("Received {0} at index {1}", c, index),
                () => Console.WriteLine("completed"));
            result.Subscribe(
                c => Console.WriteLine("Also received {0} at index {1}", c, index),
                () => Console.WriteLine("2nd completed"));

            //Received A at index 0
            //Received B at index 1
            //Received C at index 2
            //completed
            //Also received A at index 3
            //Also received B at index 4
            //Also received C at index 5
            //2nd completed
        }

        public void ExampleSafe()
        {
            var source = Observable.Range(0, 3);
            var result = source.Select(
                (idx, value) => new
                {
                    Index = idx,
                    Letter = (char)(value + 65)
                });
            result.Subscribe(
                x => Console.WriteLine("Received {0} at index {1}", x.Letter, x.Index),
                () => Console.WriteLine("completed"));
            result.Subscribe(
                x => Console.WriteLine("Also received {0} at index {1}", x.Letter, x.Index),
                () => Console.WriteLine("2nd completed"));

            //Received A at index 0
            //Received B at index 1
            //Received C at index 2
            //completed
            //Also received A at index 0
            //Also received B at index 1
            //Also received C at index 2
            //2nd completed
        }
    }
}
