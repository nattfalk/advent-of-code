import sys
from aoclib import load_input
from functools import reduce
from operator import mul, add

def parse_data(data: list[str]) -> tuple[list[list[int]], list[str]]:
    grid = [list(map(int, l.split())) for l in data[:-1]]
    transposed = [list(col) for col in zip(*grid)]
    operators = data[-1].split()
    return (transposed, operators)

def part1(data: list[str]) -> int:
    values, operators = parse_data(data)

    total = 0
    for i in range(0, len(values)):
        match operators[i]:
            case "+":  total += reduce(add, values[i])
            case "*":  total += reduce(mul, values[i])

    return total

def part2(data: list[str]) -> int:
    operators = data[-1].split()
    
    cols = [''.join(col).strip() for col in zip(*data[:-1])]

    total = 0
    op_idx = 0
    values = []
    for val in cols:
        if (val == ""):
            total += reduce(add if operators[op_idx] == "+" else mul, values)
            values.clear()
            op_idx += 1
        else:
            values.append(int(val))

    if len(values) > 0:
        total += reduce(add if operators[op_idx] == "+" else mul, values)

    return total

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=6)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
