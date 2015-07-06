using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class BehaviorSubjectExample
    {
        public void BehaviorSubjectExample1()
        {
            //Need to provide a default value.
            var subject = new BehaviorSubject<string>("a");
            subject.Subscribe(Console.WriteLine);

            //a
        }

        public void BehaviorSubjectExample2()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            subject.Subscribe(Console.WriteLine);

            //b
        }

        public void BehaviorSubjectExample3()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            subject.Subscribe(Console.WriteLine);
            subject.OnNext("c");
            subject.OnNext("d");

            //b
            //c
            //d
        }

        public void BehaviorSubjectCompletedExample()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            subject.OnNext("c");
            subject.OnCompleted();
            subject.Subscribe(Console.WriteLine);
        }
    }
}
