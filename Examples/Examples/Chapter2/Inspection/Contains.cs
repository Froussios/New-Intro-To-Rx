using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Inspection
{
    class Contains
    {
        public void Example()
        {
            var subject = new Subject<int>();
            subject.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Subject completed"));
            var contains = subject.Contains(2);
            contains.Subscribe(
                b => Console.WriteLine("Contains the value 2? {0}", b),
                () => Console.WriteLine("contains completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();

            //1
            //2
            //Contains the value 2 ? True
            //contains completed
            //3
            //Subject completed
        }
    }
}
