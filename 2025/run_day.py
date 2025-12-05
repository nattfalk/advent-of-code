import argparse
import importlib
import sys
from aoclib import load_input, time_block


def dispatch(day: int) -> None:
    module_name = f"day{day:02d}.main"
    try:
        mod = importlib.import_module(module_name)
    except ModuleNotFoundError as e:
        print(f"Day {day:02d} module not found: {e}", file=sys.stderr)
        return
    if not hasattr(mod, "part1") or not hasattr(mod, "part2"):
        print(f"Module {module_name} missing part1/part2 functions", file=sys.stderr)
        return
    data = mod.load_part_input(day)
    with time_block(f"day{day:02d} part1"):
        a1 = mod.part1(data)
    with time_block(f"day{day:02d} part2"):
        a2 = mod.part2(data)
    print(f"Day {day:02d} Part 1: {a1}")
    print(f"Day {day:02d} Part 2: {a2}")

def main(argv: list[str]) -> None:
    ap = argparse.ArgumentParser(description="Run an Advent of Code 2025 day")
    ap.add_argument("--day", type=int, required=True, help="Day number (e.g. 1 for day01)")
    args = ap.parse_args(argv)
    dispatch(args.day)


if __name__ == "__main__":
    main(sys.argv[1:])
