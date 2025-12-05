import sys
from aoclib import load_input

def get_movable_rolls(grid: list[list[str]], clear: bool = False) -> int:
    def adjecent_items(grid: list[list[str]], x: int, y: int):
        for (xs, ys) in [(-1,-1),(0,-1),(1,-1),(1,0),(1,1),(0,1),(-1,1),(-1,0)]:
            if (x+xs >= 0 and x+xs < len(grid[y]) and
                y+ys >= 0 and y+ys < len(grid)):
                yield x+xs, y+ys

    movable_rolls = 0
    moved_rolls = []
    for y in range(0, len(grid)):
        for x in range(0, len(grid[y])):
            if (grid[y][x] != "@"):
                continue
            
            roll_count = 0
            for x2,y2 in adjecent_items(grid, x, y):
                if grid[y2][x2] == "@":
                    roll_count += 1

            if roll_count < 4:
                movable_rolls += 1
                moved_rolls.append((x, y))

    if clear:
        for x, y in moved_rolls:
            grid[y][x] = "."

    return movable_rolls

def part1(data: list[str]) -> int:
    grid = [list(line) for line in data]
    return get_movable_rolls(grid)

def part2(data: list[str]) -> int:
    grid = [list(line) for line in data]

    total_moved = 0
    while True:
        movable_rolls = get_movable_rolls(grid, True)
        if movable_rolls == 0:
            break
        total_moved += movable_rolls

    return total_moved

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day=4)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
