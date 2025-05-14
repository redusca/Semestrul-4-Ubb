import pickle

import numpy as np
from typing import List, Union, Literal, Optional, Tuple, Callable
import warnings

warnings.filterwarnings("ignore", category=RuntimeWarning)


def compute_loss(y_true, y_pred):
    return np.mean(np.square(y_true - y_pred))


class CNNCode:
    filters: list[int]
    kernel_sizes: list[tuple]
    strides: list[tuple]
    padding: Literal['valid', 'same'] = 'valid'
    activation: Literal['relu', 'tanh'] = 'relu'
    pooling: Literal['max', 'avg'] = 'max'
    pooling_sizes: list[tuple]
    pooling_strides: list[tuple]
    hidden_layers: list[int]
    max_iter: int
    solver: Literal['sgd'] = 'sgd'
    random_state: int
    verbose: int
    learning_rate: Union[Literal['constant', 'invscaling'], float] = 'constant'
    learning_rate_init: float

    def __init__(
            self,
            filters,
            kernel_sizes,
            strides=None,
            padding='valid',
            activation='relu',
            pooling='max',
            pooling_sizes=None,
            pooling_strides=None,
            hidden_layers=None,
            max_iter=200,
            solver='adam',
            random_state=None,
            verbose=0,
            learning_rate='constant',
            learning_rate_init=0.001
    ):
        """
        :param filters: numărul de filtre pe fiecare strat convoluțional
        :param kernel_sizes: dimensiunile kernel-urilor pentru fiecare strat convoluțional
        :param strides: pașii de deplasare pentru fiecare operație de convoluție
        :param padding: tipul de padding ('valid' sau 'same')
        :param activation: funcția de activare pentru neuroni
        :param pooling: tipul de pooling ('max' sau 'avg')
        :param pooling_sizes: dimensiunile ferestrelor de pooling
        :param pooling_strides: pașii de deplasare pentru operațiile de pooling
        :param hidden_layers: numărul de neuroni din straturile fully connected după convoluții
        :param max_iter: numărul de iterații ale rețelei
        :param solver: algoritmul de optimizare pentru actualizarea ponderilor
        :param random_state: pentru generarea aleatorie reproductibilă
        :param verbose: din cât în cât să se afișeze loss-ul în iterații
        :param learning_rate: tipul de învățare, constant sau se schimbă learning rate-ul
        :param learning_rate_init: valoarea inițială cu care învață rețeaua neuronală
        """
        self.filters = filters
        self.kernel_sizes = kernel_sizes
        self.padding = padding
        self.activation = activation
        self.pooling = pooling

        if strides is None:
            self.strides = [(1, 1) for _ in range(len(filters))]
        else:
            self.strides = strides

        if pooling_sizes is None:
            self.pooling_sizes = [(2, 2) for _ in range(len(filters))]
        else:
            self.pooling_sizes = pooling_sizes

        if pooling_strides is None:
            self.pooling_strides = [(2, 2) for _ in range(len(filters))]
        else:
            self.pooling_strides = pooling_strides

        if hidden_layers is None:
            self.hidden_layers = (100)
        else:
            self.hidden_layers = hidden_layers

        self.max_iter = max_iter
        self.solver = solver
        self.random_state = random_state
        self.verbose = verbose
        self.learning_rate = learning_rate
        self.learning_rate_init = learning_rate_init

        if random_state is not None:
            np.random.seed(random_state)

        if self.solver != 'sgd' and self.solver != 'adam':
            raise ValueError(f"Unsupported solver: {self.solver}")

        self.conv_weights = []
        self.conv_biases = []
        self.fc_weights = []
        self.fc_biases = []
        self.n_conv_layers = len(filters)
        self.loss_history = []

        self.activation_func = self._get_activation_function()
        self.activation_derivative = self._get_activation_derivative()

    def _get_activation_function(self):
        if self.activation == 'relu':
            return lambda x: np.maximum(0, x)
        elif self.activation == 'tanh':
            return lambda x: np.tanh(x)
        else:
            raise ValueError(f"Unsupported activation function: {self.activation}")

    def _get_activation_derivative(self):
        if self.activation == 'relu':
            return lambda x: np.where(x > 0, 1, 0)
        elif self.activation == 'tanh':
            # f'(x) = 1 - f(x)^2
            return lambda x: 1 - np.square(self.activation_func(x))
        else:
            raise ValueError(f"Unsupported activation function: {self.activation}")

    def _initialize_parameters(self, input_shape, n_outputs):
        channels, height, width = input_shape

        current_channels = channels
        current_height = height
        current_width = width

        for i in range(self.n_conv_layers):
            if self.padding == 'valid':
                out_height = current_height - self.kernel_sizes[i][0] + 1
                out_width = current_width - self.kernel_sizes[i][1] + 1
            else:
                out_height = current_height
                out_width = current_width

            kernel_shape = (self.filters[i], current_channels, self.kernel_sizes[i][0], self.kernel_sizes[i][1])

            if self.activation == 'relu':
                scale = np.sqrt(2.0 / (current_channels * self.kernel_sizes[i][0] * self.kernel_sizes[i][1]))
            else:
                scale = np.sqrt(1.0 / (current_channels * self.kernel_sizes[i][0] * self.kernel_sizes[i][1]))

            self.conv_weights.append(np.random.randn(*kernel_shape) * scale)
            self.conv_biases.append(np.zeros((1, self.filters[i], 1, 1)))

            out_height = out_height // self.pooling_strides[i][0]
            out_width = out_width // self.pooling_strides[i][1]

            current_channels = self.filters[i]
            current_height = out_height
            current_width = out_width

        flattened_size = current_channels * current_height * current_width

        fc_layer_sizes = [flattened_size] + list(self.hidden_layers) + [n_outputs]

        for i in range(1, len(fc_layer_sizes)):
            if self.activation == 'relu':
                scale = np.sqrt(2.0 / fc_layer_sizes[i - 1])
            else:
                scale = np.sqrt(1.0 / fc_layer_sizes[i - 1])

            self.fc_weights.append(np.random.randn(fc_layer_sizes[i - 1], fc_layer_sizes[i]) * scale)
            self.fc_biases.append(np.zeros(fc_layer_sizes[i]))

    def _conv2d(self, input_data, kernel, stride=(1, 1), padding='valid'):
        batch_size, in_channels, in_height, in_width = input_data.shape
        out_channels, _, kernel_height, kernel_width = kernel.shape

        pad_h, pad_w = 0, 0
        if padding == 'same':
            pad_h = (kernel_height - 1) // 2
            pad_w = (kernel_width - 1) // 2

        out_height = (in_height + 2 * pad_h - kernel_height) // stride[0] + 1
        out_width = (in_width + 2 * pad_w - kernel_width) // stride[1] + 1

        output = np.zeros((batch_size, out_channels, out_height, out_width))

        if pad_h > 0 or pad_w > 0:
            padded_input = np.pad(input_data,
                                  ((0, 0), (0, 0), (pad_h, pad_h), (pad_w, pad_w)),
                                  mode='constant')
        else:
            padded_input = input_data
        i = 0
        p = 0
        max_cal = batch_size * out_channels * out_height * out_width
        for b in range(batch_size):
            for c_out in range(out_channels):
                for h_out in range(out_height):
                    for w_out in range(out_width):
                        h_start = h_out * stride[0]
                        w_start = w_out * stride[1]
                        h_end = h_start + kernel_height
                        w_end = w_start + kernel_width
                        if i % (max_cal / 10) == 0:
                            print(f'{p}% | 100% ')
                            p += 10
                        i += 1
                        patch = padded_input[b, :, h_start:h_end, w_start:w_end]

                        output[b, c_out, h_out, w_out] = np.sum(patch * kernel[c_out])
        return output

    def _pooling(self, input_data, pool_size=(2, 2), stride=(2, 2), mode='max'):
        batch_size, channels, in_height, in_width = input_data.shape

        out_height = (in_height - pool_size[0]) // stride[0] + 1
        out_width = (in_width - pool_size[1]) // stride[1] + 1

        output = np.zeros((batch_size, channels, out_height, out_width))

        i = 0
        p = 0
        max_cal = batch_size * channels * out_height * out_width
        for b in range(batch_size):
            for c in range(channels):
                for h_out in range(out_height):
                    for w_out in range(out_width):
                        h_start = h_out * stride[0]
                        w_start = w_out * stride[1]
                        h_end = h_start + pool_size[0]
                        w_end = w_start + pool_size[1]

                        if i % (max_cal / 10) == 0:
                            print(f'{p}% | 100% ')
                            p += 10
                        i += 1

                        patch = input_data[b, c, h_start:h_end, w_start:w_end]

                        if mode == 'max':
                            output[b, c, h_out, w_out] = np.max(patch)
                        else:
                            output[b, c, h_out, w_out] = np.mean(patch)

        return output

    def _forward_pass(self, X):
        activations = [X]

        for i, bias in enumerate(self.conv_biases):
            z = self._conv2d(activations[-1],
                             self.conv_weights[i],
                             stride=self.strides[i],
                             padding=self.padding) + bias

            a = self.activation_func(z)
            activations.append(a)

            p = self._pooling(a,
                              pool_size=self.pooling_sizes[i],
                              stride=self.pooling_strides[i],
                              mode=self.pooling)
            activations.append(p)

        batch_size = X.shape[0]
        flattened = activations[-1].reshape(batch_size, -1)
        activations.append(flattened)

        for i in range(len(self.fc_weights)):
            z = np.dot(activations[-1], self.fc_weights[i]) + self.fc_biases[i]
            activations.append(z)

        return activations

    def _backward_pass(self, X, y, activations):
        batch_size = X.shape[0]

        conv_weight_gradients = [np.zeros_like(w) for w in self.conv_weights]
        conv_bias_gradients = [np.zeros_like(b) for b in self.conv_biases]
        fc_weight_gradients = [np.zeros_like(w) for w in self.fc_weights]
        fc_bias_gradients = [np.zeros_like(b) for b in self.fc_biases]

        output_error = activations[-1] - y
        delta = output_error

        fc_activation_indices = []
        fc_start_idx = len(activations) - len(self.fc_weights) - 1
        for i in range(fc_start_idx, len(activations)):
            fc_activation_indices.append(i)

        for i in range(len(self.fc_weights) - 1, -1, -1):
            curr_idx = fc_activation_indices[i + 1]
            prev_idx = fc_activation_indices[i]

            fc_weight_gradients[i] = np.dot(activations[prev_idx].T, delta) / batch_size
            fc_bias_gradients[i] = np.mean(delta, axis=0)

            if i > 0:
                delta = np.dot(delta, self.fc_weights[i].T)
                z_prev = np.dot(activations[fc_activation_indices[i - 1]], self.fc_weights[i - 1]) + self.fc_biases[
                    i - 1]
                delta *= self.activation_derivative(z_prev)

        for i in range(self.n_conv_layers):
            conv_bias_gradients[i] = np.zeros_like(self.conv_biases[i])
            conv_weight_gradients[i] = np.zeros_like(self.conv_weights[i])

        return conv_weight_gradients, conv_bias_gradients, fc_weight_gradients, fc_bias_gradients

    def _update_params(self, conv_weight_gradients, conv_bias_gradients,
                       fc_weight_gradients, fc_bias_gradients, learning_rate):
        for i in range(len(self.conv_weights)):
            if i < len(conv_weight_gradients) and conv_weight_gradients[i] is not None:
                if self.conv_weights[i].shape == conv_weight_gradients[i].shape:
                    self.conv_weights[i] -= learning_rate * conv_weight_gradients[i]

            if i < len(conv_bias_gradients) and conv_bias_gradients[i] is not None:
                if self.conv_biases[i].shape == conv_bias_gradients[i].shape:
                    self.conv_biases[i] -= learning_rate * conv_bias_gradients[i]

        for i in range(len(self.fc_weights)):
            if i < len(fc_weight_gradients) and fc_weight_gradients[i] is not None:
                if self.fc_weights[i].shape == fc_weight_gradients[i].shape:
                    self.fc_weights[i] -= learning_rate * fc_weight_gradients[i]
                else:
                    print(
                        f"Warning: Shape mismatch for fc_weights[{i}]. Expected {self.fc_weights[i].shape}, got {fc_weight_gradients[i].shape}")

            if i < len(fc_bias_gradients) and fc_bias_gradients[i] is not None:
                if self.fc_biases[i].shape == fc_bias_gradients[i].shape:
                    self.fc_biases[i] -= learning_rate * fc_bias_gradients[i]
                else:
                    print(
                        f"Warning: Shape mismatch for fc_biases[{i}]. Expected {self.fc_biases[i].shape}, got {fc_bias_gradients[i].shape}")

    def fit(self, X, y):
        if len(X.shape) != 4:
            raise ValueError("Input data should be 4D: (batch_size, channels, height, width)")

        if len(y.shape) == 1:
            y = np.reshape(y, (-1, 1))

        n_samples, channels, height, width = X.shape
        n_outputs = y.shape[1]

        self._initialize_parameters((channels, height, width), n_outputs)

        if isinstance(self.learning_rate, (int, float)):
            learning_rate = float(self.learning_rate)
        else:
            learning_rate = self.learning_rate_init

        if self.verbose > 0:
            print(
                f"Training CNN with {self.n_conv_layers} convolutional layers and {len(self.hidden_layers)} fully connected layers...")
            print(f"Input shape: ({channels}, {height}, {width})")

        self.loss_history = []

        for epoch in range(self.max_iter):
            print("start")
            activations = self._forward_pass(X)
            y_pred = activations[-1]
            print("foward")
            loss = compute_loss(y, y_pred)
            self.loss_history.append(loss)

            if self.learning_rate == 'invscaling':
                learning_rate = self.learning_rate_init / (1 + 0.0001 * epoch)

            conv_weight_gradients, conv_bias_gradients, fc_weight_gradients, fc_bias_gradients = self._backward_pass(X,
                                                                                                                     y,
                                                                                                                     activations)

            print("backward")
            if self.solver == 'sgd':
                self._update_params(conv_weight_gradients, conv_bias_gradients, fc_weight_gradients, fc_bias_gradients,
                                    learning_rate)

            if self.verbose > 0 and ((epoch + 1) % self.verbose == 0 or epoch == self.max_iter - 1):
                print(f"Epoch {epoch + 1}/{self.max_iter}: loss: {loss:.6f}, lr: {learning_rate:.6f}")

        if self.verbose > 0:
            print(f"Training complete. Final loss: {self.loss_history[-1]:.6f}")

        return self

    def predict(self, X):
        return self._forward_pass(X)[-1]

    def save_model(self, file_path: str):
        """
        Save the model's parameters to a file.
        :param file_path: Path to the file where the model will be saved.
        """
        model_data = {
            'filters': self.filters,
            'kernel_sizes': self.kernel_sizes,
            'strides': self.strides,
            'padding': self.padding,
            'activation': self.activation,
            'pooling': self.pooling,
            'pooling_sizes': self.pooling_sizes,
            'pooling_strides': self.pooling_strides,
            'hidden_layers': self.hidden_layers,
            'max_iter': self.max_iter,
            'solver': self.solver,
            'random_state': self.random_state,
            'verbose': self.verbose,
            'learning_rate': self.learning_rate,
            'learning_rate_init': self.learning_rate_init,
            'conv_weights': self.conv_weights,
            'conv_biases': self.conv_biases,
            'fc_weights': self.fc_weights,
            'fc_biases': self.fc_biases,
            'loss_history': self.loss_history
        }
        with open(file_path, 'wb') as f:
            pickle.dump(model_data, f)

    @classmethod
    def load_model(cls, file_path: str):
        """
        Load the model's parameters from a file.
        :param file_path: Path to the file where the model is saved.
        :return: An instance of CNNCode with the loaded parameters.
        """
        with open(file_path, 'rb') as f:
            model_data = pickle.load(f)

        model = cls(
            filters=model_data['filters'],
            kernel_sizes=model_data['kernel_sizes'],
            strides=model_data['strides'],
            padding=model_data['padding'],
            activation=model_data['activation'],
            pooling=model_data['pooling'],
            pooling_sizes=model_data['pooling_sizes'],
            pooling_strides=model_data['pooling_strides'],
            hidden_layers=model_data['hidden_layers'],
            max_iter=model_data['max_iter'],
            solver=model_data['solver'],
            random_state=model_data['random_state'],
            verbose=model_data['verbose'],
            learning_rate=model_data['learning_rate'],
            learning_rate_init=model_data['learning_rate_init']
        )

        model.conv_weights = model_data['conv_weights']
        model.conv_biases = model_data['conv_biases']
        model.fc_weights = model_data['fc_weights']
        model.fc_biases = model_data['fc_biases']
        model.loss_history = model_data['loss_history']

        return model
