using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.SideEffects
{
    class AsObservable
    {
        public class UltraLeakyLetterRepo
        {
            public ReplaySubject<string> Letters { get; set; }
            public UltraLeakyLetterRepo()
            {
                Letters = new ReplaySubject<string>();
                Letters.OnNext("A");
                Letters.OnNext("B");
                Letters.OnNext("C");
            }
        }

        public class LeakyLetterRepo
        {
            private readonly ReplaySubject<string> _letters;
            public LeakyLetterRepo()
            {
                _letters = new ReplaySubject<string>();
                _letters.OnNext("A");
                _letters.OnNext("B");
                _letters.OnNext("C");
            }

            public ReplaySubject<string> Letters
            {
                get { return _letters; }
            }
        }

        public class ObscuredLeakinessLetterRepo
        {
            private readonly ReplaySubject<string> _letters;
            public ObscuredLeakinessLetterRepo()
            {
                _letters = new ReplaySubject<string>();
                _letters.OnNext("A");
                _letters.OnNext("B");
                _letters.OnNext("C");
            }

            public IObservable<string> Letters
            {
                get { return _letters; }
            }
        }

        public class SafeLaterRepo
        {
            private readonly ReplaySubject<string> _letters;
            public SafeLaterRepo()
            {
                _letters = new ReplaySubject<string>();
                _letters.OnNext("A");
                _letters.OnNext("B");
                _letters.OnNext("C");
            }

            public IObservable<string> Letters
            {
                get { return _letters.AsObservable(); }
            }
        }

        public void ExampleObscuredLeakiness()
        {
            var repo = new ObscuredLeakinessLetterRepo();
            var good = repo.Letters;
            var evil = repo.Letters;
            good.Subscribe(
                Console.WriteLine);
            //Be naughty
            var asSubject = evil as ISubject<string>;
            if (asSubject != null)
            {
                //So naughty, 1 is not a letter!
                asSubject.OnNext("1");
            }
            else
            {
                Console.WriteLine("could not sabotage");
            }

            //A
            //B
            //C
            //1
        }

        public void ExampleSafe()
        {
            var repo = new SafeLaterRepo();
            var good = repo.Letters;
            var evil = repo.Letters;
            good.Subscribe(
                Console.WriteLine);
            //Be naughty
            var asSubject = evil as ISubject<string>;
            if (asSubject != null)
            {
                //So naughty, 1 is not a letter!
                asSubject.OnNext("1");
            }
            else
            {
                Console.WriteLine("could not sabotage");
            }

            //A
            //B
            //C
            //could not sabotage

        }
    }
}
