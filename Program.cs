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
            Trainer ashketchum = new Trainer("SGD", 0.05); // create a new trainer using stochastic gradient descent with learning rate of 5%
            network.AddLayer("input", "leaky_relu", 2, 0); 
            network.AddLayer("hidden", "leaky_relu", 3, 2); 
            network.AddLayer("output", "sigmoid", 1, 3); // schqueeze the output (0, 1)

            // AND gate data
            ashketchum.AddData(new Matrix(new List<double>() { 0, 0 }), new Matrix(new List<double>() { 0 }));
            ashketchum.AddData(new Matrix(new List<double>() { 1, 0 }), new Matrix(new List<double>() { 0 }));
            ashketchum.AddData(new Matrix(new List<double>() { 0, 1 }), new Matrix(new List<double>() { 0 }));
            ashketchum.AddData(new Matrix(new List<double>() { 1, 1 }), new Matrix(new List<double>() { 1 }));


            do
            {
                ashketchum.Train(network);
                Console.WriteLine("error: " + ashketchum.error);
            } while (ashketchum.error > 0.005);

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
