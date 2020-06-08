using System;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace ReflectionTest
{
    public class PersonBenchmark
    {
        private static readonly Person Person = new Person();

        private static readonly PropertyInfo FullNamePropertyInfo = typeof(Person).GetProperty("FullName")!;

        private static readonly FieldInfo CachedField = typeof(Person).GetField("<FullName>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly Action<Person, string> SetFullNameDelegate = (Action<Person, string>) Delegate.CreateDelegate(
            typeof(Action<Person, string>),
            FullNamePropertyInfo.GetSetMethod(nonPublic: true)!
        );

        [Benchmark]
        public string NonReflectionBenchmark()
        {
            Person.SetFullName("Jerry Tang");
            return Person.FullName;
        }

        [Benchmark]
        public string ReflectionBenchmark()
        {
            typeof(Person).GetProperty("FullName")!.SetValue(Person, "Jerry Tang");
            return Person.FullName;
        }

        [Benchmark]
        public string ReflectionCachedBenchmark()
        {
            FullNamePropertyInfo.SetValue(Person, "Jerry Tang");
            return Person.FullName;
        }

        [Benchmark]
        public string ReflectionCachedFieldBenchmark()
        {
            CachedField.SetValue(Person, "Jerry Tang");
            return Person.FullName;
        }

        [Benchmark]
        public string ReflectionDelegateBenchmark()
        {
            SetFullNameDelegate(Person, "Jerry Tang");
            return Person.FullName;
        }
    }
}