import sys
from aoclib import load_input

def part1(data: list[str]) -> int:
    split_count = 0
    beams: list[bool] = [False] * len(data[0])
    beams[data[0].index("S")] = True

    for line in data[1:]:
        for id, c in enumerate(line):
            if c == "^" and beams[id] == True:
                beams[id-1] = True
                beams[id] = False
                beams[id+1] = True
                split_count += 1
    
    return split_count

def part2(data: list[str]) -> int:
    timelines: list[int] = [0] * len(data[0])
    timelines[data[0].index("S")] = 1

    for line in data[1:]:
        for id, c in enumerate(line):
            if c == "^":
                timelines[id-1] += timelines[id]
                timelines[id+1] += timelines[id]
                timelines[id] = 0
    
    return sum(timelines)

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=7)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
