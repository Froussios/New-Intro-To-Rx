using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.SequencesOfCoincidence
{
    class Window
    {
        public void Example()
        {
            var windowIdx = 0;
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            source.Window(3)
                .Subscribe(window =>
                {
                    var id = windowIdx++;
                    Console.WriteLine("--Starting new window");
                    var windowName = "Window" + windowIdx;
                    window.Subscribe(
                        value => Console.WriteLine("{0} : {1}", windowName, value),
                        ex => Console.WriteLine("{0} : {1}", windowName, ex),
                        () => Console.WriteLine("{0} Completed", windowName));
                },
                () => Console.WriteLine("Completed"));

            //--Starting new window
            //Window1 : 0
            //Window1 : 1
            //Window1 : 2
            //Window1 Completed
            //--Starting new window
            //Window2 : 3
            //Window2 : 4
            //Window2 : 5
            //Window2 Completed
            //--Starting new window
            //Window3 : 6
            //Window3 : 7
            //Window3 : 8
            //Window3 Completed
            //--Starting new window
            //Window4 : 9
            //Window4 Completed
            //Completed
        }

        public void ExampleManualClosing()
        {
            var windowIdx = 0;
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            var closer = new Subject<Unit>();
            source.Window(() => closer)
                .Subscribe(window =>
                {
                    var thisWindowIdx = windowIdx++;
                    Console.WriteLine("--Starting new window");
                    var windowName = "Window" + thisWindowIdx;
                    window.Subscribe(
                        value => Console.WriteLine("{0} : {1}", windowName, value),
                        ex => Console.WriteLine("{0} : {1}", windowName, ex),
                        () => Console.WriteLine("{0} Completed", windowName));
                },
                () => Console.WriteLine("Completed"));
            var input = "";
            while (input != "exit")
            {
                input = Console.ReadLine();
                closer.OnNext(Unit.Default);
            }

            //--Starting new window
            //window0 : 0
            //window0 : 1
            //window0 Completed
            //--Starting new window
            //window1 : 2
            //window1 : 3
            //window1 : 4
            //window1 : 5
            //window1 Completed
            //--Starting new window
            //window2 : 6
            //window2 : 7
            //window2 : 8
            //window2 : 9
            //window2 Completed
            //Completed
        }
    }
}
