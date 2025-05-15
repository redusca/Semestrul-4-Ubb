import numpy as np
from resurse.Bow import preprocess, vocabulary


def tf(dataset, voc):
    tf_matrix = []
    for word in voc.keys():
        X = []
        for data in dataset:
            X.append(sum([1 if w == word else 0 for w in data]) / len(data))
        tf_matrix.append(X)

    return tf_matrix


def idf(tf_matrix, voc):
    n = len(tf_matrix[0])
    idf_voc = {}
    for i, word in enumerate(voc.keys()):
        idf_voc[word] = np.log(n / sum([1 if val != 0 else 0 for val in tf_matrix[i]]))

    return idf_voc


def set_tf_idf(dataset):
    dataset = preprocess(dataset)
    pos_voc = vocabulary(dataset)

    set_matrix = []
    tf_matrix = tf(dataset, pos_voc)
    idf_voc = idf(tf_matrix, pos_voc)

    for i, data in enumerate(dataset):
        X = []
        for j, word in enumerate(idf_voc.keys()):
            X.append(tf_matrix[j][i] * idf_voc[word])

        set_matrix.append(X)

    return set_matrix
