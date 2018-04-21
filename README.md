# DejaVu
This project exists to document my creation some sort of neural network library from scratch. I am developing 
this in order to solidify my understanding of neural networks, not necessarily as a competitor to other existing 
machine learning libraries out there. Essentially, the features included in this library are the features I 
understand about neural networks. Also, this project is sort of representative of my own knowledge of 
programming as I proceed through schooling. This is also somewhat based off of my previous repo of a neural 
network based off of objects such as neurons and layers. However, this project focuses on rank-2 tensors or 
matrices. 

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
A class for holding layer values, weights, and biases and performing essential functions
#### Constructors
- Layer(string,string,int,int) (layer type, activation type, number of neurons, number of neurons of previous layer)
#### Properties
- Values (a matrix of layer neuron values)
- Weights (a matrix of layer weights)
- Biases (a matrix of layer biases)
#### Methods
- Activate(x) (activation function) (x can be a matrix or a double)
- d_Activate(x) (derivative of activation function) (x can be a matrix or a double)
- FeedLayer(Matrix) (forward propagate the layer based on previous layer values)
- Backprop(Layer, Layer) (backpropagate the layer based on next layer backpropagation and previous layer values)
- UpdateWeights(double) (updates the weights based on calculated derivatives from backpropagation and a learning rate)
- SetValues(Matrix) (explicitly sets values of layer)

### Matrix class
A class for holding matrices and performing matrix operations
#### Constructors
- Matrix() (default constructor)
- Matrix(Matrix) (copy constructor)
- Matrix(List\<double\>) (vector constructor)
#### Properties
- this[index] (accesses each column)
- Rows (returns # of rows)
- Columns (return # of columns)
#### Methods
- Matrix addition
- Matrix multiplication
- Scalar multiplication
- AddColumn(List) (adds a column to the matrix)

## To-do
- Allow different network structures, such as the different kinds of recurrent networks
- more activation functions?
- maybe create this in C++ and other languages too, just to practice my hand and allow me to use it in various 
other projects if ever
- git gud 
