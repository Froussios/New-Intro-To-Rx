using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Inspection
{
    class ElementAt
    {
        public void Example()
        {
            var subject = new Subject<int>();
            subject.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Subject completed"));
            var elementAt1 = subject.ElementAt(1);
            elementAt1.Subscribe(
                b => Console.WriteLine("elementAt1 value: {0}", b),
                () => Console.WriteLine("elementAt1 completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();

            //1
            //2
            //elementAt1 value: 2
            //elementAt1 completed
            //3
            //subject completed
        }
    }
}
