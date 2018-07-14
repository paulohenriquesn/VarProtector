using System;
using VarProtector;

class Program
    {
        static void Main(string[] args)
        {
            varProtector vp = new varProtector();

            vp.makeValue(99999);

            Console.WriteLine($"Value: {vp.getValue(0)}");

            while (true)
            {
                Task memoryProtect = Task.Run(()=>vp.memoryProtect(0));
                Thread.Sleep(900);
                vp.changeValue(0, (vp.getValue(0)+1));
                Console.WriteLine("Value: " + vp.getValue(0));             
            }
        }
    }
