import numpy as np

def normalizareZ(x):
    """
    Normalizarea statistica
    :param x: lista de date
    :return: lista normalizata
    :type x: list
    :rtype: list
    """
    x = np.array(x)
    return ((x - x.mean()) / x.std()).tolist()
