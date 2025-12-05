import sys
from aoclib import load_input

def parse_data(data: list[str]) -> tuple[list[tuple[int, int]], list[int]]:
    ranges: list[tuple[int, int]] = []
    ingredients: list[int] = []

    i = 0    
    for line in data:
        if line == '':
            break
        i += 1
        a, b = line.split("-")
        ranges.append((int(a), int(b)))
    for line in data[i+1:]:
        ingredients.append(int(line))

    return ranges, ingredients

def part1(data: list[str]) -> int:
    ranges, ingredients = parse_data(data)

    fresh_ingredients = 0
    for ingr in ingredients:
        for rng in ranges:
            if ingr in range(rng[0], rng[1]+1):
                fresh_ingredients += 1
                break

    return fresh_ingredients

def part2(data: list[str]) -> int:
    ranges, _ = parse_data(data)

    sorted_ranges = sorted(ranges, key=lambda r: r[0])

    total_fresh = 0
    current_max = 0
    for rng in sorted_ranges:
        if current_max < rng[0]:
            total_fresh += len(range(rng[0], rng[1]+1))
            current_max = rng[1]
        elif rng[0] <= current_max and rng[1] > current_max:
            total_fresh += len(range(current_max+1, rng[1]+1))
            current_max = rng[1]

    return total_fresh

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=5)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
