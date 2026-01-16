import sys
from aoclib import load_input

def part1(data: list[str]) -> int:
    valid_regions = 0
    for line in data:
        if not 'x' in line:
            continue

        sx, sy = map(int,line.split(':')[0].split('x'))        
        pc = sum(map(int, line.split(':')[1].split()))

        if sx*sy > pc*(9-1):
            valid_regions += 1

    return valid_regions

def part2(data: list[str]) -> int:
    return 0

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=12)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
