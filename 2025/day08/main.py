import sys
import math
from aoclib import load_input, compute_distance_map_3d

def part1(data: list[str]) -> int:
    box_count = int(data[0])
    junction_boxes = [list(map(int, l.strip().split(','))) for l in data[1:]]
    distance_map = compute_distance_map_3d(junction_boxes)
    sorted_circuts = sorted(distance_map.items(), key=lambda f: f[1])[:box_count]
    sorted_circuts = sorted(sorted_circuts, key=lambda f: f[0][0]*1000+f[0][1])

    circuts = []
    for (a,b), _ in sorted_circuts:
        new = True
        for circut in circuts:
            if a in circut and b not in circut:
                circut.append(b)
                new = False
                break
            elif a not in circut and b in circut:
                circut.append(a)
                new = False
                break
            elif a in circut and b in circut:
                new = False
                break

        if new: circuts.append([a, b])

    merged_circuts = []
    for circut in circuts:
        circut_set = set(circut)
        merged = []
        for mc in merged_circuts:
            if circut_set & mc:
                circut_set |= mc
            else:
                merged.append(mc)
        merged.append(circut_set)
        merged_circuts = merged

    circut_lengths = [len(c) for c in merged_circuts]
    reverse_sorted = sorted(circut_lengths, key=lambda l: l, reverse=True)
    result = math.prod(reverse_sorted[:3])

    return result

def part2(data: list[str]) -> int:
    box_count = int(data[0])
    junction_boxes = [list(map(int, l.strip().split(','))) for l in data[1:]]
    distance_map = compute_distance_map_3d(junction_boxes)
    sorted_circuts = sorted(distance_map.items(), key=lambda f: f[1])

    circuts: list[set[int]] = [] 

    def get_circut(id: int) -> set[int]:
        for c in circuts:
            if id in c:
                return c
        return set()

    last_pair = {}
    for (a,b), _ in sorted_circuts:
        aa = get_circut(a)
        bb = get_circut(b)

        aa_len = len(aa)
        bb_len = len(bb)

        if aa == bb and aa_len > 0:
            continue
        elif aa_len == 0 and bb_len == 0:
            circuts.append(set([a,b]))
        elif aa != bb and aa_len > 0 and bb_len > 0:
            last_pair = (a,b)
            aa.update(bb)
            circuts.remove(bb)
        elif aa_len == 0 and bb_len > 0:
            last_pair = (a,b)
            bb.add(a)
        elif aa_len > 0 and bb_len == 0:
            last_pair = (a,b)
            aa.add(b)

    return junction_boxes[last_pair[0]][0] * junction_boxes[last_pair[1]][0]

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=8)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
