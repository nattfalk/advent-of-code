#!/usr/bin/env python
"""Scaffold a new Advent of Code day folder.

Usage:
  python scaffold_day.py --day 5
Creates directory `day05` with:
  - main.py (part1/part2 stubs)
  - input.txt (empty)
  - tests/ (empty placeholder)
Will not overwrite existing files; prints actions.
"""
from __future__ import annotations
import argparse
import pathlib
import sys

MAIN_TEMPLATE = """import sys
from aoclib import load_input

def part1(data: list[str]) -> int:
    # TODO: implement Part 1
    return 0

def part2(data: list[str]) -> int:
    # TODO: implement Part 2
    return 0

def load_part_input(day: int) -> list[str]:
    return load_input(day)

def main(argv: list[str]) -> None:
    data = load_part_input(day={day})
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
"""

TEST_TEMPLATE = """from day{day}.main import part1, part2

data = []

def test_part1_returns_correct_result():
    assert part1(data) == X

def test_part2_returns_correct_result():
    assert part2(data) == X
"""

def ensure_day(day: int) -> None:
    root = pathlib.Path(__file__).resolve().parent
    folder = root / f"day{day:02d}"
    if folder.exists():
        print(f"[skip] {folder} already exists")
    else:
        folder.mkdir()
        print(f"[create] {folder}/")

    # input.txt
    input_file = folder / "input.txt"
    if input_file.exists():
        print(f"[skip] {input_file.name} exists")
    else:
        input_file.write_text("", encoding="utf-8")
        print(f"[create] {input_file.name}")

    # tests directory
    tests_dir = folder / "tests"
    if not tests_dir.exists():
        tests_dir.mkdir()
        print(f"[create] {tests_dir}/")
    else:
        print(f"[skip] {tests_dir}/ exists")

    # test_dayXX.py
    test_file = tests_dir / f"test_day{day:02d}.py"
    if test_file.exists():
        print(f"[skip] test_dayXX.py exists")
    else:
        # Use simple replacement to avoid interfering with other braces in f-strings
        test_file.write_text(TEST_TEMPLATE.replace("{day}", f"{day:02d}"), encoding="utf-8")
        print(f"[create] test_dayXX.py")

    # main.py
    main_py = folder / "main.py"
    if main_py.exists():
        print(f"[skip] main.py exists")
    else:
        # Use simple replacement to avoid interfering with other braces in f-strings
        main_py.write_text(MAIN_TEMPLATE.replace("{day}", str(day)), encoding="utf-8")
        print(f"[create] main.py")


def parse_args(argv: list[str]) -> argparse.Namespace:
    ap = argparse.ArgumentParser(description="Scaffold a new AoC day")
    ap.add_argument("--day", type=int, required=True, help="Day number (e.g. 5)")
    return ap.parse_args(argv)


def main(argv: list[str]) -> None:
    args = parse_args(argv)
    if not (1 <= args.day <= 25):
        print("Day must be between 1 and 25", file=sys.stderr)
        sys.exit(1)
    ensure_day(args.day)

if __name__ == "__main__":
    main(sys.argv[1:])
