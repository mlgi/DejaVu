using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork network = new NeuralNetwork();
            network.AddLayer("input", "leaky_relu", 2, 0);
            network.AddLayer("hidden", "leaky_relu", 3, 2);
            network.AddLayer("output", "sigmoid", 1, 3);

            while (true)
            {
                Matrix test_inputs = new Matrix();
                List<double> newColumn = new List<double>();
                for (int i = 0; i < network.InputSize; i++)
                {
                    Console.Write("input" + i + ": ");
                    double one = Convert.ToDouble(Console.ReadLine());
                    newColumn.Add(one);
                }
                test_inputs.AddColumn(newColumn);
                Console.WriteLine(network.Predict(test_inputs));
            }
        }
    }
}
