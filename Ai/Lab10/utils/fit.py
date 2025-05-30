#profa
def modularity(communities, param):
    noNodes = param['noNodes']
    mat = param['mat']
    degrees = param['degrees']
    noEdges = param['noEdges']
    M = 2 * noEdges
    Q = 0.0
    for i in range(0, noNodes):
        for j in range(0, noNodes):
            if communities[i] == communities[j]:
                Q += (mat[i][j] - degrees[i] * degrees[j] / M)
    return Q * 1 / M


def conductance(communities, param):
    noNodes = param['noNodes']
    mat = param['mat']
    degrees = param['degrees']

    from collections import defaultdict
    community_groups = defaultdict(list)
    for i, comm_id in enumerate(communities):
        community_groups[comm_id].append(i)

    total_conductance = 0.0
    num_communities = len(community_groups)

    if num_communities == 0:
        return 0.0

    for nodes_in_community in community_groups.values():
        if not nodes_in_community:
            continue

        volume_S = sum(degrees[i] for i in nodes_in_community)
        cut_size = 0

        community_set = set(nodes_in_community)
        for i in nodes_in_community:
            for j in range(noNodes):
                if mat[i][j] == 1 and j not in community_set:
                    cut_size += 1

        actual_cut_size = cut_size / 2
        community_conductance = actual_cut_size / volume_S if volume_S > 0 else 0.0
        total_conductance += community_conductance

    return 1.0 - (total_conductance / num_communities)


def density(communities, param):
    mat = param['mat']

    from collections import defaultdict
    community_groups = defaultdict(list)
    for i, comm_id in enumerate(communities):
        community_groups[comm_id].append(i)

    total_density = 0.0
    num_communities = len(community_groups)

    if num_communities == 0:
        return 0.0

    for nodes_in_community in community_groups.values():
        size_S = len(nodes_in_community)

        if size_S <= 1:
            community_density = 0.0
        else:
            internal_edges = 0
            for i_idx in range(size_S):
                i = nodes_in_community[i_idx]
                for j_idx in range(i_idx + 1, size_S):
                    j = nodes_in_community[j_idx]
                    if mat[i][j] == 1:
                        internal_edges += 1

            max_possible_edges = size_S * (size_S - 1) // 2
            community_density = internal_edges / max_possible_edges if max_possible_edges > 0 else 0.0

        total_density += community_density

    return total_density / num_communities
