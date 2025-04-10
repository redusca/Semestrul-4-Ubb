"""
trainPlots.py

Modul folosit pentru creeare datele de antrenare si validare si
validarea datelor de calcul
Functiile Compare_ compara datele din 2 surse

"""

import matplotlib.pyplot as plt
import numpy as np


# Sursa -  AI-UBB

def UniSelectTVdata(inputs, outputs, labelInput, labelOutput, seed=5):
    """
    Functia selecteaza datele de antrenare si validare pentru regresie univariata
    :param inputs: datele de input
    :param outputs: datele de output
    :param labelInput: eticheta pentru input
    :param labelOutput: eticheta pentru output
    :param seed: seed pentru functia de random
    :type inputs: list
    :type outputs: list
    :type labelInput: str
    :type labelOutput: str
    :type seed: int
    :return: datele de antrenare(input/output) si validare(input/output)
    :rtype: tuple[list, list, list, list]
    """
    # Folosesc un seed pentru a separa datele de antrenare 80% si datele de validare 20%
    np.random.seed(seed)
    indexes = [i for i in range(len(inputs))]
    trainSample = np.random.choice(indexes, int(0.8 * len(inputs)), replace=False)
    validationSample = [i for i in indexes if not i in trainSample]

    # Plot datele de antrenare si validare
    trainInputs = [inputs[i] for i in trainSample]
    trainOutputs = [outputs[i] for i in trainSample]

    validationInputs = [inputs[i] for i in validationSample]
    validationOutputs = [outputs[i] for i in validationSample]

    plt.plot(trainInputs, trainOutputs, 'ro', label='training data')  # train data are plotted by red and circle sign
    plt.plot(validationInputs, validationOutputs, 'g^',
             label='validation data')  # test data are plotted by green and a triangle sign
    plt.title('train and validation data')
    plt.xlabel(labelInput)
    plt.ylabel(labelOutput)
    plt.legend()
    plt.show()

    return trainInputs, trainOutputs, validationInputs, validationOutputs


def BiSelectTVdata(inputsFirst, inputsSecond, outputs, labelInputFirst, labelInputSecond, labelOutput, seed=5):
    """
    Functia selecteaza datele de antrenare si validare pentru regresie bivariata
    :param inputsFirst: datele de input prima axa
    :param inputsSecond: datele de input a doua axa
    :param outputs: datele de output
    :param labelInputFirst: eticheta pentru input prima axa
    :param labelInputSecond: eticheta pentru input a doua axa
    :param labelOutput: eticheta pentru output
    :param seed: seed pentru functia de random
    :type inputsFirst: list
    :type inputsSecond: list
    :type outputs: list
    :type labelInputFirst: str
    :type labelInputSecond: str
    :type labelOutput: str
    :type seed: int
    :return: datele de antrenament (input1/input2/output) si validare(input1/input2/output)
    :rtype: tuple[list, list, list, list, list, list]
    """

    # Folosesc un seed pentru a separa datele de antrenare 80% si datele de validare 20%
    np.random.seed(seed)
    indexes = [i for i in range(len(inputsFirst))]
    trainSample = np.random.choice(indexes, int(0.8 * len(inputsFirst)), replace=False)
    validationSample = [i for i in indexes if not i in trainSample]

    # Plot datele de antrenare si validare
    trainInputsFirst = [inputsFirst[i] for i in trainSample]
    trainInputsSecond = [inputsSecond[i] for i in trainSample]
    trainOutputs = [outputs[i] for i in trainSample]

    validationInputsFirst = [inputsFirst[i] for i in validationSample]
    validationInputsSecond = [inputsSecond[i] for i in validationSample]
    validationOutputs = [outputs[i] for i in validationSample]

    ax = plt.figure().add_subplot(projection='3d')
    ax.scatter(inputsFirst, inputsSecond, outputs, c='r', marker='o')

    ax.plot(trainInputsFirst, trainInputsSecond, trainOutputs, 'ro', label='training data')
    ax.plot(validationInputsFirst, validationInputsSecond, validationOutputs, 'g^', label='validation data')

    plt.title('train and validation data')
    ax.set_xlabel(labelInputFirst)
    ax.set_ylabel(labelInputSecond)
    ax.set_zlabel(labelOutput)

    plt.legend()
    plt.show()

    return trainInputsFirst, trainInputsSecond, trainOutputs, validationInputsFirst, validationInputsSecond, validationOutputs


