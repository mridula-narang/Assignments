namespace College
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student std1 = new Student(101,"Ramu","12-July-2001", "9876543210","ramu@gmail.com");
            Student std2 = new Student(102, "Harry", "31-July-1975", "8584986749", "harry@gmail.com");
            std1.DisplayId();
            std2.DisplayId();
        }
    }
}
