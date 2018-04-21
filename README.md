# DejaVu
This project exists to document my creation some sort of neural network library from scratch. I am developing 
this in order to solidify my understanding of neural networks, not necessarily as a competitor to other existing 
machine learning libraries out there. Essentially, the features included in this library are the features I 
understand about neural networks. Also, this project is sort of representative of my own knowledge of 
programming as I proceed through schooling. This is also somewhat based off of my previous repo of a neural 
network based off of objects such as neurons and layers. However, this project focuses on rank-2 tensors or 
matrices. 

## Program
The example program attempts to train a neural network with 2 input neurons, 3 hidden neurons, and 1 output neuron. It uses the dataset of an AND gate to do so. 

## Structure
### NeuralNetwork class
This class holds the multitude of layers that are added to a network. This allows simple forward-propagation of 
the network through the layers. It exists simply to organize the layers created and their functions. In any context, backpropagation/forward propagation, next layers refer to layers towards output, previous layers refer to layers towards input.
#### Constructors
- NeuralNetwork()
#### Properties
- InputSize (size of input layer)
#### Methods
- Predict(Matrix) (given inputs, network will feedforward an output)
- Backpropagate(Matrix) (given target values, network will backpropagate)
- UpdateWeights(double) (updates the weights based on calculated derivatives from backpropagation and a learning rate)
- AddLayer(string,string,int,int) (layer type, activation type, number of neurons, number of neurons of previous layer)

### Layer class
This class holds layer values, weights, and biases and performing essential functions. Uses matrices to acheive these functions.

### Matrix class
This class holds matrices and performing matrix operations

### Trainer class
This class manages training for neural networks. Currently only supports stochasitic gradient descent. 

## To-do
- Allow different network structures, such as the different kinds of recurrent networks
- Implement other training styles?
- more activation functions?
- maybe create this in C++ and other languages too, just to practice my hand and allow me to use it in various 
other projects if ever
- git gud 
