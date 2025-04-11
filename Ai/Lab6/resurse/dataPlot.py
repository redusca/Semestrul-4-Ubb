"""

dataPlot.py

Modul folosit pentru afisarea datelor
Functiile Compare_ compara datele din 2 surse

"""
import matplotlib.pyplot as plt
import numpy as np


def plotDataBar(x, variableName):
    """
    Functie pentru afisarea datelor sub forma de bara
    :param x: lista de date
    :param variableName: eticheta pentru date
    :type x: list
    :type variableName: str
    """
    M_count = len([el for el in x if el])
    plt.bar(['B', 'M'],
            [len(x) - M_count, M_count]
            , color=['blue', 'red'])
    plt.text(0, len(x) - M_count, str(len(x) - M_count), ha='center', va='bottom')
    plt.text(1, M_count, str(M_count), ha='center', va='bottom')
    plt.title('Bar plot of ' + variableName)
    plt.show()

def plotDataBar2(x, variableName):
    """

    :param x:
    :param variableName:
    :return:
    """
    IS_count = len([el for el in x if el == 'Iris-setosa'])
    IVC_count = len([el for el in x if el == 'Iris-versicolor'])
    IV_count = len(x) - IS_count - IVC_count

    plt.bar(['IS', 'IVC', 'IV'],
            [IS_count, IVC_count, IV_count]
            , color=['blue', 'red', 'green'])
    plt.text(0, IS_count, str(IS_count), ha='center', va='bottom')
    plt.text(1, IVC_count, str(IVC_count), ha='center', va='bottom')
    plt.text(2, IV_count, str(IV_count), ha='center', va='bottom')
    plt.title('Bar plot of ' + variableName)
    plt.show()

# Sursa -  AI-UBB
def plotDataHistogram(x, variableName):
    """
    Functie pentur Histograma unor date
    :param x: datele anonimate
    :param variableName: numele variabilei
    :type x: list
    :type variableName: str
    """
    n, bins, patches = plt.hist(x, 10)
    plt.title('Histogram of ' + variableName)
    plt.show()


# Sursa -  AI-UBB
def PlotLiniarData(inputs, outputs, labelInput, labelOutput):
    """
    Functie pentru verificare liniaritatii output-ului
    :param inputs: datele de input
    :param outputs: datele de output
    :param labelInput: eticheta pentru input
    :param labelOutput: eticheta pentru output
    :type inputs: list
    :type outputs: list
    :type labelInput: str
    :type labelOutput: str
    """
    plt.plot(inputs, outputs, 'ro', label='train data')
    plt.xlabel(labelInput)
    plt.ylabel(labelOutput)
    plt.legend()
    plt.show()


def Plot3DLiniardata(inputsFirst, inputsSecond, outputs, labelInputFirst, labelInputSecond, labelOutput):
    """
    Functie pentru verificarea liniaritatii output-ului in mediu 3D
    :param inputsFirst: datele de input prima axa
    :param inputsSecond: datele de input a doua axa
    :param outputs: datele de output
    :param labelInputFirst: eticheta pentru input prima axa
    :param labelInputSecond: eticheta pentru input a doua axa
    :param labelOutput: eticheta pentru output
    :type inputsFirst: list
    :type inputsSecond: list
    :type outputs: list
    :type labelInputFirst: str
    :type labelInputSecond: str
    :type labelOutput: str
    """
    ax = plt.figure().add_subplot(projection='3d')

    ax.scatter(inputsFirst, inputsSecond, outputs, c='r', marker='o', label='train data')
    ax.set_xlabel(labelInputFirst)
    ax.set_ylabel(labelInputSecond)
    ax.set_zlabel(labelOutput)

    plt.legend()
    plt.show()


