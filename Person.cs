namespace ReflectionTest
{
    public class Person
    {
        // <FullName>k__BackingField
        public string FullName { get; private set; }

        public void SetFullName(string name)
        {
            FullName = name;
        }
    }
}