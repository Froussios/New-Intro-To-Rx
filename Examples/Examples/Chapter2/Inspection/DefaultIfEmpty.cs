using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Inspection
{
    class DefaultIfEmpty
    {
        public void Example1()
        {
            var subject = new Subject<int>();
            subject.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Subject completed"));
            var defaultIfEmpty = subject.DefaultIfEmpty();
            defaultIfEmpty.Subscribe(
                b => Console.WriteLine("defaultIfEmpty value: {0}", b),
                () => Console.WriteLine("defaultIfEmpty completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();

            //1
            //defaultIfEmpty value: 1
            //2
            //defaultIfEmpty value: 2
            //3
            //defaultIfEmpty value: 3
            //Subject completed
            //defaultIfEmpty completed
        }

        public void Example2()
        {
            var subject = new Subject<int>();
            subject.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Subject completed"));
            var defaultIfEmpty = subject.DefaultIfEmpty();
            defaultIfEmpty.Subscribe(
                b => Console.WriteLine("defaultIfEmpty value: {0}", b),
                () => Console.WriteLine("defaultIfEmpty completed"));
            var default42IfEmpty = subject.DefaultIfEmpty(42);
            default42IfEmpty.Subscribe(
                b => Console.WriteLine("default42IfEmpty value: {0}", b),
                () => Console.WriteLine("default42IfEmpty completed"));
            subject.OnCompleted();

            //Subject completed
            //defaultIfEmpty value: 0
            //defaultIfEmpty completed
            //default42IfEmpty value: 42
            //default42IfEmpty completed
        }
    }
}
