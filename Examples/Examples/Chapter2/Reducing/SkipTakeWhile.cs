using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Reducing
{
    class SkipTakeWhile
    {
        public void ExampleSkip()
        {
            var subject = new Subject<int>();
            subject
                .SkipWhile(i => i < 4)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(3);
            subject.OnNext(2);
            subject.OnNext(1);
            subject.OnNext(0);
            subject.OnCompleted();

            //4
            //3
            //2
            //1
            //0
            //Completed
        }

        public void ExampleTake()
        {
            var subject = new Subject<int>();
            subject
                .TakeWhile(i => i < 4)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(3);
            subject.OnNext(2);
            subject.OnNext(1);
            subject.OnNext(0);
            subject.OnCompleted();

            //1
            //2
            //3
            //Completed
        }
    }
}
