using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    class Trainer
    {
        private string _type; // trainer type "SGD"
        private string _optimizer; // "none", "momentum"
        private double _rate; // learning rate 
        private Matrix _inputSet;
        private Matrix _targetSet;
        private int _trainingSetSize;
        public double error;
        private NeuralNetwork _lastUpdate = null;
        private double _momentumTerm;

        public Trainer(string type, string optimizer, double rate, params double[] list)
        {
            _inputSet = new Matrix();
            _targetSet = new Matrix();
            _type = type;
            _rate = rate;
            _optimizer = optimizer;
            _trainingSetSize = 0;
            if (list.Count() > 0)
            {
                _momentumTerm = list[0];
            }
        }

        public void AddData(Matrix input, Matrix target)
        {
            _inputSet.AddColumn(input[0]);
            _targetSet.AddColumn(target[0]);
            _trainingSetSize++;
        }

        public void Train(NeuralNetwork network)
        {
            switch (_optimizer) {
                case ("none"):
                    for (int i = 0; i < _trainingSetSize; i++) // for each training set
                    {
                        Matrix testInputs = new Matrix();
                        Matrix targetOutputs = new Matrix();
                        testInputs.AddColumn(_inputSet[i]);
                        targetOutputs.AddColumn(_targetSet[i]);

                        Matrix testOutputs = network.Predict(testInputs); // forward propagate inputs

                        network.Backpropagate(targetOutputs); // calculate derivatives using target outputs
                        for (int x = 1; x < network.Layers.Count; x++)
                        {
                            // weights = weights - learningrate * gradient direction
                            network.Layers[x].Weights = network.Layers[x].Weights + (-_rate * network.Layers[x].dWeights); 
                        }
                        error = 0.0;
                        for (int j = 0; j < _targetSet.Rows; j++)
                        {
                            error += Math.Pow(testOutputs[0][j] - _targetSet[i][j], 2);
                        }
                    }
                    break;
                case ("momentum"):
                    for (int i = 0; i < _trainingSetSize; i++) // for each training set
                    {
                        Matrix testInputs = new Matrix();
                        Matrix targetOutputs = new Matrix();
                        testInputs.AddColumn(_inputSet[i]);
                        targetOutputs.AddColumn(_targetSet[i]);

                        Matrix testOutputs = network.Predict(testInputs); // forward propagate inputs

                        network.Backpropagate(targetOutputs);

                        if (_lastUpdate == null) 
                        {
                            _lastUpdate = new NeuralNetwork(network);
                            foreach (Layer layer in _lastUpdate.Layers)
                            {
                                layer.dWeights *= 0;
                            }
                        }
                        NeuralNetwork _vector = new NeuralNetwork(network);
                        for (int x = 1; x < _vector.Layers.Count; x++)
                        {
                            // update vector is last update vector times momentum term plus gradient direction times learningrate
                            _vector.Layers[x].dWeights = (_momentumTerm *_lastUpdate.Layers[x].dWeights) + (_rate * network.Layers[x].dWeights); 
                        }
                        _lastUpdate = new NeuralNetwork(_vector); // save update vector
                        for (int x = 1; x < network.Layers.Count; x++)
                        {
                            // weights = weights - update vector
                            network.Layers[x].Weights = network.Layers[x].Weights + (-1 * _vector.Layers[x].dWeights); 
                        }
                        error = 0.0;
                        for (int j = 0; j < _targetSet.Rows; j++)
                        {
                            error += Math.Pow(testOutputs[0][j] - _targetSet[i][j], 2);
                        }
                    }
                    break;
            }
        }
    }
}
