using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Reducing
{
    class SkipTakeLast
    {
        public void ExampleSkipLast()
        {
            var subject = new Subject<int>();
            subject
                .SkipLast(2)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            Console.WriteLine("Pushing 1");
            subject.OnNext(1);
            Console.WriteLine("Pushing 2");
            subject.OnNext(2);
            Console.WriteLine("Pushing 3");
            subject.OnNext(3);
            Console.WriteLine("Pushing 4");
            subject.OnNext(4);
            subject.OnCompleted();

            //Pushing 1
            //Pushing 2
            //Pushing 3
            //1
            //Pushing 4
            //2
            //Completed
        }

        public void ExampleTakeLast()
        {
            var subject = new Subject<int>();
            subject
                .TakeLast(2)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            Console.WriteLine("Pushing 1");
            subject.OnNext(1);
            Console.WriteLine("Pushing 2");
            subject.OnNext(2);
            Console.WriteLine("Pushing 3");
            subject.OnNext(3);
            Console.WriteLine("Pushing 4");
            subject.OnNext(4);
            Console.WriteLine("Completing");
            subject.OnCompleted();

            //Pushing 1
            //Pushing 2
            //Pushing 3
            //Pushing 4
            //Completing
            //3
            //4
            //Completed
        }
    }
}
