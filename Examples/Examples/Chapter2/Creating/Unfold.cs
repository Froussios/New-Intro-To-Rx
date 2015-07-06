using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class UnfoldExample
    {
        private static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
        {
            var nextValue = seed;
            while (true)
            {
                yield return nextValue;
                nextValue = accumulator(nextValue);
            }
        }

        public void Example()
        {
            var naturalNumbers = Unfold(1, i => i + 1);
            Console.WriteLine("1st 10 Natural numbers");
            foreach (var naturalNumber in naturalNumbers.Take(10))
            {
                Console.WriteLine(naturalNumber);
            }

            //1st 10 Natural numbers
            //1
            //2
            //3
            //4
            //5
            //6
            //7
            //8
            //9
            //10
        }
    }
}
