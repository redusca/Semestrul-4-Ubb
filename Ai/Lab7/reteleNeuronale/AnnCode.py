import numpy as np
from typing import List, Union, Literal, Optional, Tuple, Callable
import warnings

warnings.filterwarnings("ignore", category=RuntimeWarning)


def compute_loss(y_true, y_pred):
    return np.mean(np.square(y_true - y_pred))


class AnnCode:
    hidden_layers: list[int]
    activation: Literal['relu', 'tanh'] = 'relu',
    max_iter: int
    solver: Literal['sgd', 'adam'] = 'adam',
    random_state: int
    verbose: int
    learning_rate: Union[Literal['constant', 'invscaling'], float] = 'constant',
    learning_rate_init: float

    def __init__(
            self,
            hidden_layers,
            activation='relu',
            max_iter: int = 200,
            solver='sgd',
            random_state=None,
            verbose=0,
            learning_rate='constant',
            learning_rate_init=0.001
    ):
        """
        :param hidden_layers: numarul de neuroni de pe fiecare layer
        :param activation: functia de activare pentru neuroni
        :param max_iter: numarul de iteratii ale retelei
        :param solver: algoritmul de optimizare pentru actualizarea ponderilor
        :param random_state: for random generation
        :param verbose: din cat in cat sa se afiseze loss-ul in iteratii
        :param learning_rate:  tipul de invatare , constant sau se schimba learning rate-ul
        :param learning_rate_init: valoarea cu care invata reteau neuronala
        """
        self.hidden_layers = hidden_layers
        self.activation = activation
        self.max_iter = max_iter
        self.solver = solver
        self.random_state = random_state
        self.verbose = verbose
        self.learning_rate = learning_rate
        self.learning_rate_init = learning_rate_init

        if random_state is not None:
            np.random.seed(random_state)

        if self.solver != 'sgd' and self.solver != 'adam':
            raise ValueError(f"Unsupported solver : {self.solver}")

        self.weights = []
        self.biases = []
        self.n_layers = len(hidden_layers) + 2  # input + hidden + output
        self.loss_history = []

        self.activation_func = self._get_activation_function()
        self.activation_derivative = self._get_activation_derivative()

    def _get_activation_function(self):
        """
        Functia de activare
        """
        if self.activation == 'relu':
            return lambda x: np.maximum(0, x)
        elif self.activation == 'tanh':
            return lambda x: np.tanh(x)
        else:
            raise ValueError(f"Unsupported activation function: {self.activation}")

    def _get_activation_derivative(self):
        """
        Derivata functie de activare
        """
        if self.activation == 'relu':
            return lambda x: np.where(x > 0, 1, 0)
        elif self.activation == 'tanh':
            # f'(x) = 1 - f(x)^2
            return lambda x: 1 - np.square(self.activation_func(x))
        else:
            raise ValueError(f"Unsupported activation function: {self.activation}")

    def _initialize_parameters(self, nr_features, nr_outputs):
        """
        Initializare parametrilor pentru reteaua neuronala
        -weights: matrici de valori aleatorii , fiecare linie corespunda unui neuron intr-un layer anterior,
        fiecare coloana pentru neuron din layer-ul curent ( prev layer x current layer )
        -biases: vector de zero , pentru fiecare neuron , valori ce vor fi modificate
        -scale factor care determina variata valorilor initiale aleatorii
        :param nr_features: numarul de param de intrare
        :param nr_outputs: numarul de param de iesire
        :return:
        """
        layer_sizes = [nr_features] + list(self.hidden_layers) + [nr_outputs]

        for i in range(1, len(layer_sizes)):
            if self.activation == 'relu':
                scale = np.sqrt(2.0 / layer_sizes[i - 1])
            else:
                scale = np.sqrt(1.0 / layer_sizes[i - 1])

            self.weights.append(np.random.randn(layer_sizes[i - 1], layer_sizes[i]) * scale)
            self.biases.append(np.zeros(layer_sizes[i]))

    def _forward_pass(self, x):
        activations = [x]

        for i in range(len(self.weights)):
            z = np.dot(activations[-1], self.weights[i]) + self.biases[i]
            activations.append(self.activation_func(z))

        return activations

    def _backward_pass(self, X, y, activations):
        n_samples = X.shape[0]
        n_layers = len(activations)

        weight_gradients = [np.zeros_like(w) for w in self.weights]
        bias_gradients = [np.zeros_like(b) for b in self.biases]

        # Output Error
        delta = activations[-1] - y

        for layer in range(n_layers - 2, -1, -1):
            weight_gradients[layer] = np.dot(activations[layer].T, delta) / n_samples
            bias_gradients[layer] = np.mean(delta, axis=0)

            if layer > 0:
                delta = np.dot(delta, self.weights[layer].T)
                delta *= self.activation_derivative(activations[layer])

        return weight_gradients, bias_gradients

    def _update_params(self, weight_gradients, bias_gradients, learning_rate):
        for i in range(len(self.weights)):
            self.weights[i] -= learning_rate * weight_gradients[i]
            self.biases[i] -= learning_rate * bias_gradients[i]

    def _adam_update(self, weight_gradients, bias_gradients, learning_rate):
        if not hasattr(self, 'm_weights'):
            # Initialize Adam parameters
            self.m_weights = [np.zeros_like(w) for w in self.weights]
            self.v_weights = [np.zeros_like(w) for w in self.weights]
            self.m_biases = [np.zeros_like(b) for b in self.biases]
            self.v_biases = [np.zeros_like(b) for b in self.biases]
            self.beta1 = 0.9
            self.beta2 = 0.999
            self.epsilon = 1e-8
            self.t = 0

        self.t += 1
        for i in range(len(self.weights)):
            self.m_weights[i] = self.beta1 * self.m_weights[i] + (1 - self.beta1) * weight_gradients[i]
            self.v_weights[i] = self.beta2 * self.v_weights[i] + (1 - self.beta2) * np.square(weight_gradients[i])

            # Bias correction
            m_weights_corrected = self.m_weights[i] / (1 - self.beta1 ** self.t)
            v_weights_corrected = self.v_weights[i] / (1 - self.beta2 ** self.t)

            # Update weights
            self.weights[i] -= learning_rate * m_weights_corrected / (np.sqrt(v_weights_corrected) + self.epsilon)

            # Update momentum and RMSprop for biases
            self.m_biases[i] = self.beta1 * self.m_biases[i] + (1 - self.beta1) * bias_gradients[i]
            self.v_biases[i] = self.beta2 * self.v_biases[i] + (1 - self.beta2) * np.square(bias_gradients[i])

            # Bias correction
            m_biases_corrected = self.m_biases[i] / (1 - self.beta1 ** self.t)
            v_biases_corrected = self.v_biases[i] / (1 - self.beta2 ** self.t)

            # Update biases
            self.biases[i] -= learning_rate * m_biases_corrected / (np.sqrt(v_biases_corrected) + self.epsilon)

    def fit(self, X, y):
        y = np.reshape(y, (-1, 1))

        n_samples, n_features = X.shape
        n_outputs = y.shape[1]

        self._initialize_parameters(n_features, n_outputs)

        if isinstance(self.learning_rate, (int, float)):
            learning_rate = float(self.learning_rate)
        else:
            learning_rate = self.learning_rate_init

        if self.verbose > 0:
            print(f"Training neural network with {len(self.hidden_layers)} hidden layers...")
            print(f"Architecture: {n_features} -> {' -> '.join(map(str, self.hidden_layers))} -> {n_outputs}")

        self.loss_history = []

        for epoch in range(self.max_iter):
            activations = self._forward_pass(X)
            y_pred = activations[-1]

            loss = compute_loss(y, y_pred)
            self.loss_history.append(loss)

            if self.learning_rate == 'invscaling':
                learning_rate = self.learning_rate_init / (1 + 0.0001 * epoch)

            weight_gradients, bias_gradients = self._backward_pass(X, y, activations)

            if self.solver == 'sgd':
                self._update_params(weight_gradients, bias_gradients, learning_rate)
            else:
                if self.solver == 'adam':
                    self._adam_update(weight_gradients, bias_gradients, learning_rate)

            if self.verbose > 0 and ((epoch + 1) % self.verbose == 0 or epoch == self.max_iter - 1):
                print(f"Epoch {epoch + 1}/{self.max_iter}: loss: {loss:.6f}, lr: {learning_rate:.6f}")

        if self.verbose > 0:
            print(f"Training complete. Final loss: {self.loss_history[-1]:.6f}")

        return self

    def predict(self, X):
        return self._forward_pass(X)[-1]
