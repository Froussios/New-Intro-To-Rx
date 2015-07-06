using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Inspection
{
    class All
    {
        public void Example()
        {
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject completed"));
            var all = subject.All(i => i < 5);
            all.Subscribe(
                b => Console.WriteLine("All values less than 5? {0}", b),
                () => Console.WriteLine("all completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(6);
            subject.OnNext(2);
            subject.OnNext(1);
            subject.OnCompleted();

            //1
            //2
            //6
            //All values less than 5? False
            //all completed
            //2
            //1
            //subject completed
        }
    }
}
