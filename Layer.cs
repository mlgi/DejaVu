﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    [System.Serializable]
    class Layer
    {
        private Matrix _values = new Matrix(); // matrix that holds the layer's values
        private Matrix _dValues = new Matrix(); // matrix that holds the layer's values inputted through the derivative of the activation function
        public Matrix Weights = new Matrix(); // matrix that holds the layer's weights for the previous layer
        public Matrix dWeights = new Matrix(); // matrix that holds the partial derivatives of the error with regards to each weight
        private Matrix _biases = new Matrix(); // matrix that holds the layer's biases
        private Matrix _dBiases = new Matrix(); // matrix that holds the partial derivatives of the error with regards to each bias
        private Matrix _bpError = new Matrix(); // matrix that holds the backpropagated error for each neuron
        private int _size; // number of neurons in the layer
        private string _type; // "input", "hidden", "output"
        private string _activation; // "leaky_relu", "tanh", "sigmoid", "relu", relu
        Random rand = new Random(Guid.NewGuid().GetHashCode()); // random number generator

        // properties
        public int Size // returns layer size
        {
            get {
                return _size;
            }
            set
            {
                throw new Exception("Cannot change layer size");
            }
        }
        public Matrix Values // returns a copied matrix of layer values
        {
            get
            {
                return new Matrix(_values);
            }
        }
        /*public Matrix Weights // returns a copy of _weight matrix
        {
            get
            {
                if (_type != "input")
                    return new Matrix(Weights);
                else
                    return null;
            }
            set
            {
                Weights = value;
            }
        }*/
        public Matrix Biases // returns a copy of_biases matrix
        {
            get
            {
                return new Matrix(_biases);
            }
            set
            {
                _biases = value;
            }
        }
        // constructors
        public Layer(string type, string activation, int neuronCount, int prevLayerNeuronCount) // constructor with type, activation, and prevLayerCount
        {
            _type = type;
            _activation = activation;
            _size = neuronCount;
            if (type != "input") // input layers dont have a previousLayer
            {
                // initialize random weights
                Matrix weightMatrix = new Matrix(); // matrix size should be neuronCount by prevLayerNeuronCount
                for (int i = 0; i < prevLayerNeuronCount; i++)
                {
                    List<double> newColumn = new List<double>();
                    for (int j = 0; j < neuronCount; j++)
                    {
                        newColumn.Add(NextNormal(0, 1));
                    }

                    weightMatrix.AddColumn(newColumn);
                    
                }
                Weights = weightMatrix;
                // initialize random biases
                Matrix biasMatrix = new Matrix();
                List<double> newBiasColumn = new List<double>();
                for (int i = 0; i < _size; i++)
                {
                    newBiasColumn.Add(NextNormal(0, 1));
                }
                biasMatrix.AddColumn(newBiasColumn);
                _biases = biasMatrix;
            }
            rand = null;
        }
        public Layer(Layer other) // copy constructor
        {
            _values = new Matrix(other._values);
            _dValues = new Matrix(other._dValues);
            Weights = new Matrix(other.Weights);
            dWeights = new Matrix(other.dWeights);
            _biases = new Matrix(other._biases);
            _dBiases = new Matrix(other._dBiases);
            _bpError = new Matrix(other._bpError);
            _size = other._size;
            _type = other._type;
            _activation = other._activation;
        }
        // methods
        public double NextNormal(double mean, double stdDev) // next gaussian random number with mean and standard deviation
        { 
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = mean + stdDev * randStdNormal;
            return randNormal;
        }
        public double Activate(double x) // activation function
        {
            switch(_activation)
            {
                case "leaky_relu":
                    if (x > 0)
                        return x;
                    else
                        return 0.01 * x;
                case "tanh":
                    return Math.Tanh(x);
                case "sigmoid":
                    return 1.0 / (1 + Math.Exp(-x));
                default:
                case "relu":
                    return (x > 0) ? x : 0;
            }
        }
        public double d_Activate(double x) // derivative of the activation functions
        {
            switch (_activation)
            {
                case "leaky_relu":
                    if (x > 0)
                        return 1;
                    else
                        return 0.01;
                case "tanh":
                    return 1 / Math.Pow(Math.Cosh(x), 2);
                case "sigmoid":
                    return (1.0 / (1 + Math.Exp(-x))) * (1.0 - (1.0 / (1 + Math.Exp(-x))));
                default:
                case "relu":
                    return (x > 0) ? 1 : 0;
            }
        }
        public Matrix Activate(Matrix X) // activation function applied to a matrix
        {
            Matrix newMatrix = new Matrix();
            for (int i = 0; i < X.Columns; i++)
            {
                List<double> newColumn = new List<double>();
                for (int j = 0; j < X.Rows; j++)
                {
                    newColumn.Add(Activate(X[i][j]));
                }
                newMatrix.AddColumn(newColumn);
            }
            return newMatrix;
        }
        public Matrix d_Activate(Matrix X) // derivative of the activation function applied to a matrix
        {
            Matrix newMatrix = new Matrix();
            for (int i = 0; i < X.Columns; i++)
            {
                List<double> newColumn = new List<double>();
                for (int j = 0; j < X.Rows; j++)
                {
                    newColumn.Add(d_Activate(X[i][j]));
                }
                newMatrix.AddColumn(newColumn);
            }
            return newMatrix;
        }
        public void FeedLayer(Matrix previousLayer) // generates this layer's values based off previous layer's values and this layer's weights and biases
        {
            _dValues = (Weights * previousLayer) + _biases;
            _values = (Weights * previousLayer) + _biases;
            _dValues = d_Activate(_dValues);
            _values = Activate(_values);
        }
        public void Backprop(Layer previousLayer, Layer nextLayer) // backpropagates previous layer's weights and calculates dWeights (next is towards output, previous is towards input)
        {
            if (_type == "output") // backpropagates with regards to error function
            {
                
                dWeights = new Matrix();
                _bpError = new Matrix();
                for (int i = 0; i < Weights.Columns; i++) // for each neuron i in the preceding layer
                {
                    List<double> newColumn = new List<double>();
                    List<double> newBPErrorColumn = new List<double>();
                    for (int j = 0; j < _size; j++) // for each neuron j in this layer
                    {
                        double dError_dOj = 2 * (_values[0][j] - nextLayer.Values[0][j]); // d of the error function with regards to the output value
                        double dOj_dNetj = _dValues[0][j]; // d of the output value with regards to the total sum
                        double dNetj_dwij = previousLayer.Values[0][i]; // d of the total sum with regards to weight ij

                        double Oalphaj = dError_dOj * dOj_dNetj; // store in _bpError for further backpropagation

                        double dError_dwij = Oalphaj * dNetj_dwij; // chain rule: d of the error function with regards to weight ij

                        newBPErrorColumn.Add(Oalphaj);
                        newColumn.Add(dError_dwij);
                    }
                    _bpError.AddColumn(newBPErrorColumn);
                    dWeights.AddColumn(newColumn);
                }
            }
            else if (_type != "input" && _type != "output") // input layer doesnt have weights
            {
                dWeights = new Matrix();
                _bpError = new Matrix();
                for (int i = 0; i < Weights.Columns; i++) // for each neuron i in the preceding layer
                {
                    List<double> newColumn = new List<double>();
                    List<double> newBPErrorColumn = new List<double>();
                    for (int j = 0; j < Weights.Rows; j++) // for each neuron j in this layer
                    {
                        double Sum_xwjq_Xalphaq = 0.0;
                        for (int q = 0; q < nextLayer.Size; q++) // for each neuron q in the next layer x
                        {
                            Sum_xwjq_Xalphaq += nextLayer.Weights[j][q] * nextLayer._bpError[0][q]; // weight of layer x from this layer neuron j to layer x neuron q times bpError of neuron q
                        }

                        double Halphaj = _dValues[0][j] * Sum_xwjq_Xalphaq;
                        double dError_dwij = Halphaj * previousLayer.Values[0][i]; // derivative of the error function with respect to weight ij

                        newBPErrorColumn.Add(Halphaj);
                        newColumn.Add(dError_dwij);
                    }
                    _bpError.AddColumn(newBPErrorColumn);
                    dWeights.AddColumn(newColumn);
                }
            }
        }
        /*public void UpdateWeights(Layer parameters) // update weights using weights of another layer
        {
            Weights = parameters.Weights;
        }*/
        public void SetValues(Matrix inputValues)
        {
            _values = inputValues;
        } // sets neuron values
        ~Layer() // destroi boii
        {
            _values = null;
            Weights = null;
        }
    }
}
