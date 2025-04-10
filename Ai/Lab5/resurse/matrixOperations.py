"""
    matrixOperations.py

    Modul cu functii de lucru cu matrici.
"""

def Biformula(X, Y):
    """
    Biformula este o metoda de regresie liniara multipla  care se bazeaza pe metoda celor mai mici patrate.
    :param X: matricea X
    :param Y: matricea Y
    :return:  matricea cu coeficientii regresiei
    :type  X: list[list[float]]
    :type  Y: list[list[float]]
    :rtype : list[list[float]]
    """
    # algoritm transpusa
    X_t = matrix_transpose(X)

    # algoritm inmultire
    X_tX = matrix_multiply(X_t, X)

    # algoritm inversa
    inv_X_tX = get_matrix_inverse(X_tX)

    # inmultire
    matrice = matrix_multiply(inv_X_tX, X_t)

    # inmultire
    return matrix_multiply(matrice, Y)


def get_matrix_minor(matrix, i, j):
    """
    Functia pentru Minorul matricei.
    Minorul unei matrice este matricea obtinuta prin eliminarea unei linii si a unei coloane.
    :param matrix: matricea
    :param i: linia de eliminat
    :param j: colona de eliminat
    :return: matricea minorant
    :type matrix:  list[list[float]]
    :type  i: int
    :type  j: int
    :rtype : list[list[float]]
    """
    return [row[:j] + row[j + 1:] for row in (matrix[:i] + matrix[i + 1:])]


def get_matrix_determinant(matrix):
    """
    Functia pentru determinatul unei matrice.
    Determinantul unei matrice este un numar care poate fi calculat din elementele matricei.
    :param matrix: matricea
    :return: valoarea determinatului
    :type  matrix: list[list[float]]
    :rtype : float
    """

    # calculam determinantul pentru matricea de dimensiune 2
    if len(matrix) == 2:
        return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0]

    # calculam determinantul pentru matricea de dimensiune n cu minorul matricei
    determinant = 0
    for c in range(len(matrix)):
        determinant += ((-1) ** c) * matrix[0][c] * get_matrix_determinant(get_matrix_minor(matrix, 0, c))
    return determinant


def get_matrix_inverse(matrix):
    """
    Functia pentru inversa unei matrice.
    Inversa unei matrice este matricea care, inmultita cu matricea initiala, da matricea unitate.
    :param matrix: matricea
    :return: matricea inversa
    :type matrix:  list[list[float]]
    :rtype : list[list[float]]
    """

    determinant = get_matrix_determinant(matrix)

    # verificam daca matricea este inversabila
    if determinant == 0:
        raise ValueError("Matrix is not invertible")

    # special case for 2x2 matrix:
    if len(matrix) == 2:
        return [[matrix[1][1] / determinant, -1 * matrix[0][1] / determinant],
                [-1 * matrix[1][0] / determinant, matrix[0][0] / determinant]]

    # calcul cofactorilor (matricea adjuncta)
    cofactors = []
    for r in range(len(matrix)):
        cofactorRow = []
        for c in range(len(matrix)):
            minor = get_matrix_minor(matrix, r, c)
            cofactorRow.append(((-1) ** (r + c)) * get_matrix_determinant(minor))
        cofactors.append(cofactorRow)
    cofactors = list(map(list, zip(*cofactors)))
    for r in range(len(cofactors)):
        for c in range(len(cofactors)):
            cofactors[r][c] = cofactors[r][c] / determinant
    return cofactors


def matrix_multiply(A, B):
    """
    Functia pentru inmultirea a doua matrice.
    :param A: matricea A
    :param B: matricea B
    :return: matricea rezultat
    :type  B :list[list[float]]
    :type  A : list[list[float]]
    :rtype : list[list[float]]
    """
    # numar de linii si coloane pentru matricea A
    rows_A, cols_A = len(A), len(A[0])
    rows_B, cols_B = len(B), len(B[0])

    # Verificam daca matricele pot fi inmultite
    if cols_A != rows_B:
        raise ValueError("Cannot multiply matrices: Number of columns in A must equal number of rows in B")

    # Initializam matricea rezultat cu zerouri
    result = [[0 for _ in range(cols_B)] for _ in range(rows_A)]

    for i in range(rows_A):
        for j in range(cols_B):
            for k in range(cols_A):
                result[i][j] += A[i][k] * B[k][j]

    return result


def matrix_transpose(matrix):
    """
    Functia pentru transpunerea unei matrice.
    Transpunerea unei matrice este matricea obtinuta prin schimbarea liniilor cu coloanele.
    :param matrix: matricea
    :return: transpunerea matricei
    :type matrix: list[list[float]]
    :rtype : list[list[float]]
    """

    # Get the number of rows and columns for the matrix
    rows, cols = len(matrix), len(matrix[0])

    # Initialize the transpose matrix with zeros
    transpose = [[0 for _ in range(rows)] for _ in range(cols)]

    # Perform the transpose
    for i in range(rows):
        for j in range(cols):
            transpose[j][i] = matrix[i][j]

    return transpose
