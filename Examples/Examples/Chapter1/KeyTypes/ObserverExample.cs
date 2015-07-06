using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class ObserverExample
    {
        public class MyConsoleObserver<T> : IObserver<T>
        {
            public void OnNext(T value)
            {
                Console.WriteLine("Received value {0}", value);
            }
            public void OnError(Exception error)
            {
                Console.WriteLine("Sequence faulted with {0}", error);
            }
            public void OnCompleted()
            {
                Console.WriteLine("Sequence terminated");
            }
        }

        public class MySequenceOfNumbers : IObservable<int>
        {
            public IDisposable Subscribe(IObserver<int> observer)
            {
                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnNext(3);
                observer.OnCompleted();
                return Disposable.Empty;
            }
        }

        public void Run()
        {
            var numbers = new MySequenceOfNumbers();
            var observer = new MyConsoleObserver<int>();
            numbers.Subscribe(observer);

            //Received value 1
            //Received value 2
            //Received value 3
            //Sequence terminated
        }
    }
}
