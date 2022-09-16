using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int p = 0;
            while (true)
            {
                StreamWriter writer;
                writer = File.AppendText("C:\\Users\\Admin\\Desktop\\writeTest.txt");
                for (int i = 0; i < 10; i++)
                {
                    writer.WriteLine(DateTime.Now);
                }
                writer.Close();
                Console.WriteLine(p);
                p++;
                Thread.Sleep(1000);
            }
        }
    }
}
