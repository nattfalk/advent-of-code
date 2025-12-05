import sys
from aoclib import load_input

testval = 1

def part1(data: list[str]) -> int:
    total = 0

    for line in data:
        left, right = line.split('-', 1)

        v1 = int(left)
        v2 = int(right)
        
        for i in range(v1, v2+1):
            strValue = str(i)
            strLen = len(strValue)
            if strLen % 2 != 0:
                continue
            
            midpoint = int(strLen / 2)
            if strValue[:midpoint] == strValue[midpoint:]:
                total += i

    return total

def part2(data: list[str]) -> int:
    total = 0
    prev_val = 0

    for line in data:
        left, right = line.split('-', 1)

        v1 = int(left)
        v2 = int(right)
        
        for i in range(v1, v2+1):
            strValue = str(i)

            strLen = len(strValue)
            for j in range(1, strLen):
                match_str = strValue[:j]
                match_str *= int(strLen / len(match_str))
                if strValue == match_str and i != prev_val:
                    total += i
                    prev_val = i

    return total

def test_part2_optimized(data: list[str]) -> int:
    """Optimized version of part2. Created with Copilot GPT-5

    A number is counted if its decimal representation is a repetition
    of some shorter substring (periodic string). The original implementation
    tried every prefix length and rebuilt the string; this version uses the
    classic periodicity test: for a non-empty string s of length > 1,
    s is periodic iff it appears inside (s + s)[1:-1]. This runs in O(n)
    per number instead of O(n^2) prefix attempts.

    Semantics preserved: single-digit numbers are never counted; numbers
    whose representation is made by repeating a prefix (including a single
    digit, e.g. '111') are counted once.
    """
    total = 0
    for line in data:
        left, right = line.split('-', 1)
        v1 = int(left)
        v2 = int(right)
        for i in range(v1, v2 + 1):
            s = str(i)
            # Skip length 1 (matches original behavior) and test periodicity.
            if len(s) > 1 and s in (s + s)[1:-1]:
                total += i
    return total

def load_part_input(day: int) -> list[str]:
    return load_input(day, sep=',')

def main(argv: list[str]) -> None:
    data = load_part_input(day=2)
    print(f"Part 1: {part1(data)}")
    print(f"Part 2: {part2(data)}")

if __name__ == "__main__":
    main(sys.argv[1:])
