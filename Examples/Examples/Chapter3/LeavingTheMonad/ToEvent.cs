using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.LeavingTheMonad
{
    class ToEvent
    {
        public void Example()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(5);
            var result = source.ToEvent();
            result.OnNext += val => Console.WriteLine(val);

            //0
            //1
            //2
            //3
            //4
        }

        public class MyEventArgs : EventArgs
        {
            private readonly long _value;
            public MyEventArgs(long value)
            {
                _value = value;
            }
            public long Value
            {
                get { return _value; }
            }
        }

        public void ExampleToEventPattern()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(i => new EventPattern<MyEventArgs>(this, new MyEventArgs(i)));
            var result = source.ToEventPattern();
            result.OnNext += (sender, eventArgs) => Console.WriteLine(eventArgs.Value);

            //0
            //1
            //2
            //3
            //4
            //...
        }
    }
}
