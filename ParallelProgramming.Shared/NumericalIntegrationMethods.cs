using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.Shared
{
    public class NumericalIntegrationMethods
    {
        double Function(double x)
        {
            return x * x + 2 * x;
        }

        public double RectangularIntegration(double xp, double xk, int n)
        {
            double dx, integral = 0;
            dx = (xk - xp) / n;

            for (int i = 1; i <= n; i++)
            {
                integral += dx * Function(xp + i * dx);
                Console.WriteLine("iteracja {0} wątek {1}", i, Thread.CurrentThread.ManagedThreadId);
            }

            return integral;
        }

        public double TrapezoidalIntegration(double xp, double xk, int n)
        {
            double dx, integral = 0;
            dx = (xk - xp) / n;

            for (int i = 1; i <= n; i++)
            {
                integral += Function(xp + i * dx);
                Console.WriteLine("iteracja {0} wątek {1}", i, Thread.CurrentThread.ManagedThreadId);
            }

            integral += (Function(xp) + Function(xk)) / 2;
            integral *= dx;

            return integral;
        }

        public double SimpsonsIntegration(double xp, double xk, int n)
        {
            double dx, integral, s, x;
            integral = 0;
            s = 0;

            dx = (xk - xp) / n;

            for (int i = 1; i <= n; i++)
            {
                x = xp + i * dx;
                s += Function(x - dx / 2);
                integral += Function(x);

                Console.WriteLine("iteracja {0} wątek {1}", i, Thread.CurrentThread.ManagedThreadId);
            }

            s += Function(xk - dx / 2);
            integral = (dx / 6) * (Function(xp) + Function(xk) + 2 * integral + 4 * s);

            return integral;
        }

        public double RectangularIntegrationParallel(double xp, double xk, int n, int maxThreads)
        {
            double dx, integral = 0;
            dx = (xk - xp) / n;

            Parallel.For(1, n + 1, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, i =>
            {
                integral += dx * Function(xp + i * dx);
                Console.WriteLine("Parallel - iteracja {0} wątek {1}", i, Thread.CurrentThread.ManagedThreadId);
            });

            return integral;
        }

        public double TrapezoidalIntegrationParallel(double xp, double xk, int n, int maxThreads)
        {
            double dx, integral = 0;
            dx = (xk - xp) / n;

            Parallel.For(1, n + 1, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, i =>
            {
                integral += Function(xp + i * dx);
                Console.WriteLine("Parallel - iteracja {0} wątek {1}", i, Thread.CurrentThread.ManagedThreadId);
            });

            integral += (Function(xp) + Function(xk)) / 2;
            integral *= dx;

            return integral;
        }

        public double SimpsonsIntegrationParallel(double xp, double xk, int n, int maxThreads)
        {
            double dx, integral, s, x;
            integral = 0;
            s = 0;

            dx = (xk - xp) / n;

            Parallel.For(1, n + 1, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, i =>
            {
                x = xp + i * dx;
                s += Function(x - dx / 2);
                integral += Function(x);

                Console.WriteLine("Parallel - iteracja {0} wątek {1}", i, Thread.CurrentThread.ManagedThreadId);
            });

            s += Function(xk - dx / 2);
            integral = (dx / 6) * (Function(xp) + Function(xk) + 2 * integral + 4 * s);

            return integral;
        }
    }
}
