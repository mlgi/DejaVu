using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    class Trainer
    {
        private int _batchSize; // 1 = stochastic
        private string _optimizer; // "none", "momentum"
        private Matrix _inputSet;
        private Matrix _targetSet;
        private int _trainingSetSize;
        public double error;
        private double[] parameters; 
        

        // constructors
        public Trainer(int batchSize, string optimizer, params double[] list) // constructor
        {
            _inputSet = new Matrix();
            _targetSet = new Matrix();
            _batchSize = batchSize;
            _optimizer = optimizer;
            _trainingSetSize = 0;
            parameters = list;
        }
        // methods
        private double Batchpropagate(int start, int batchSize, NeuralNetwork network) // find the avg dweights for a given number of training data inputs, returns avg error
        {
            Matrix testInputs = new Matrix(_inputSet[start]);
            Matrix targetOutputs = new Matrix(_targetSet[start]);
            Matrix testOutputs = network.Predict(testInputs);
            network.Backpropagate(targetOutputs);
            NeuralNetwork deltaSum = new NeuralNetwork(network); // backpropagate the first training input and store it in a separate network deltaSum

            double totalError = 0.0;
            for (int j = 0; j < _targetSet.Rows; j++)
            {
                totalError += Math.Pow(testOutputs[0][j] - _targetSet[start][j], 2); // calculate error
            }

            for (int i = start + 1; i < start + batchSize; i++) // add dWeights of each training data into deltaSum
            {
                if (i >= _trainingSetSize) break; // break loop if batch goes past training set size
                testInputs = new Matrix(_inputSet[i]);
                targetOutputs = new Matrix(_targetSet[i]);
                testOutputs = network.Predict(testInputs);
                network.Backpropagate(targetOutputs);
                for (int x = 1; x < network.Layers.Count; x++) // do this for each layer
                {
                    deltaSum.Layers[x].dWeights += network.Layers[x].dWeights;
                }

                for (int j = 0; j < _targetSet.Rows; j++)
                {
                    totalError += Math.Pow(testOutputs[0][j] - _targetSet[i][j], 2); // calculate error
                }
            }
            for (int x = 1; x < network.Layers.Count; x++)  // find the avg
            {
                deltaSum.Layers[x].dWeights *= (1.0 / batchSize);
                network.Layers[x].dWeights = deltaSum.Layers[x].dWeights;
            }
            deltaSum = null;

            return totalError / batchSize;
        }
        public void AddData(Matrix input, Matrix target) // add training data
        {
            _inputSet.AddColumn(input[0]);
            _targetSet.AddColumn(target[0]);
            _trainingSetSize++;
        }
        public void Train(NeuralNetwork network) // trains the network throughout entire training data once
        {
            double _rate = parameters[0];
            switch (_optimizer) {
                case ("none"):
                    for (int i = 0; i < _trainingSetSize; i+=_batchSize) // for every batch of size batchSize in training data 
                    {
                        error = Batchpropagate(i, _batchSize, network); // calculate derivatives and error for this batch
                        for (int x = 1; x < network.Layers.Count; x++)
                        {
                            // weights = weights - learningrate * gradient direction
                            network.Layers[x].Weights = network.Layers[x].Weights + (-_rate * network.Layers[x].dWeights);
                        }
                    }
                    break;
                case ("momentum"):
                    double _momentumTerm = parameters[1];
                    NeuralNetwork _lastUpdate = null;
                    for (int i = 0; i < _trainingSetSize; i+=_batchSize) // for every batch of size batchSize in training data 
                    {
                        error = Batchpropagate(i, _batchSize, network); // calculate derivatives and error for this batch

                        if (_lastUpdate == null) 
                        {
                            _lastUpdate = new NeuralNetwork(network);
                            foreach (Layer layer in _lastUpdate.Layers)
                            {
                                layer.dWeights *= 0; // assume dweights at first iteration is 0
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
                    }
                    break;
            }
        }
    }
}
