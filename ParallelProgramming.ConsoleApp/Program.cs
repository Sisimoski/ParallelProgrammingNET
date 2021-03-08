using System;
using System.Diagnostics;
using System.Threading;
using ParallelProgramming.Shared;

namespace ParallelProgramming.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Wybór metody całkowania
            Console.WriteLine("Wybierz metodę całkowania:");
            Console.WriteLine("1. Metoda Prostokątów");
            Console.WriteLine("2. Metoda Trapezów");
            Console.WriteLine("3. Metoda Simpsona");
            Console.Write("Wpisz cyfrę: ");
            string input = Console.ReadLine();

            //Wybór początku przedziału
            Console.Write("Podaj początek przedziału całkowania: ");
            var intervalBegin = Convert.ToDouble(Console.ReadLine());

            //Wybór końca przedziału
            Console.Write("Podaj koniec przedziału całkowania: ");
            var intervalEnd = Convert.ToDouble(Console.ReadLine());

            //Wybór dokładności N (iteracje)
            Console.Write("Podaj dokładność N (iteracje): ");
            var nPrecisionValue = Convert.ToInt32(Console.ReadLine());

            //Wybór ilości wątków
            Console.Write("Podaj ilość wątków: ");
            var threadValue = Convert.ToInt32(Console.ReadLine());

            NumericalIntegrationMethods całka = new();

            switch (input)
            {
                case "1":
                    var watch1 = Stopwatch.StartNew();

                    Console.WriteLine("Całka jest równa: {0}", całka.RectangularMethod(intervalBegin, intervalEnd, nPrecisionValue, threadValue));

                    watch1.Stop();

                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts1 = watch1.Elapsed;
                    // Format and display the TimeSpan value.
                    string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts1.Hours, ts1.Minutes, ts1.Seconds,
                        ts1.Milliseconds / 10);

                    var elapsedMs1 = watch1.ElapsedMilliseconds;
                    Console.WriteLine($"Czas wykonania: {elapsedTime1} | {elapsedMs1}ms");

                    break;
                case "2":
                    var watch2 = Stopwatch.StartNew();

                    Console.WriteLine("Całka jest równa: {0}", całka.TrapezoidalIntegration(intervalBegin, intervalEnd, nPrecisionValue, threadValue));

                    watch2.Stop();

                    TimeSpan ts2 = watch2.Elapsed;
                    string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts2.Hours, ts2.Minutes, ts2.Seconds,
                        ts2.Milliseconds / 10);

                    var elapsedMs2 = watch2.ElapsedMilliseconds;
                    Console.WriteLine($"Czas wykonania: {elapsedTime2} | {elapsedMs2}ms");

                    break;
                case "3":
                    var watch3 = Stopwatch.StartNew();

                    Console.WriteLine("Całka jest równa: {0}", całka.SimpsonsIntegration(intervalBegin, intervalEnd, nPrecisionValue, threadValue));

                    watch3.Stop();
                    TimeSpan ts3 = watch3.Elapsed;
                    string elapsedTime3 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts3.Hours, ts3.Minutes, ts3.Seconds,
                        ts3.Milliseconds / 10);

                    var elapsedMs3 = watch3.ElapsedMilliseconds;
                    Console.WriteLine($"Czas wykonania: {elapsedTime3} | {elapsedMs3}ms");

                    break;
                default:
                    Console.WriteLine("Błąd.");
                    break;
            }
        }

        private static void DisplayEnvironmentVariables()
        {
            Console.WriteLine("Environment.CommandLine: " + Environment.CommandLine);
            Console.WriteLine("Environment.CurrentDirectory: " + Environment.CurrentDirectory);
            Console.WriteLine("Environment.CurrentManagedThreadId: "+ Environment.CurrentManagedThreadId);
            Console.WriteLine("Environment.ExitCode: " + Environment.ExitCode);
            Console.WriteLine("Environment.HasShutdownStarted: "+ Environment.HasShutdownStarted);
            Console.WriteLine("Environment.Is64BitOperatingSystem: " + Environment.Is64BitOperatingSystem);
            Console.WriteLine("Environment.Is64BitProcess: " + Environment.Is64BitProcess);
            Console.WriteLine("Environment.MachineName: " + Environment.MachineName);
            Console.WriteLine("Environment.NewLine: " + Environment.NewLine);
            Console.WriteLine("Environment.OSVersion: " + Environment.OSVersion);
            Console.WriteLine("Environment.ProcessId: " + Environment.ProcessId);
            Console.WriteLine("Environment.ProcessorCount: " + Environment.ProcessorCount);
            Console.WriteLine("Environment.StackTrace: " + Environment.StackTrace);
            Console.WriteLine("Environment.SystemDirectory: " + Environment.SystemDirectory);
            Console.WriteLine("Environment.SystemPageSize: " + Environment.SystemPageSize);
            Console.WriteLine("Environment.TickCount: " + Environment.TickCount);
            Console.WriteLine("Environment.TickCount64: " + Environment.TickCount64);
            Console.WriteLine("Environment.UserDomainName: " + Environment.UserDomainName);
            Console.WriteLine("Environment.UserInteractive: " + Environment.UserInteractive);
            Console.WriteLine("Environment.UserName: " + Environment.UserName);
            Console.WriteLine("Environment.Version: " + Environment.Version);
            Console.WriteLine("Environment.WorkingSet: " + Environment.WorkingSet);

            Console.WriteLine("_________________________________________________");
        }

        private void ThreadInfos()
        {
            int workerThreads;
            int portThreads;

            ThreadPool.GetMaxThreads(out workerThreads, out portThreads);
            Console.WriteLine("\nMaximum worker threads: \t{0}" +
                "\nMaximum completion port threads: {1}",
                workerThreads, portThreads);

            ThreadPool.GetAvailableThreads(out workerThreads,
                out portThreads);
            Console.WriteLine("\nAvailable worker threads: \t{0}" +
                "\nAvailable completion port threads: {1}\n",
                workerThreads, portThreads);

            ThreadPool.SetMaxThreads(1000, 32767);
            ThreadPool.SetMinThreads(8, 8);
        }
    }
}
