using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class SimpleFactories
    {
        //TODO: improve format

        public void ExampleReturn()
        {
            var singleValue = Observable.Return<string>("Value");
            //which could have also been simulated with a replay subject
            var subject = new ReplaySubject<string>();
            subject.OnNext("Value");
            subject.OnCompleted();

            singleValue.Subscribe(Console.WriteLine);
            
            //Value
        }

        public void ExampleEmpty()
        {
            var empty = Observable.Empty<string>();
            //Behaviorally equivalent to
            var subject = new ReplaySubject<string>();
            subject.OnCompleted();

            empty.Subscribe(Console.WriteLine);
        }

        public void ExampleNever()
        {
            var never = Observable.Never<string>();
            //similar to a subject without notifications
            var subject = new Subject<string>();

            never.Subscribe(Console.WriteLine);
        }

        public void ExampleThrow()
        {
            var throws = Observable.Throw<string>(new Exception());
            //Behaviorally equivalent to
            var subject = new ReplaySubject<string>();
            subject.OnError(new Exception());

            throws.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                Console.WriteLine);

            //System.Exception: Exception of type 'System.Exception' was thrown.
        }
    }
}
