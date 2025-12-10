import sys
from aoclib import load_input, compute_area_map
from shapely import Polygon, box

def part1(data: list[str]) -> int:
    coords = [list(map(int, l.strip().split(','))) for l in data]
    area_map = compute_area_map(coords)
    return max(area_map.values())

def part2(data: list[str]) -> int:
    def area(x1, y1, x2, y2) -> int:
        return (abs(x2-x1)+1)*(abs(y2-y1)+1)

    coords = [list(map(int, l.strip().split(','))) for l in data]
    poly = Polygon(coords)

    max_area = 0
    for i in range(len(coords)-1):
        for j in range(i+1,len(coords)):
            b = box(coords[i][0], coords[i][1], coords[j][0], coords[j][1])
            if poly.contains(b):
                max_area = max(max_area, area(*coords[i], *coords[j]))
        
    return max_area

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=9)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
