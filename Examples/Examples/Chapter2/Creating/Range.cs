using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class Range
    {
        public void Example()
        {
            var range = Observable.Range(10, 15);
            range.Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));

            //10
            //11
            //12
            //13
            //14
            //15
            //16
            //17
            //18
            //19
            //20
            //21
            //22
            //23
            //24
        }
    }
}
