using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace Sorting
{
    public partial class Form1 : Form
    {
        static int elements = 50;
        int[] array = new int[elements];
        string[] series = new String[elements];
        Series chartSeries;

        public Form1()
        {

            InitializeComponent();

            Random rnd = new Random();
            for (int i=0; i<array.Length; i++)
            {
                series[i] = i.ToString();
                array[i] = rnd.Next(1, 100);
            }
            Visualize();
        }

        public void Quick()
        {
            QuickSort(array, 0, array.Length - 1);
        }

        public void Bubble()
        {
            BubbleSort(array);
        }

        public void Bucket()
        {
            BucketSort(ref array);
        }

        private void QuickSort(int[] tab, int left, int right)
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
                if (chart.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { Visualize(); });
                }
            } while (i <= j);
            if (left < j) QuickSort(tab, left, j);
            if (right > i) QuickSort(tab, i, right);
        }

        public void BubbleSort(int[] ar)
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
                    if (chart.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate { Visualize(); });
                    }
                }
            }
        }

        public void BucketSort(ref int[] x)
        {
            List<int> result = new List<int>();
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
            }
            x = result.ToArray();
        }

        public int[] BubbleSortList(List<int> input)
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

        public void Visualize()
        {
            chart.Series.Clear();
            for (int i = 0; i < array.Length; i++)
            {
                chartSeries = chart.Series.Add(series[i]);
                chartSeries.Points.Add(array[i]);
                chart.Series[i].Color = Color.Blue;
            }
        }

        private void btnBubbleSort_Click(object sender, EventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(this.Bubble));
            myThread.IsBackground = true;
            myThread.Start();
            Visualize();
        }

        private void btnQuickSort_Click(object sender, EventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(this.Quick));
            myThread.IsBackground = true;
            myThread.Start();
            Visualize();
        }

        private void btnBucketSort_Click(object sender, EventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(this.Bucket));
            myThread.IsBackground = true;
            myThread.Start();
            Visualize();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {        
            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(1, 100);
            }
            Visualize();
        }
    }
}
