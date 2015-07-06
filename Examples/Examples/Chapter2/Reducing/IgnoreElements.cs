using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Reducing
{
    class IgnoreElements
    {
        public void Example()
        {
            var subject = new Subject<int>();
            //Could use subject.Where(_=>false);
            var noElements = subject.IgnoreElements();
            subject.Subscribe(
                i => Console.WriteLine("subject.OnNext({0})", i),
                () => Console.WriteLine("subject.OnCompleted()"));
            noElements.Subscribe(
                i => Console.WriteLine("noElements.OnNext({0})", i),
                () => Console.WriteLine("noElements.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();

            //subject.OnNext(1)
            //subject.OnNext(2)
            //subject.OnNext(3)
            //subject.OnCompleted()
            //noElements.OnCompleted()
        }
    }
}
