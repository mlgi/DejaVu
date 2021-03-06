﻿using System;
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
        public List<Layer> Layers {
            get
            {
                return _layers;
            }
            set
            {
                throw new Exception("cannot directly change layers");
            }
        }
        // constructors
        public NeuralNetwork() 
        {
            _layers = new List<Layer>();
            _layerDepth = 0;
        }
        public NeuralNetwork(NeuralNetwork other) // copy constructor
        {
            _layerDepth = other._layerDepth;
            _inputSize = other._inputSize;
            _layers = new List<Layer>();
            foreach (Layer layer in other._layers)
            {
                _layers.Add(new Layer(layer));
            }
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
                //Console.WriteLine("Evaluating layer " + i + " with " + currentValues);
                _layers[i].FeedLayer(currentValues);
                currentValues = new Matrix(_layers[i].Values);
            }
            return _layers[_layerDepth - 1].Values;
        }
        public void Backpropagate(Matrix targets)
        {
            Layer targetLayer = new Layer("target", "lmao", _layers[_layerDepth - 1].Size, _layers[_layerDepth - 1].Size);
            targetLayer.SetValues(targets);

            for (int x = _layerDepth - 1; x >= 1; x--) // backprop each layer starting from the output layer
            {
                if (x == _layerDepth - 1)
                {
                    
                    _layers[x].Backprop(_layers[x - 1], targetLayer);
                } 
                else
                {
                    _layers[x].Backprop(_layers[x - 1], _layers[x + 1]);
                }
            }
        } // backpropagate using target values
        /*public void UpdateWeights(NeuralNetwork parameters)
        {
            for (int i = 1; i < _layerDepth; i++)
            {
                _layers[i].UpdateWeights(parameters._layers[i]);
            }
        }*/
    }
}