# Sursa -  AI-UBB 50%
def Compare_LiniarData(inputs, outputs, w0_a, w1_a, w0_b, w1_b, labelInput, labelOuput):
    """
    Functia de comparare a 2 regresii liniare
    Functia afiseaza graficul regresiei liniare comparativ cu datele initiale de output
    :param inputs: date de input
    :param outputs: date de output
    :param w0_a: w0 regresia A (tool)
    :param w1_a: w1 regresia A (tool)
    :param w0_b: w0 regresia B (code)
    :param w1_b: w0 regresia B (code)
    :param labelInput: eticheta pentru input
    :param labelOuput: eticheta pentru output
    :type inputs: list
    :type outputs: list
    :type w0_a: float
    :type w1_a: float
    :type w0_b: float
    :type w1_b: float
    :type labelInput: str
    :type labelOuput: str
    :return:
    """

    # generarea output-ului pentru regresii
    noOfPoints = 1000
    xref = []
    val = min(inputs)
    step = (max(inputs) - min(inputs)) / noOfPoints

    for i in range(1, noOfPoints):
        xref.append(val)
        val += step
    yref_Aplot = [w0_a + w1_a * el for el in xref]
    yref_Bplot = [w0_b + w1_b * el for el in xref]

    # compareae regresiilor cu output-ul dat
    fig, ax = plt.subplots(1, 2, figsize=(15, 5))
    plt.title('plot regresions')
    ax[0].plot(inputs, outputs, 'ro', label='training data')  # train data are plotted by red and circle sign
    ax[0].plot(xref, yref_Aplot, 'b-', label='learnt model by tool')  # model is plotted by a blue line

    ax[1].plot(inputs, outputs, 'ro')
    ax[1].plot(xref, yref_Bplot, 'g-', label='learnt model by code')

    ax[0].set_title("Plot Tool")
    ax[1].set_title("Plot Code")

    ax[0].set_xlabel(labelInput)
    ax[0].set_ylabel(labelOuput)

    ax[1].set_xlabel(labelInput)
    ax[1].set_ylabel(labelOuput)

    fig.legend()

    plt.show()


# Sursa Ai-Ubb
def Compare_PlaneData(inputsFirst, inputsSecond, outputs, w0_a, w1_a, w2_a, w0_b, w1_b, w2_b, labelInputFirst,
                      labelInputSecond, labelOuput):
    """
    Functia de comparare a 2 regresii bivariate
    Functia afiseaza graficul regresiei bivariata comparativ cu datele initiale de output
    :param inputsFirst: datele de input prima axa
    :param inputsSecond: datele de input a doua axa
    :param outputs: datele de output
    :param w0_a: w0 regresia A (tool)
    :param w1_a: w1 regresia A (tool)
    :param w2_a: w2 regresia A (tool)
    :param w0_b: w0 regresia B (code)
    :param w1_b: w1 regresia B (code)
    :param w2_b: w2 regresia B (code)
    :param labelInputFirst: eticheta pentru input prima axa
    :param labelInputSecond: eticheta pentru input a doua axa
    :param labelOuput: eticheta pentru output
    :type inputsFirst: list
    :type inputsSecond: list
    :type outputs: list
    :type w0_a: float
    :type w1_a: float
    :type w2_a: float
    :type w0_b: float
    :type w1_b: float
    :type w2_b: float
    :type labelInputFirst: str
    :type labelInputSecond: str
    :type labelOuput: str
    """
    noOfPoints = 50
    xref1 = []
    val = min(inputsFirst)
    step1 = (max(inputsFirst) - min(inputsFirst)) / noOfPoints
    for _ in range(1, noOfPoints):
        for _ in range(1, noOfPoints):
            xref1.append(val)
        val += step1

    xref2 = []
    val = min(inputsSecond)
    step2 = (max(inputsSecond) - min(inputsSecond)) / noOfPoints
    for _ in range(1, noOfPoints):
        aux = val
        for _ in range(1, noOfPoints):
            xref2.append(aux)
            aux += step2

    yref_Aplot = [w0_a + w1_a * el1 + w2_a * el2 for el1, el2 in zip(xref1, xref2)]
    yref_Bplot = [w0_b + w1_b * el1 + w2_b * el2 for el1, el2 in zip(xref1, xref2)]

    fig, ax = plt.subplots(1, 2, figsize=(15, 5), subplot_kw={'projection': '3d'})

    ax[0].scatter(inputsFirst, inputsSecond, outputs, c='r', marker='o', label='training data')
    ax[0].scatter(xref1, xref2, yref_Aplot, c='b', marker='o', label='learnt model by tool')

    ax[1].scatter(inputsFirst, inputsSecond, outputs, c='r', marker='o')
    ax[1].scatter(xref1, xref2, yref_Bplot, c='g', marker='o', label='learnt model by code')

    ax[0].set_xlabel(labelInputFirst)
    ax[0].set_ylabel(labelInputSecond)
    ax[0].set_zlabel(labelOuput)

    ax[1].set_xlabel(labelInputFirst)
    ax[1].set_ylabel(labelInputSecond)
    ax[1].set_zlabel(labelOuput)

    plt.legend()
    plt.show()

