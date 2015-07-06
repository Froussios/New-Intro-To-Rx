using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class StartWith
    {
        public void Example()
        {
            //Generate values 0,1,2
            var source = Observable.Range(0, 3);
            var result = source.StartWith(-3, -2, -1);
            result.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //-3
            //-2
            //-1
            //0
            //1
            //2
            //Completed
        }
    }
}
