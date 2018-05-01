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
#### Methods
##### Predict(Matrix)
Given a matrix of inputs, this method feedforward the inputs through the layers and return a matrix of outputs
##### Backpropagate(Matrix) 
Given a matrix of expected outputs, this method will backpropagate through the layers and calculate the derivatives of the error function with respect to each weight. It will be stored in a matrix (dWeights) under each layer.
##### AddLayer(string,string,int,int) 
Given the layer type (input, hidden, output), this method will add a layer to the network, initializing a weight matrix dependent on the size of this new layer and the layer before it.

### Trainer class
This class manages training for neural networks. 
#### Constructor Trainer(int batchSize, string optimizer, params double[] list)
- batchSize is the size of each batch to train
- optimizer is the type of optimizer to use
  - "none" uses pure gradient descent with no optimization
  - "momentum" uses momentum
- list takes in a list of parameters dependent on the optimizer
  - "none"
    - learningRate
  - "momentum"
    - learningRate
    - momentumTerm

## Last updates
### mini-batch / batch gradient descent support
Using SGD or mini-batch/batch is simple. For SGD, use a batch size of 1. For BGD, use the size of the training set.

## To-do
- Allow different network structures, such as the different kinds of recurrent networks
- more activation functions? (softplus?)
- maybe create this in C++ and other languages too, just to practice my hand and allow me to use it in various 
other projects if ever
- create this in python and try openAI's contest
- git gud 
- dropout(not out of college, see source #16)


## Working on
- more optimizers
- a test program using MNIST handwritten numbers
- ability to read large datasets from file instead of having to import the entire dataset into memory
- understanding dropout

## Resources
1. [An overview of gradient descent optimization algorithms](http://ruder.io/optimizing-gradient-descent/) by Sebastian Ruder. 
   - The source I mainly use when implementing optimization algorithms.
2. [Backpropagation on Wikipedia](https://en.wikipedia.org/wiki/Backpropagation). 
   - The mathematical explanations help when trying to get a generalization of the entire process. It's hard to read unless you've grasped the basic idea, but still understandable.
3. [Tensor on Wikipedia](https://en.wikipedia.org/wiki/Tensor). 
4. [Matrix on Wikipedia](https://en.wikipedia.org/wiki/Matrix_(mathematics)). 
   - Good read when I was implementing the Matrix class and multiplication.
5. [Deep Learning](http://www.deeplearningbook.org/) by Ian Goodfellow, Yoshua Bengio, and Aaron Courville. 
   - A good source overall for deep learning, one I have not finished yet.
6. [A Step by Step Backpropagation Example](https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/) by Matt Mazur. 
   - A good source to visualize actual backpropagation. This helps actually see what is going on, but assumes you're only using sigmoid for an activation function.
7. [How the backpropagation algorithm works](http://neuralnetworksanddeeplearning.com/chap2.html) by Michael Nielsen
8. [Back-propagation is very simple. Who made it complicated?](https://medium.com/@14prakash/back-propagation-is-very-simple-who-made-it-complicated-97b794c97e5c) by Prakash Jay
9. [The Backpropagation Algorithm](https://page.mi.fu-berlin.de/rojas/neural/chapter/K7.pdf) by Raul Rojas.
   - The entire book can be found [here](https://page.mi.fu-berlin.de/rojas/neural/neuron.pdf). I've mainly only read the the first half of chapter 7. I think this is the one I've referred to the most when I was having trouble understanding backpropagation. This also assumes you're using sigmoid, but explains how to use others. 
10. [Backpropagation](http://www.cs.cornell.edu/courses/cs5740/2016sp/resources/backprop.pdf) by J.G. Makin
11. [Convolutional Neural Networks](http://cs231n.github.io/optimization-2/).
    - Course material for the [CS231n](http://vision.stanford.edu/teaching/cs231n/) class.
12. [Tensor Calculus 0: Introduction](https://www.youtube.com/watch?v=kGXr1SF3WmA) 
    - a video series by [eigenchris](https://www.youtube.com/user/eigenchris).
13. [But what *is* a Neural Network? | Chapter 1, deep learning](https://www.youtube.com/watch?v=aircAruvnKk)
    - a video series by [3Blue1Brown](https://www.youtube.com/channel/UCYO_jab_esuFRV4b17AJtAw). I'm particularly a big fan of his videos and this series is no exception.
14. [How Deep Neural Networks Work](https://www.youtube.com/watch?v=ILsA4nyG7I0) 
    - a video by [Brandon Rohrer](https://www.youtube.com/channel/UCsBKTrp45lTfHa_p49I2AEQ).
15. [Beginner Intro to Neural Networks](https://www.youtube.com/watch?v=ZzWaow1Rvho). 
    - The video that started it all. By watching this I was amazed by how simple neural networks really are(kinda). At the very least, it seemed doable so I decided to try it. Here I am today.
16. [Dropout: A simple way to prevent neural networks from overfitting](http://jmlr.org/papers/volume15/srivastava14a.old/srivastava14a.pdf) by Nitish Srivastava, Geoffrey Hinton, Alex Krizhevsky, Ilya Sutskever and Ruslan Salakhutdinov.
17. [A Theoretical Framework for Back-Propagation](http://yann.lecun.com/exdb/publis/pdf/lecun-88.pdf) by Yann LeCunn 
    - How could I not include Yann LeCun
