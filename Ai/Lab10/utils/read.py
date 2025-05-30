def readNetEdges(fileName):
    net = {}
    edges = []
    max_node_id = 0

    with open(fileName, "r") as f:
        for line in f:
            line = line.strip()
            if line:
                u, v = map(int, line.split())
                edges.append((u, v))
                max_node_id = max(max_node_id, u, v)

    noNodes = max_node_id
    net['noNodes'] = noNodes

    mat = [[0] * noNodes for _ in range(noNodes)]

    noEdges = len(edges)
    for u, v in edges:
        mat[u - 1][v - 1] = 1
        mat[v - 1][u - 1] = 1

    net["mat"] = mat
    net["noEdges"] = noEdges

    degrees = [sum(row) for row in mat]
    net["degrees"] = degrees

    return net

#profa
def readNet(fileName):
    f = open(fileName, "r")
    net = {}
    n = int(f.readline())
    net['noNodes'] = n
    mat = []
    for i in range(n):
        mat.append([])
        line = f.readline()
        elems = line.split(" ")
        for j in range(n):
            mat[-1].append(int(elems[j]))
    net["mat"] = mat
    degrees = []
    noEdges = 0
    for i in range(n):
        d = 0
        for j in range(n):
            if (mat[i][j] == 1):
                d += 1
            if (j > i):
                noEdges += mat[i][j]
        degrees.append(d)
    net["noEdges"] = noEdges
    net["degrees"] = degrees
    f.close()
    return net