# Sursa -  AI-UBB - 50%
def Compare_ValidationVSComputed(validationInputs, validationOutputs, computedOutputsA, computedOutputsB, labelInput,
                                 labelOutput):
    """
    Functie de comparare a doua regresii
    Functia compara datele de validare cu datele calculate ale regresiei liniare
    :param validationInputs: datele de validare pentru input
    :param validationOutputs: datele de validare pentru output
    :param computedOutputsA: datele calculate pentru input ale regresiei A
    :param computedOutputsB: datele calculate pentru input ale regresiei B
    :param labelInput: eticheta pentru input
    :param labelOutput: eticheta pentru output
    :type validationInputs: list
    :type validationOutputs: list
    :type computedOutputsA: list
    :type computedOutputsB: list
    :type labelInput: str
    :type labelOutput: str
    """
    fig2, ax = plt.subplots(1, 2, figsize=(15, 5))

    ax[0].plot(validationInputs, validationOutputs, 'y^',
               label='real test data')  # real test data are plotted by yellow triangles
    ax[1].plot(validationInputs, validationOutputs, 'y^')

    ax[0].plot(validationInputs, computedOutputsA, 'bo', label='computed test data tool')
    ax[1].plot(validationInputs, computedOutputsB, 'go', label='computed test data code')

    plt.title('computed validation and real validation data')

    ax[0].set_title("Plot Tool")
    ax[1].set_title("Plot Code")

    ax[0].set_xlabel(labelInput)
    ax[0].set_ylabel(labelOutput)

    ax[1].set_xlabel(labelInput)
    ax[1].set_ylabel(labelOutput)

    fig2.legend()

    plt.show()


def Compare_ValidationVSComputed3D(validationInputsFirst, validationInputsSecond, validationOutputs, computedOutputsA, computedOutputsB
                                   , labelInputFirst, labelInputSecond, labelOutput):
    """
    Functie de comparare a doua regresii
    Functia compara datele de validare cu datele calculate ale regresiei bivariate
    :param validationInputsFirst: datele de validare pentru input prima axa
    :param validationInputsSecond: datele de validare pentru input a doua axa
    :param validationOutputs: datele de validare pentru output
    :param computedOutputsA: datele calculate  ale regresiei A
    :param computedOutputsB: datele calculate  ale regresiei B
    :param labelInputFirst: eticheta pentru input prima axa
    :param labelInputSecond: eticheta pentru input a doua axa
    :param labelOutput: eticheta pentru output
    """
    fig2, ax = plt.subplots(1, 2, figsize=(15, 5), subplot_kw={'projection': '3d'})

    ax[0].scatter(validationInputsFirst, validationInputsSecond, validationOutputs, c='y', marker='^',
                  label='real test data')  # real test data are plotted by yellow triangles
    ax[1].scatter(validationInputsFirst, validationInputsSecond, validationOutputs, c='y', marker='^')

    ax[0].scatter(validationInputsFirst, validationInputsSecond, computedOutputsA, c='b', marker='o',
                  label='computed test data tool')
    ax[1].scatter(validationInputsFirst, validationInputsSecond, computedOutputsB, c='g', marker='o',
                  label='computed test data code')

    ax[0].set_xlabel(labelInputFirst)
    ax[1].set_xlabel(labelInputFirst)
    ax[0].set_ylabel(labelInputSecond)
    ax[1].set_ylabel(labelInputSecond)
    ax[0].set_zlabel(labelOutput)
    ax[1].set_zlabel(labelOutput)

    plt.title('computed validation and real validation data')

    ax[0].set_title("Plot Tool")
    ax[1].set_title("Plot Code")

    fig2.legend()

    plt.show()
