import math

def compute_distance_map_3d(data: list[list[int]]) -> dict[tuple[int,int], float]:
    distance_map: dict[tuple[int,int], float] = {} 

    for id1,n1 in enumerate(data):
        for id2, n2 in enumerate(data):
            if id1 == id2: continue

            k = (id1, id2) if id1 < id2 else (id2, id1)

            distance_map[k] = math.sqrt(
                math.pow(n2[0]-n1[0], 2)+math.pow(n2[1]-n1[1],2)+math.pow(n2[2]-n1[2],2))

    return distance_map
