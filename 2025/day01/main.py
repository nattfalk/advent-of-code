import sys
from aoclib import load_input
from aoclib.io import map_lines

def part1(data: list[str]) -> int:
	zero_count = 0
	position = 50

	def calc_steps(instruction: str):
		nonlocal position, zero_count

		direction = instruction[0]
		steps = int(instruction[1:])

		if direction == "L":
			steps = -steps

		position = (position + steps) % 100

		if position == 0:
			zero_count += 1

		return

	map_lines(data, calc_steps)
	return zero_count

def part2(data: list[str]) -> int:
	zero_count = 0
	position = 50

	def calc_steps(instruction: str):
		nonlocal position, zero_count

		direction = instruction[0]
		steps = int(instruction[1:])

		zero_count += steps // 100
		steps %= 100

		if direction == "L":
			steps = -steps

		old_pos = position
		position += steps
		if position >= 100:
			zero_count += 1
		elif old_pos != 0 and position <= 0:
			zero_count += 1

		position %= 100
			
		return

	map_lines(data, calc_steps)
	return zero_count

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
	data = load_part_input(day=1)
	print(f"Part 1: {part1(data)}")
	print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
	main(sys.argv[1:])
