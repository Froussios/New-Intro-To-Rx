using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Inspection
{
    class Any
    {
        public void Example1()
        {
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject completed"));
            var any = subject.Any();
            any.Subscribe(b => Console.WriteLine("The subject has any values? {0}", b));
            subject.OnNext(1);
            subject.OnCompleted();

            //1
            //The subject has any values? True
            //subject completed
        }

        public void Example2()
        {
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject completed"));
            var any = subject.Any();
            any.Subscribe(b => Console.WriteLine("The subject has any values? {0}", b));
            //subject.OnNext(1);
            subject.OnCompleted();

            //subject completed
            //The subject has any values? False
        }

        public void Example3()
        {
            var subject = new Subject<int>();
            subject.Subscribe(
                Console.WriteLine,
                ex => Console.WriteLine("subject OnError : {0}", ex),
                () => Console.WriteLine("Subject completed"));
            var any = subject.Any();
            any.Subscribe(
                b => Console.WriteLine("The subject has any values? {0}", b),
                ex => Console.WriteLine(".Any() OnError : {0}", ex),
                () => Console.WriteLine(".Any() completed"));
            subject.OnError(new Exception());

            //subject OnError : System.Exception: Fail
            //.Any() OnError: System.Exception: Fail
        }
    }
}
