using System;
using BenchmarkDotNet.Running;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<PersonBenchmark>();
            Console.ReadLine();
        }
    }
}