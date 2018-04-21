# DejaVu
This project exists to document my creation some sort of neural network library from scratch. I am developing 
this in order to solidify my understanding of neural networks, not necessarily as a competitor to other existing 
machine learning libraries out there. Essentially, the features included in this library are the features I 
understand the most about neural networks. Also, this project is sort of representative of my own knowledge of 
programming as I proceed through schooling. This is also somewhat based off of my previous repo of a neural 
network based off of objects such as neurons and layers. However, this project focuses on rank-2 tensors or 
matrices. 

## Structure
### Matrix class
This class exists to be able to create a matrix rather than just a multidimensional array or list with varying 
dimensions. Also, this allows me to wrap the functions as operators so with Matrix A and B, I can find the 
resulting matrix from the multiplication of A and B in (A * B). Again, I know there are Math libraries out there 
with Matrices included and all their operations, but as I have not taken Linear Algebra, this helps me evaluate 
my understanding of Matrices. At the very least, the knowledge I need to use them in neural networks.

### Layer class
This class exists to hold the value matrix of the Layer as well as its weights to be used with the previous 
layer's values. It also allows me to categorize a layer as "input", "output", and "hidden". Also, each layer can 
have a different activation function, allowing me, for example, to use ReLU for all layers except the output 
layer, in which I'd use another one, like a sigmoid activation function.

### NeuralNetwork class
This class holds the multitude of layers that are added to a network. This allows simple forward-propagation of 
the network through the layers. It exists simply to organize the layers created.

## To-do
- Create a trainer class that uses various supervised training models, like SGD or maybe some sort of genetic 
algorithm
- Understand backpropagation enough to code it
- Allow different network structures, such as the different kinds of recurrent networks
- more activation functions?
- maybe create this in C++ and other languages too, just to practice my hand and allow me to use it in various 
other projects if ever
- git gud 
