from day01.main import part1, part2

data = ["L68","L30","R48","L5","R60","L55","L1","L99","R14","L82"]

def test_part1_returns_correct_result():
    assert part1(data) == 3

def test_part2_returns_correct_result():
    assert part2(data) == 6
