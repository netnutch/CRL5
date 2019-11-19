﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRLTest
{
    class CounterWatch
    {

        long ElapsedMilliseconds;
        double UsedMemory;
        int count;
        string name;
        public void Start(string _name,Action action, int loopCount=1)
        {
            action();
            //GC.Collect();
            count = loopCount;
            name = _name;
            ElapsedMilliseconds = 0;
            UsedMemory = 0;
            var sw = new Stopwatch();
            sw.Start();
            var m1 = GC.GetTotalMemory(false);
            for (int i = 0; i < loopCount; i++)
            {
                action();
            }

            var m2 = GC.GetTotalMemory(false);

            sw.Stop();
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
            UsedMemory = Math.Round((m2 - m1) / 1024.0, 4);
            Console.WriteLine(this.ToString());
            GC.Collect();
        }
        public override string ToString()
        {
            return $"{name} 运行{count}次 用时{ElapsedMilliseconds}ms 内存占用{UsedMemory}kb";
        }
    }
}
