using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class Concat
    {
        public void Example()
        {
            //Generate values 0,1,2
            var s1 = Observable.Range(0, 3);
            //Generate values 5,6,7,8,9
            var s2 = Observable.Range(5, 5);
            s1.Concat(s2)
                .Subscribe(Console.WriteLine);

            //0
            //1
            //2
            //5
            //6
            //7
            //8
            //9
        }

        public void ExampleLaziness()
        {
            Provider.GetSequences().Concat().Dump("Concat");

            //GetSequences() called
            //Yield 1st sequence
            //1st subscribed to
            //Concat-->1
            //Yield 2nd sequence
            //2nd subscribed to
            //Concat-->2
            //Yield 3rd sequence
            //3rd subscribed to
            //Concat-->3
            //GetSequences() complete
            //Concat completed
        }

    }
}
