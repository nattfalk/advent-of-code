import sys
from aoclib import load_input

def part1(data: list[str]) -> int:
    total_joltage = 0

    for line in data:
        first = max([int(c) for c in line[:-1]])
        idx  = line[:-1].index(str(first))
        second = max([int(c) for c in line[idx+1:]])
        total_joltage += int(f"{first}{second}")

    return total_joltage

def part2(data: list[str]) -> int:
    total_joltage = 0

    for batteries in data:
        joltage = 0

        idx = 0
        battery_count = len(batteries)
        for i in range(12, 0, -1):
            slice_len = battery_count - i - idx + 1
            slice = batteries[idx:idx+slice_len] if i > 1 else batteries[idx:]
            battery = max(slice)
            idx += slice.index(battery)+1
            joltage = (joltage * 10) + int(battery)
        total_joltage += joltage

    return total_joltage

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=3)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
