using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int size = 6; 
        int[] array = new int[size];
        Random rand = new Random();

        
        for (int i = 0; i < size; i++)
        {
            array[i] = rand.Next(1,100);        
        }

        Console.WriteLine("Згенерований масив:");
        Console.WriteLine(string.Join(", ", array));

        int numThreads = 4;
        int sum = ParallelSum(array, numThreads);

        Console.WriteLine($"Сума елементів масиву: {sum}");
    }

    static int ParallelSum(int[] array, int numThreads)
    {
        int length = array.Length;
        int chunkSize = length / numThreads;
        int sum = 0;
        object lockObj = new object();

        Parallel.For(0, numThreads, i =>
        {
            int start = i * chunkSize;
            int end = (i == numThreads - 1) ? length : start + chunkSize;
            int localSum = 0;

            for (int j = start; j < end; j++)
            {
                localSum += array[j];
            }

            lock (lockObj)
            {
                sum += localSum;
            }
        });

        return sum;
    }
}
