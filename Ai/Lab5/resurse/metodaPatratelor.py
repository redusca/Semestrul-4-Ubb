def metodaPatratelor(inputs, outputs):
    """
    Implementarea metodei celor mai mici patrate pentru regresia liniara.
    :param inputs: valori de input
    :param outputs: valori de output
    :return: coeficientii regresiei w0 si w1
    :type inputs: list
    :type outputs: list
    :rtype: tuple[float, float]
    """
    n = len(outputs)
    sumXY = sum(list([x * y for x, y in zip(inputs, outputs)]))
    sumX = sum(inputs)
    sumY = sum(outputs)
    sumXsqr = sum([x ** 2 for x in inputs])

    w1 = (n * sumXY - sumX * sumY) / (n * sumXsqr - sumX ** 2)
    w0 = (sumY - w1 * sumX) / n
    return w0, w1