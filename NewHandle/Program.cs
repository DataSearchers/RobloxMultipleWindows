﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewHandle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Mutex(true, "ROBLOX_singletonMutex");
            Console.Read();
        }
    }
}
