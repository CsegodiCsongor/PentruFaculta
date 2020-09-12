import numpy as np


class Layer:
    def __init__(self, neuron_count):
        self.b = np.random.rand(1, 1)
        self.a = np.empty((1, neuron_count))
        self.z = np.empty((1, neuron_count))
        self.w = np.empty((1, 1))
        self. neuron_count = neuron_count


class NeuralNetwork:
    def __init__(self, layers_structure):
        self.layers = list()

        for neuron_count in layers_structure:
            self.layers.append(Layer(neuron_count))
        for i in range(len(self.layers) - 1):
            self.layers[i].w = (np.random.rand(layers_structure[i], layers_structure[i + 1]) - 0.5) * 100
        for i in range(len(self.layers)):
            self.layers[i].b = (np.random.rand(1, layers_structure[i]) - 0.5) * 100

    def propagate_forward(self):
        for i in range(1, len(self.layers)):
            self.layers[i].z = self.layers[i-1].a.dot(self.layers[i-1].w)
            self.layers[i].z += self.layers[i].b
            self.activate_layer(i)

    def activate_layer(self, layer_number):
        for i in range(self.layers[layer_number].neuron_count):
            if self.layers[layer_number].z[0, i] < 0:
                self.layers[layer_number].a[0, i] = 0
            else:
                self.layers[layer_number].a[0, i] = 1

    def make_decision(self, a):
        self.layers[0].a = np.array(a)
        self.propagate_forward()
        return self.layers[-1].a