def Compare_SigmoidData(inputsFirst, inputsSecond, outputs, w0_a, w1_a, w2_a, w0_b, w1_b, w2_b, labelInputFirst,
                      labelInputSecond, labelOuput):
    """
    Functia de comparare a 2 regresii bivariate
    Functia afiseaza graficul regresiei bivariata comparativ cu datele initiale de output bazata pe regresia logistica
    :param inputsFirst: datele de input prima axa
    :param inputsSecond: datele de input a doua axa
    :param outputs: datele de output
    :param w0_a: w0 regresia A (tool)
    :param w1_a: w1 regresia A (tool)
    :param w2_a: w2 regresia A (tool)
    :param w0_b: w0 regresia B (code)
    :param w1_b: w1 regresia B (code)
    :param w2_b: w2 regresia B (code)
    :param labelInputFirst: eticheta pentru input prima axa
    :param labelInputSecond: eticheta pentru input a doua axa
    :param labelOuput: eticheta pentru output
    :type inputsFirst: list
    :type inputsSecond: list
    :type outputs: list
    :type w0_a: float
    :type w1_a: float
    :type w2_a: float
    :type w0_b: float
    :type w1_b: float
    :type w2_b: float
    """
    x = np.arange(min(inputsFirst), max(inputsFirst), step=0.1)
    y = np.arange(min(inputsSecond), max(inputsSecond), step=0.1)
    x,y = np.meshgrid(x,y)

    v_sigmod = np.vectorize(lambda x: 1 / (1 + np.exp(-x))) # sigmoid function

    z_a = w0_a + w1_a * x + w2_a * y
    z_a = v_sigmod(z_a)

    z_b = w0_b + w1_b * x + w2_b * y
    z_b = v_sigmod(z_b)

    fig, ax = plt.subplots(1, 2, figsize=(15, 5), subplot_kw={'projection': '3d'})

    ax[0].scatter(inputsFirst, inputsSecond, outputs, c='r', marker='o', label='training data')
    ax[0].plot_surface(x, y, 0.5 + x*0 + y*0, alpha=0.5, color='yellow', label='Treshold')  #plan pt treshold
    ax[0].plot_surface(x, y, z_a, alpha=0.7, color='blue',label='learnt model by tool')

    ax[1].scatter(inputsFirst, inputsSecond, outputs, c='r', marker='o')
    ax[1].plot_surface(x, y, 0.5 + x*0 + y*0, alpha=0.5,color='yellow')
    ax[1].plot_surface(x, y, z_b, alpha=0.7, label='learnt model by code', color='green')

    ax[0].set_xlabel(labelInputFirst)
    ax[0].set_ylabel(labelInputSecond)
    ax[0].set_zlabel(labelOuput)

    ax[1].set_xlabel(labelInputFirst)
    ax[1].set_ylabel(labelInputSecond)
    ax[1].set_zlabel(labelOuput)

    plt.legend()
    plt.show()