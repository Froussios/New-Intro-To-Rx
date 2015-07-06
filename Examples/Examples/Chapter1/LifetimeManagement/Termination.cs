using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class Termination
    {
        public void Example()
        {
            var subject = new Subject<int>();
            subject.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
            subject.OnCompleted();
            subject.OnNext(2);

            //Completed
        }
    }
}
