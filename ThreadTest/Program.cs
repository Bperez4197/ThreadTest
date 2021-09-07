using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread threadA = new Thread(ThreadA);
            System.Threading.Thread threadB = new Thread(ThreadB);
            threadA.Start();
            threadB.Start();
            threadAEvent.WaitOne();
            threadBEvent.WaitOne();

            foreach (string entry in entries)
            {
                WriteFile(entry);
            }

            Console.ReadLine();
        }

        static void ThreadA()
        {
            for (int i = 0; i < 10; i++)
            {
                string output = "Thread A." + DateTime.Now;
                entries.Add(output);
            }
            threadAEvent.Set();
        }

        static void ThreadB()
        {
            for (int i = 0; i < 10; i++)
            {
                string output = "Thread B." + DateTime.Now;
                entries.Add(output);
            }
            threadBEvent.Set();
        }

        static void WriteFile(string text)
        {
            using (FileStream fs = new FileStream(@"C:\Users\bryce\OneDrive\C#\C# 2 class\ThreadTest\ThreadTest.txt", FileMode.Append))
            using (StreamWriter sr = new StreamWriter(fs))
            {
                sr.WriteLine(text);
            }
        }

        static List<string> entries = new List<string>();

        static ManualResetEvent threadAEvent = new ManualResetEvent(false);
        static ManualResetEvent threadBEvent = new ManualResetEvent(false);
    }
}
