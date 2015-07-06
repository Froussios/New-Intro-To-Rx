using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Transformation
{
    class TimestampTimeInterval
    {
        public void ExampleTimestamp()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(3)
                .Timestamp()
                .Dump("TimeStamp");

            //TimeStamp-->0@01/01/2012 12:00:01 a.m. +00:00
            //TimeStamp-->1@01/01/2012 12:00:02 a.m. +00:00
            //TimeStamp-->2@01/01/2012 12:00:03 a.m. +00:00
            //TimeStamp completed
        }

        public void ExampleTimeInterval()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(3)
                .TimeInterval()
                .Dump("TimeInterval");

            //TimeInterval-->0@00:00:01.0180000
            //TimeInterval-->1@00:00:01.0010000
            //TimeInterval-->2@00:00:00.9980000
            //TimeInterval completed
        }
    }
}
