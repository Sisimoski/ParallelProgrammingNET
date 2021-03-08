using System;
using System.Diagnostics;
using System.Threading;
using ParallelProgramming.Shared;

namespace ParallelProgramming.ConsoleApp
{
    class Program
    {
        public static string IntegrationMethod { get; set; }
        public static double IntervalBegin { get; set; }
        public static double IntervalEnd { get; set; }
        public static int NPrecisionValue { get; set; }
        public static bool IsParallel { get; set; }
        public static int ThreadValue { get; set; }
        public static Stopwatch Stopwatch { get; set; }
        public static double Result { get; set; }

        static void Main(string[] args)
        {
            IntegrationMethod = ReadUserInput();

            NumericalIntegrationMethods integral = new();

            switch (IntegrationMethod)
            {
                case "1":
                    Stopwatch = Stopwatch.StartNew();

                    if (IsParallel is true)
                    {
                        Result = integral.RectangularIntegrationParallel(IntervalBegin, IntervalEnd, NPrecisionValue, ThreadValue);
                    } else
                    {
                        Result = integral.RectangularIntegration(IntervalBegin, IntervalEnd, NPrecisionValue);
                    }

                    Console.WriteLine("Całka jest równa: {0}", Result);

                    Stopwatch.Stop();
                    GetElapsedTime(Stopwatch);
                    break;
                case "2":
                    Stopwatch = Stopwatch.StartNew();

                    if (IsParallel is true)
                    {
                        Result = integral.TrapezoidalIntegrationParallel(IntervalBegin, IntervalEnd, NPrecisionValue, ThreadValue);
                    }
                    else
                    {
                        Result = integral.TrapezoidalIntegration(IntervalBegin, IntervalEnd, NPrecisionValue);
                    }

                    Console.WriteLine("Całka jest równa: {0}", Result);

                    Stopwatch.Stop();
                    GetElapsedTime(Stopwatch);
                    break;
                case "3":
                    Stopwatch = Stopwatch.StartNew();

                    if (IsParallel is true)
                    {
                        Result = integral.SimpsonsIntegrationParallel(IntervalBegin, IntervalEnd, NPrecisionValue, ThreadValue);
                    }
                    else
                    {
                        Result = integral.SimpsonsIntegration(IntervalBegin, IntervalEnd, NPrecisionValue);
                    }

                    Console.WriteLine("Całka jest równa: {0}", Result);

                    Stopwatch.Stop();
                    GetElapsedTime(Stopwatch);
                    break;
                default:
                    Console.WriteLine("Błąd.");
                    break;
            }
        }

        private static string ReadUserInput()
        {
            //Wybór metody całkowania
            Console.WriteLine("Wybierz metodę całkowania:");
            Console.WriteLine("1. Metoda Prostokątów");
            Console.WriteLine("2. Metoda Trapezów");
            Console.WriteLine("3. Metoda Simpsona");
            Console.Write("Wpisz cyfrę: ");
            ConsoleKeyInfo integrationMethodInput = Console.ReadKey();
            string input = integrationMethodInput.KeyChar.ToString();

            Console.WriteLine(String.Empty);

            //Wybór początku przedziału
            Console.Write("Podaj początek przedziału całkowania: ");
            IntervalBegin = Convert.ToDouble(Console.ReadLine());

            //Wybór końca przedziału
            Console.Write("Podaj koniec przedziału całkowania: ");
            IntervalEnd = Convert.ToDouble(Console.ReadLine());

            //Wybór dokładności N (iteracje)
            Console.Write("Podaj dokładność N (iteracje): ");
            NPrecisionValue = Convert.ToInt32(Console.ReadLine());

            //Wybór, czy ma się wykonywać sekwencyjnie czy współbieżnie
            Console.Write("Czy obliczenie ma się wykonać sekwencyjnie (jednowątkowo) czy współbieżnie (wielowątkowo)? T(wielowątkowo) / N(jednowątkowo): ");
            ConsoleKeyInfo parallelInput = Console.ReadKey();
            string pInput = parallelInput.KeyChar.ToString().ToUpper();
            IsParallel = pInput == "T" ? true : false;

            Console.WriteLine(String.Empty);

            if (IsParallel is true)
            {
                //Wybór ilości wątków
                Console.Write("Podaj ilość wątków: ");
                ThreadValue = Convert.ToInt32(Console.ReadLine());
            }

            return input;
        }

        private static void GetElapsedTime(Stopwatch stopwatch)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan timeSpan = stopwatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
                timeSpan.Milliseconds / 10);

            var elapsedMs = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Czas wykonania: {elapsedTime} | {elapsedMs}ms");
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
