using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.Scheduling
{
    class FileReader
    {
        string filename = @"test.txt";

        public void ExampleSimple()
        {
            var source = new FileStream(filename, FileMode.Open, FileAccess.Read);
            Func<byte[], int, int, IObservable<int>> factory =
                (b, offset, bSize) =>
                    source.ReadAsync(b, 0, bSize).ToObservable();
            var buffer = new byte[source.Length];
            IObservable<int> reader = factory(buffer, 0, (int)source.Length);
            reader.Subscribe(
                bytesRead =>
                    Console.WriteLine("Read {0} bytes from file into buffer", bytesRead));

            //Read 19 bytes from file into buffer
        }

        public void Example()
        {
            var source = new FileStream(filename, FileMode.Open, FileAccess.Read);
            source.ToObservable(10, Scheduler.Default)
                .Count()
                .Subscribe(
                    bytesRead =>
                        Console.WriteLine("Read {0} bytes from file", bytesRead));
        }
    }

    static class FileStreamExtensions
    {
        private sealed class StreamReaderState
        {
            private readonly int _bufferSize;
            private readonly Func<byte[], int, int, IObservable<int>> _factory;
            public StreamReaderState(FileStream source, int bufferSize)
            {
                _bufferSize = bufferSize;
                _factory = (b, offset, bSize) =>
                    source.ReadAsync(b, 0, bSize).ToObservable();
                Buffer = new byte[bufferSize];
            }
            public IObservable<int> ReadNext()
            {
                return _factory(Buffer, 0, _bufferSize);
            }
            public byte[] Buffer { get; set; }
        }

        public static IObservable<byte> ToObservable(
            this FileStream source,
            int buffersize,
            IScheduler scheduler)
        {
            var bytes = Observable.Create<byte>(o =>
            {
                var initialState = new StreamReaderState(source, buffersize);
                var currentStateSubscription = new SerialDisposable();
                Action<StreamReaderState, Action<StreamReaderState>> iterator =
                (state, self) =>
                    currentStateSubscription.Disposable = state.ReadNext()
                        .Subscribe(
                            bytesRead =>
                            {
                                for (int i = 0; i < bytesRead; i++)
                                {
                                    o.OnNext(state.Buffer[i]);
                                }
                                if (bytesRead > 0)
                                    self(state);
                                else
                                    o.OnCompleted();
                            },
                            o.OnError);
                var scheduledWork = scheduler.Schedule(initialState, iterator);
                return new CompositeDisposable(currentStateSubscription, scheduledWork);
            });
            return Observable.Using(() => source, _ => bytes);
        }
    }
}
