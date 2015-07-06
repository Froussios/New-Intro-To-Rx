using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace IntroToRx.Examples.Chapter2.Transformation
{
    public class CastOfType
    {
        public void ExampleCast1()
        {
            var objects = new Subject<object>();
            objects.Cast<int>().Dump("cast");
            objects.OnNext(1);
            objects.OnNext(2);
            objects.OnNext(3);
            objects.OnCompleted();

            //cast-->1
            //cast-->2
            //cast-->3
            //cast completed
        }

        public void ExampleCastError()
        {
            var objects = new Subject<object>();
            objects.Cast<int>().Dump("cast");
            objects.OnNext(1);
            objects.OnNext(2);
            objects.OnNext("3");//Fail

            //cast-->1
            //cast-->2
            //cast failed --> Specified cast is not valid.
        }

        public void ExampleOfType()
        {
            var objects = new Subject<object>();
            objects.OfType<int>().Dump("OfType");
            objects.OnNext(1);
            objects.OnNext(2);
            objects.OnNext("3");//Ignored
            objects.OnNext(4);
            objects.OnCompleted();

            //OfType-->1
            //OfType-->2
            //OfType-->4
            //OfType completed
        }


    }
}