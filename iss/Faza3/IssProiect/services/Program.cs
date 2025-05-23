using ServiceTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    // Main program to run all tests
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ServiceImpl Test Suite");
            Console.WriteLine("=====================\n");

            var tests = new ServiceImplTests();
            tests.RunAllTests();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
