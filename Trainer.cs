using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    class Trainer
    {
        private string _type; // trainer type "SGD" (will add more)
        private double _rate; // learning rate (or mutation rate in genetic algo)
        private Matrix _inputSet;
        private Matrix _targetSet;
        private int _trainingSetSize;
        public double error;

        public Trainer(string type, double rate)
        {
            _inputSet = new Matrix();
            _targetSet = new Matrix();
            _type = type;
            _rate = rate;
            _trainingSetSize = 0;
        }

        public void AddData(Matrix input, Matrix target)
        {
            _inputSet.AddColumn(input[0]);
            _targetSet.AddColumn(target[0]);
            _trainingSetSize++;
        }

        public void Train(NeuralNetwork network)
        {
            for (int i = 0; i < _trainingSetSize; i++)
            {
                Matrix testInputs = new Matrix();
                Matrix targetOutputs = new Matrix();
                testInputs.AddColumn(_inputSet[i]);
                targetOutputs.AddColumn(_targetSet[i]);

                Matrix testOutputs = network.Predict(testInputs);
                
                network.Backpropagate(targetOutputs);
                network.UpdateWeights(_rate);
                error = 0.0;
                for (int j = 0; j < _targetSet.Rows; j++)
                {
                    error += Math.Pow(testOutputs[0][j] - _targetSet[i][j], 2);
                }
            }
        }
    }
}
