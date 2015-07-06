using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Reducing
{
    class SkipTakeUntil
    {
        public void ExampleSkipUntil()
        {
            var subject = new Subject<int>();
            var otherSubject = new Subject<Unit>();
            subject
                .SkipUntil(otherSubject)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            otherSubject.OnNext(Unit.Default);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnCompleted();

            //4
            //5
            //6
            //7
            //Completed
        }

        public void ExampleTakeUntil()
        {
            var subject = new Subject<int>();
            var otherSubject = new Subject<Unit>();
            subject
                .TakeUntil(otherSubject)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            otherSubject.OnNext(Unit.Default);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnNext(8);
            subject.OnCompleted();

            //1
            //2
            //3
            //Completed
        }
    }
}
