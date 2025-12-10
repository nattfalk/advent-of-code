from day09.main import part1, part2

data = ["7,1",
        "11,1",
        "11,7",
        "9,7",
        "9,5",
        "2,5",
        "2,3",
        "7,3"]

def test_part1_returns_correct_result():
    assert part1(data) == 50

def test_part2_returns_correct_result():
    assert part2(data) == 24
