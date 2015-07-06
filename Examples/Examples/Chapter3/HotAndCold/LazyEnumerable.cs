using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.HotAndCold
{
    class LazyEnumerable
    {
        public void ReadFirstValue(IEnumerable<int> list)
        {
            foreach (var i in list)
            {
                Console.WriteLine("Read out first value of {0}", i);
                break;
            }
        }

        public IEnumerable<int> EagerEvaluation()
        {
            var result = new List<int>();
            Console.WriteLine("About to return 1");
            result.Add(1);
            //code below is executed but not used.
            Console.WriteLine("About to return 2");
            result.Add(2);
            return result;
        }

        public void ExampleEager()
        {
            ReadFirstValue(EagerEvaluation());

            //About to return 1
            //About to return 2
            //Read out first value of 1
        }

        public IEnumerable<int> LazyEvaluation()
        {
            Console.WriteLine("About to return 1");
            yield return 1;
            //Execution stops here in this example
            Console.WriteLine("About to return 2");
            yield return 2;
        }

        public void ExampleLazy()
        {
            ReadFirstValue(LazyEvaluation());

            //About to return 1
            //Read out first value of 1
        }

    }
}
