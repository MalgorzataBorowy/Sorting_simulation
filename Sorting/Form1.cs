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

        Semaphore semaphoreObject = new Semaphore(initialCount: 1, maximumCount: 1);

        public Form1()
        {

            InitializeComponent();

            Random rnd = new Random();
            for (int i=0; i<array.Length; i++)
            {
                series[i] = i.ToString();
                array[i] = rnd.Next(1, 100);
            }
            Visualize(array);
        }

        public void Visualize(int []arr)
        {
            chart.Series.Clear();
            for (int i = 0; i < array.Length; i++)
            {
                chartSeries = chart.Series.Add(series[i]);
                chartSeries.Points.Add(arr[i]);
                chart.Series[i].Color = Color.Blue;
            }
            chart.Update();
        }

        private void btnBubbleSort_Click(object sender, EventArgs e)
        {
            semaphoreObject.WaitOne();
            Sorting.BubbleSort(array, Visualize);
            semaphoreObject.Release();
        }

        private void btnQuickSort_Click(object sender, EventArgs e)
        {
            semaphoreObject.WaitOne();
            Sorting.QuickSort(array, 0, array.Length - 1, Visualize);
            semaphoreObject.Release();
        }

        private void btnBucketSort_Click(object sender, EventArgs e)
        {
            semaphoreObject.WaitOne();
            Sorting.BucketSort(ref array, Visualize);
            semaphoreObject.Release();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {        
            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(1, 100);
            }
            Visualize(array);
        }
    }
}
