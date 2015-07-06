using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Aggregation
{
    class Count
    {
        public void Example()
        {
            var numbers = Observable.Range(0, 3);
            numbers.Dump("numbers");
            numbers.Count().Dump("count");

            //numbers-- > 1
            //numbers-- > 2
            //numbers-- > 3
            //numbers Completed
            //count-- > 3
            //count Completed
        }
    }
}
