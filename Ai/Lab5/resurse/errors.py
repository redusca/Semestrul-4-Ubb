# Sursa -  AI-UBB
# VO - Validation Outputs
"""
    errors.py

Modulul errors.py contine functii pentru calcularea erorilor
dintre rezultatele obtinute de algoritm si cele reale
"""
from sklearn.metrics import mean_squared_error


def ManualError(computedVO, VO):
    """
    Functia ManualError calculeaza eroarea dintre rezultatele obtinute de algoritm(code) si cele reale
    :param computedVO: rezultatele obtinute de algoritm
    :param VO: valorile reale
    :return: eroarea calculata
    :type computedVO: list
    :type VO: list
    :rtype: float
    """
    er = 0.0
    for t1, t2 in zip(computedVO, VO):
        er += (t1 - t2) ** 2
    er = er / len(VO)
    return er


def ToolError(computedVO, VO):
    """
    Functia ToolError calculeaza eroarea dintre rezultatele obtinute de algoritm(tool) si cele reale
    :param computedVO: rezultatele obtinute de algoritm
    :param VO: valorile reale
    :return: eroarea calculata
    :type computedVO: list
    :type VO: list
    :rtype: float
    """
    return mean_squared_error(VO, computedVO)
