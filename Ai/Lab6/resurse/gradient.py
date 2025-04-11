"""
gradient.py
Modul folosit pentru algoritm Gradientului descrescator

Gradientul descrescator este un algoritm de optimizare folosit pentru a minimiza o functie de cost
"""
import math


# Sura -  AI-UBB
def fit(x, y, learningRate=0.001, noEpochs=1000):
    """
    Calcularea coeficientilor pentru regresia liniara folosind stocastic gradientul descrescator
    :param x: listele de input
    :param y: lista de output
    :param learningRate: rata de invatare
    :param noEpochs: numarul de epoci
    :return: coeficientii w0 si w1....wn
    :rtype: tuple[float, list]
    :type x: list[list]
    :type y: list
    :type learningRate: float
    :type noEpochs: int
    """
    coef_ = [0.0 for _ in range(len(x[0]) + 1)]  # coeficienti y = w0 + w1*x1 + ... wn * xn , w0 = coef_[-1]

    for epoch in range(noEpochs):
        for i in range(len(x)):
            y_computed = eval(x[i], coef_)  # estimam outputul
            crtError = y_computed - y[i]  # calculamn eroarea pentru exemplul curent
            for j in range(len(x[0])):  # modificam coeficienti
                coef_[j] -= learningRate * crtError * x[i][j]
            coef_[len(x[0])] -= learningRate * crtError * 1  # pentru w0

    return coef_[-1], coef_  # returnam w0 si ceilatlti coeficienti


def batchfit(x, y, learningRate=0.001, noEpochs=1000):
    """
    Calcularea coeficientilor pentru regresia liniara folosind batch gradientul descrescator
    :param x: listele de input
    :param y: lista de output
    :param learningRate: rata de invatare
    :param noEpochs: numarul de epoci
    :return: coeficientii w0 si w1....wn
    :rtype: tuple[float, list]
    :type x: list[list]
    :type y: list
    :type learningRate: float
    :type noEpochs: int
    """
    coef_ = [0.0 for _ in range(len(x[0]) + 1)]  # coeficienti y = w0 + w1*x1 + ... wn * xn , w0 = coef_[-1]

    for epoch in range(noEpochs):
        # calculam gradientul pentru fiecare coeficient
        gradient = [0.0 for _ in range(len(x[0]) + 1)]  # gradientul pentru fiecare coeficient
        for i in range(len(x)):
            y_computed = eval(x[i], coef_)  # estimam outputul
            crtError = y_computed - y[i]  # calculamn eroarea pentru exemplul curent
            for j in range(len(x[0])):  # modificam coeficienti
                gradient[j] += crtError * x[i][j]
            gradient[len(x[0])] += crtError * 1  # pentru w0

        for j in range(len(x[0])):
            coef_[j] -= learningRate * (gradient[j] / len(x))
        coef_[len(x[0])] -= learningRate * (gradient[len(x[0])] / len(x))

    return coef_[-1], coef_  # returnam w0 si ceilatlti coeficienti


def eval(x, coef_):
    """
    Calcularea outputului pentru un set de date
    :param x: set-ul de date
    :param coef_: w1,w2....wn,w0 ;
    :return: f(x) = w0 + w1*x1 + ... wn * xn = yi
    :rtype: float
    :type x: list
    """
    yi = coef_[-1]
    for j in range(len(x)):
        yi += coef_[j] * x[j]
    return yi


def logistic_fit(x, y, learningRate=0.001, noEpochs=1000):
    """
    Calcularea coeficientilor pentru regresia logistica folosind  gradientul descrescator stocastic
    :param x: listele de input
    :param y: lista de output
    :param learningRate: rata de invatare
    :param noEpochs: numarul de epoci
    :return: coeficientii w0 si w1....wn
    :rtype: tuple[float, list]
    :type x: list[list]
    :type y: list
    :type learningRate: float
    :type noEpochs: int
    """
    coef_ = [0.0 for _ in range(len(x[0]) + 1)]  # coeficienti y = w0 + w1*x1 + ... wn * xn , w0 = coef_[-1]

    for epoch in range(noEpochs):
        for i in range(len(x)):
            y_computed = eval(x[i], coef_)  # estimam outputul
            crtError = sigmoid(y_computed) - y[i]  # calculamn eroarea pentru exemplul curent
            for j in range(len(x[0])):  # modificam coeficienti
                coef_[j] -= learningRate * crtError * x[i][j]
            coef_[len(x[0])] -= learningRate * crtError * 1  # pentru w0

    return coef_[-1], coef_  # returnam w0 si ceilatlti coeficienti


def sigmoid(x):
    """
    Functia sigmoid
    :param x: inputul
    :return: outputul
    :rtype: float
    :type x: float
    """
    return 1 / (1 + math.exp(-x))  # sigmoid function


def predict(x, coef_):
    """
    Calcularea outputului pentru intreg setul de date
    :param x: lista de inputuri(liste)
    :param coef_: w1,w2....wn,w0 ;
    :return: toate outputurile calculate f(x) = w0 + w1*x1 + ... wn * xn = yi
    :rtype: list[float]
    :type x: list[list[float]]
    """
    yComputed = [eval(xi, coef_) for xi in x]
    return yComputed


def probabiliate(x, coef_):
    """
    Functia de probabilitate pentru regresia logistica
    :param x: lista de inputuri(liste)
    :param coef_: w1,w2....wn,w0 ;
    :return: toate probabilitatile calculate f(x) = w0 + w1*x1 + ... wn * xn = yi
    :rtype: list[float]
    :type x: list[list[float]]
    """
    yComputed = [sigmoid(eval(xi, coef_)) for xi in x]
    return yComputed


def clasificare(x, coef_, threshold=0.5):
    """
    Functia de clasificare a outputului in 0 1 cu ajutrul sigmoid
    :param x: lista de inputuri(liste)
    :param coef_: w1,w2....wn,w0 ;
    :param threshold: threshold-ul pentru clasificare 0-1
    :return: lista de 1 sau 0
    :rtype: list[int]
    :type x: list[list[float]]
    :type threshold: float
    """
    yComputed = [1 if sigmoid(eval(xi, coef_)) >= threshold else 0 for xi in x]

    return yComputed
