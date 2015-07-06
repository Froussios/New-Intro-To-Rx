using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Aggregation
{
    public class MinMaxSumAverage
    {
        public void Example()
        {
            var numbers = new Subject<int>();
            numbers.Dump("numbers");
            numbers.Min().Dump("Min");
            numbers.Average().Dump("Average");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();

            //numbers-- > 1
            //numbers-- > 2
            //numbers-- > 3
            //numbers completed
            //Min-- > 1
            //Min completed
            //Average-- > 2
            //Average completed

        }
    }
}
