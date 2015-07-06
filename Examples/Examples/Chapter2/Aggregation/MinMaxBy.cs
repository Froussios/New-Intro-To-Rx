using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace IntroToRx.Examples.Chapter2.Aggregation
{
    class MinMaxBy
    {
        public void Example()
        {
            var data = new String[] 
            {
                "First",
                "Second",
                "Third"
            };
            var source = data.ToObservable();

            source.MaxBy(s => s.Length)
                .Subscribe(vs => 
                {
                    Console.Write("MaxBy: ");
                    foreach (var item in vs)
                        Console.Write(item + ", ");
                    Console.WriteLine();
                });

            source.MinBy(s => s.Length)
                .Subscribe(vs =>
                {
                    Console.Write("MinxBy: ");
                    foreach (var item in vs)
                        Console.Write(item + ", ");
                    Console.WriteLine();
                });

            //MaxBy: Second,
            //MinxBy: First, Third,
        }
    }
}
