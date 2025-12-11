import sys
from aoclib import load_input
from functools import cache

initial_data: dict[str, set[str]] = {}

@cache
def find_out(key: str) -> int:
    cnt = 0
    if key in initial_data:
        values = initial_data[key]
        for val in values:
            if val == "out": return 1
            cnt += find_out(val)

    return cnt

@cache
def find_out_dac_svr(key: str, dac: bool, fft: bool) -> int:
    cnt = 0
    if key in initial_data:
        values = initial_data[key]
        for val in values:
            if val == "out": return 1 if dac and fft else 0
            dac2 = True if val == "dac" else dac
            fft2 = True if val == "fft" else fft
            cnt += find_out_dac_svr(val, dac2, fft2)

    return cnt

def part1(data: list[str]) -> int:
    for line in data:
        parent = line[:3]
        children = set(line[5:].split())
        initial_data[parent] = children

    count = find_out("you")

    return count

def part2(data: list[str]) -> int:
    if len(initial_data) == 0:
        for line in data:
            parent = line[:3]
            children = set(line[5:].split())
            initial_data[parent] = children

    count = find_out_dac_svr("svr", False, False)

    return count

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=11)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
