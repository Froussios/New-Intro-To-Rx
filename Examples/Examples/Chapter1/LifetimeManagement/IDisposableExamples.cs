using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    public class TimeIt : IDisposable
    {
        private readonly string _name;
        private readonly Stopwatch _watch;
        public TimeIt(string name)
        {
            _name = name;
            _watch = Stopwatch.StartNew();
        }
        public void Dispose()
        {
            _watch.Stop();
            Console.WriteLine("{0} took {1}", _name, _watch.Elapsed);
        }
    }

    //Creates a scope for a console foreground color. When disposed, will return to
    // the previous Console.ForegroundColor
    public class ConsoleColor : IDisposable
    {
        private readonly System.ConsoleColor _previousColor;
        public ConsoleColor(System.ConsoleColor color)
        {
            _previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }
        public void Dispose()
        {
            Console.ForegroundColor = _previousColor;
        }
    }

    class IDisposableExamples
    {
        private void DoSomeWork(string w)
        {
            switch (w)
            {
                case "B": 
                    Thread.Sleep(TimeSpan.FromMilliseconds(1500));
                    break;
                case "A": 
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    break;
            }
        }

        private void Cleanup()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(300));
        }

        public void ExampleTimer()
        {
            using (new TimeIt("Outer scope"))
            {
                using (new TimeIt("Inner scope A"))
                {
                    DoSomeWork("A");
                }
                using (new TimeIt("Inner scope B"))
                {
                    DoSomeWork("B");
                }
                Cleanup();
            }

            //Inner scope A took 00:00:01.0000000
            //Inner scope B took 00:00:01.5000000
            //Outer scope took 00:00:02.8000000
        }

        public void ExampleConsoleColor()
        {
            Console.WriteLine("Normal color");
            using (new ConsoleColor(System.ConsoleColor.Red))
            {
                Console.WriteLine("Now I am Red");
                using (new ConsoleColor(System.ConsoleColor.Green))
                {
                    Console.WriteLine("Now I am Green");
                }
                Console.WriteLine("and back to Red");
            }
        }

        public void ExampleDisposableUtils()
        {
            var disposable = Disposable.Create(() => Console.WriteLine("Being disposed."));
            Console.WriteLine("Calling dispose...");
            disposable.Dispose();
            Console.WriteLine("Calling again...");
            disposable.Dispose();

            //Calling dispose...
            //Being disposed.
            //Calling again...
        }
    }
}
