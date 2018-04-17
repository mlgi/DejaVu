using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    [System.Serializable]
    class Layer
    {
        private Matrix _values; // matrix that holds the layer's values
        private Matrix _weights; // matrix that holds the layer's weights for the previous layer
        private Matrix _biases; // matrix that holds the layer's biases
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
        public Matrix Weights // returns a copy of _weight matrix
        {
            get
            {
                if (_type != "input")
                    return new Matrix(_weights);
                else
                    return null;
            }
            set
            {
                _weights = value;
            }
        }
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
                for (int j = 0; j < prevLayerNeuronCount; j++)
                {
                    List<double> newColumn = new List<double>();
                    for (int i = 0; i < neuronCount; i++)
                    {
                        newColumn.Add(NextNormal(0, 1));
                    }

                    weightMatrix.AddColumn(newColumn);
                    
                }
                _weights = weightMatrix;
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
        public void FeedLayer(Matrix previousLayer) // generates this layer's values based off previous layer's values and this layer's weights+biases
        {
            _values = (_weights * previousLayer) + _biases;
            _values = Activate(_values);
        }
        public void SetValues(Matrix inputValues)
        {
            _values = inputValues;
        } // sets neuron values
        ~Layer() // destroi boii
        {
            _values = null;
            _weights = null;
        }
    }
}
