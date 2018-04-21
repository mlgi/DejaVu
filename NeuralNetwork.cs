using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    [System.Serializable]
    class NeuralNetwork
    {
        private List<Layer> _layers;
        private int _layerDepth;
        private int _inputSize;

        // properties
        public int InputSize // return size of inputlayer
        {
            get
            {
                return (int)_inputSize;
            }
            set
            {
                throw new Exception("Cannot change network size");
            }
        }
        // constructors
        public NeuralNetwork() 
        {
            _layers = new List<Layer>();
            _layerDepth = 0;
        }
        // methods
        public void AddLayer(string type, string activation, int neuronCount, int previousLayerNeuronCount)
        {
            Layer newLayer = new Layer(type, activation, neuronCount, previousLayerNeuronCount);
            _layers.Add(newLayer);
            _layerDepth++;
            if (type == "input")
            {
                _inputSize = neuronCount;
            }
        } // adds a layer to the network
        public Matrix Predict(Matrix inputs) // pass inputs through layers
        {
            _layers[0].SetValues(inputs);
            Matrix currentValues = inputs;
            for (int i = 1; i < _layerDepth; i++)
            {
                Console.WriteLine("Evaluating layer " + i + " with " + currentValues);
                _layers[i].FeedLayer(currentValues);
                currentValues = new Matrix(_layers[i].Values);
            }
            return _layers[_layerDepth - 1].Values;
        }
    }
}
