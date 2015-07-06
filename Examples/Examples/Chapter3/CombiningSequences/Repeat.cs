using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class Repeat
    {
        public void Example()
        {
            var source = Observable.Range(0, 3);
            var result = source.Repeat(3);
            result.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //0
            //1
            //2
            //0
            //1
            //2
            //0
            //1
            //2
            //Completed
        }
    }
}
