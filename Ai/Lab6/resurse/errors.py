# Sursa -  AI-UBB
# VO - Validation Outputs
"""
    errors.py

Modulul errors.py contine functii pentru calcularea erorilor
dintre rezultatele obtinute de algoritm si cele reale
"""
import math

import numpy as np
from sklearn.metrics import mean_squared_error, confusion_matrix, classification_report


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


def ToolErrorClassification(VO, computedVO):
    """
    Functia ToolErrorClassification calculeaza eroarea dintre rezultatele obtinute de algoritm(tool) si cele reale
    folosind matricea de confuzie
    :param computedVO: rezultatele obtinute de algoritm
    :param VO: valorile reale
    :return: matricea de confuzie
    :type computedVO: list
    :type VO: list
    :rtype: tuple[list[list[float]], string]
    """
    return confusion_matrix(VO, computedVO), classification_report(VO, computedVO)


def ManualErrorClassification(VO, computedVO):
    """
    Functia ManualErrorClassification calculeaza eroarea dintre rezultatele obtinute de algoritm(code) si cele reale
    folosind matricea de confuzie
    :param computedVO: rezultatele obtinute de algoritm
    :param VO: valorile reale
    :return: matricea de confuzie
    :type computedVO: list
    :type VO: list
    :rtype: list[list[float]
    """
    TP = sum([1 if (computedVO[i] and VO[i]) else 0 for i in range(len(VO))])
    FP = sum([1 if (computedVO[i] and not (VO[i])) else 0 for i in range(len(VO))])
    FN = sum([1 if (not (computedVO[i]) and VO[i]) else 0 for i in range(len(VO))])
    TN = sum([1 if (not (computedVO[i]) and not (VO[i])) else 0 for i in range(len(VO))])

    acc = (TP + TN) / (TP + TN + FP + FN)
    precision = TP / (TP + FP)
    recall = TP / (TP + FN)

    return [[TP, FP], [FN, TN]], f"Accuracy: {acc}\n, Precision: {precision}\n, Recall: {recall}"


def BCE(VO, computedVO, epsilon=1e-15):
    """
    Binary Cross-Entropy (sau log loss) este o funcție de pierdere folosită când ai o problemă de clasificare binară
     – adică două clase posibile (de ex. "spam" sau "nu spam", "pisică" sau "nu pisică").
    :param VO: true values
    :param computedVO: computed values in float forms 0 -1
    :param epsilon:
    :return: eroarea calculata
    :rtype: float
    :type VO: list
    :type computedVO: list
    """
    y_true = np.array(VO)
    y_pred = np.clip(computedVO, epsilon, 1 - epsilon)  # eliminare log 0

    loss = - np.mean(
        y_true * np.log(y_pred) + (1 - y_true) * np.log(1 - y_pred)
    )

    return loss


def CSE(y_true, prob):
    """
    Cross-Entropy Loss (CSE) este o funcție de pierdere utilizată în problemele de clasificare multi-clasă.
    :param y_true: true values
    :param prob: computed values in float forms
    :return: eroarea calculata
    :rtype: float
    :type y_true: list[list[float]]
    :type prob: list[float]
    """
    loss = 0.0
    for true, pred in zip(y_true, prob):
        loss += true * math.log(pred)

    return -loss
