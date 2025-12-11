import sys
import numpy as np
from functools import reduce
from operator import xor
from aoclib import load_input
from itertools import combinations
from scipy.optimize import linprog

def create_combinations(input: list[int]) -> list[list[int]]:
    return [list(c) for r in range(1, len(input)+1) for c in combinations(input, r)]

def parse_data(line: str) -> tuple[int, list[int], list[int]]:
    parts = line.split()

    pattern_str = str.join('', ["0" if c == "." else "1" for c in parts[0][1:-1]])
    pattern_len = len(pattern_str) - 1
    pattern = int(pattern_str, 2)

    buttons = [sum([1<<(pattern_len - i) for i in map(int, b[1:-1].split(','))]) for b in parts[1:-1]]

    return (pattern, buttons, [])

def parse_data2(line: str) -> tuple[list[list[int]], list[int]]:
    parts = line.split()

    buttons = [list(map(int, b[1:-1].split(','))) for b in parts[1:-1]]
    joltages = list(map(int, parts[-1][1:-1].split(',')))

    N = len(joltages)
    button_presses = [[1 if b in button else 0 for b in range(N)] for button in buttons]

    return (button_presses, joltages)

def part1(data: list[str]) -> int:
    total_presses = 0
    for line in data:
        pattern, buttons, _ = parse_data(line)

        button_combinations = create_combinations(buttons)

        min_presses = len(buttons) + 1
        for combo in button_combinations:
            if pattern == reduce(xor, combo):
                min_presses = min(min_presses, len(combo))

        total_presses += min_presses if min_presses <= len(buttons) else 0

    return total_presses

def part2(data: list[str]) -> int:
    total_presses = 0
    for line in data:
        button_presses, joltages = parse_data2(line)
        
        optimizer = [1] * len(button_presses)
        num_presses = linprog(c=optimizer, A_eq=np.array(button_presses).T, b_eq=joltages, integrality=optimizer).fun

        total_presses += num_presses

    return int(total_presses)

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=10)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
