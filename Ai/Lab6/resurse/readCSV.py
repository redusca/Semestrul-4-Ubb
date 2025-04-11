"""

    readCSV.py

    Modul folosit pentru citirea datelor din fisiere CSV
"""
import csv


# Sursa -  AI-UBB
def ReadSingleInputCSV(fileName, inputIndex, outputIndex):
    """
    Citirea unui set de date de input si unul pentru Output
    Ignora spatiile libere in tuplurie ( i , o)
    :param fileName: denumirea fisierului CSV
    :param inputIndex: index-ul coloanei de input
    :param outputIndex: index-ul coloanei de output
    :type fileName: str
    :type inputIndex: str
    :type outputIndex: str
    :return: input list si output list
    :rtype: tuple[list, list]
    """
    data = []
    dataNames = []
    with open(fileName) as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        line_count = 0
        for row in csv_reader:
            if line_count == 0:
                dataNames = row
            else:
                data.append(row)
            line_count += 1

    selectedVariable = dataNames.index(inputIndex)
    inputs = [data[i][selectedVariable] for i in range(len(data))
              if data[i][selectedVariable] != '']

    selectedOutput = dataNames.index(outputIndex)
    outputs = [data[i][selectedOutput] for i in range(len(data))
               if data[i][selectedVariable] != '']

    return inputs, outputs


def ReadDoubleInputCSV(fileName, inputFirstIndex, inputSecondIndex, ouputIndex):
    """
    Citirea a doua set-uri de date de input si unul pentru Output
    Ignora spatiile libere in tuplurie ( i , i , o)
    :param fileName: denumirea fisierului CSV
    :param inputFirstIndex: index-ul coloanei de input 1
    :param inputSecondIndex: index-ul coloanei de input 2
    :param ouputIndex: index-ul coloanei de output
    :type fileName: str
    :type inputFirstIndex: str
    :type inputSecondIndex: str
    :type ouputIndex: str
    :return: inputurile listele si output list
    :rtype: tuple[list, list, list]
    """
    data = []
    dataNames = []
    with open(fileName) as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        line_count = 0
        for row in csv_reader:
            if line_count == 0:
                dataNames = row
            else:
                data.append(row)
            line_count += 1

    selectedVariableFirst = dataNames.index(inputFirstIndex)
    selectedVariableSecond = dataNames.index(inputSecondIndex)

    inputsFirst = [data[i][selectedVariableFirst] for i in range(len(data))
                   if data[i][selectedVariableFirst] != ''
                   and data[i][selectedVariableSecond] != '']

    inputsSecond = [data[i][selectedVariableSecond] for i in range(len(data))
                    if data[i][selectedVariableFirst] != ''
                    and data[i][selectedVariableSecond] != '']

    selectedOutput = dataNames.index(ouputIndex)
    outputs = [data[i][selectedOutput] for i in range(len(data))
               if data[i][selectedVariableFirst] != ''
               and data[i][selectedVariableSecond] != '']

    return inputsFirst, inputsSecond, outputs
