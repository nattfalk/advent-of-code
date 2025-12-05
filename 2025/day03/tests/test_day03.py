from day03.main import part1, part2

data = ["987654321111111","811111111111119","234234234234278","818181911112111"]

def test_part1_returns_correct_result():
    assert part1(data) == 357

def test_part2_returns_correct_result():
    assert part2(data) == 3121910778619
