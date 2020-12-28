using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public static class Sorting
    {
        public static void QuickSort(int[] tab, int left, int right, Action<int[]> Visualize)
        {
            int i = left;
            int j = right;
            int x = tab[(left + right) >> 1];
            do
            {
                while (tab[i] < x) i++;
                while (tab[j] > x) j--;
                if (i <= j)
                {
                    int temp = tab[i];
                    tab[i] = tab[j];
                    tab[j] = temp;
                    i++;
                    j--;
                }
                Visualize(tab);

            } while (i <= j);
            if (left < j) QuickSort(tab, left, j, Visualize);
            if (right > i) QuickSort(tab, i, right, Visualize);
        }

        public static void BubbleSort(int[] ar, Action<int[]> Visualize)
        {
            for (int pass = 1; pass < ar.Length; pass++)
            {
                for (int i = 0; i < ar.Length - 1; i++)
                {
                    if (ar[i] > ar[i + 1])
                    {
                        int hold = ar[i];
                        ar[i] = ar[i + 1];
                        ar[i + 1] = hold;
                    }
                    Visualize(ar);
                }
            }
        }

        public static void BucketSort(ref int[] x, Action<int[]> Visualize)
        {
            List<int> result = new List<int>();
            List<int> vis = new List<int>();
            vis.AddRange(x);
            int numOfBuckets = 10;

            List<int>[] buckets = new List<int>[numOfBuckets];

            for (int i = 0; i < numOfBuckets; i++)
                buckets[i] = new List<int>();

            for (int i = 0; i < x.Length; i++)
            {
                int buckitChoice = (x[i] / numOfBuckets);
                buckets[buckitChoice].Add(x[i]);
            }

            for (int i = 0; i < numOfBuckets; i++)
            {
                int[] temp = BubbleSortList(buckets[i]);
                result.AddRange(temp);

                vis.RemoveRange(i * (x.Length/numOfBuckets), temp.Length);
                vis.InsertRange(i * (x.Length/numOfBuckets), temp);

                Visualize(vis.ToArray());
            }
            x = result.ToArray();
            Visualize(x);
        }

        public static int[] BubbleSortList(List<int> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    if (input[i] < input[j])
                    {
                        int temp = input[i];
                        input[i] = input[j];
                        input[j] = temp;
                    }
                }
            }
            return input.ToArray();
        }

        public static void InsertSort(int[] a, int length, Action<int[]> Visualize)
        {
            int i, j, value;

            for (i = 1; i < length; ++i)
            {
                value = a[i];
                for (j = i - 1; j >= 0 && a[j] > value; --j)
                {
                    a[j + 1] = a[j];
                }
                a[j + 1] = value;
                Visualize(a);
            }
        }

        public static void SelectionSort(int[] t, int n, Action<int[]> Visualize)
        {
            int i, j, k;
            for (i = 0; i < n; i++)
            {
                k = i;
                for (j = i + 1; j < n; j++) if (t[j] < t[k]) k = j;

                int temp = t[k];
                t[k] = t[i];
                t[i] = temp;

                Visualize(t);
            }
        }
    }
}
