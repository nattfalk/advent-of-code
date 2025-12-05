from day04.main import part1, part2

data = ["..@@.@@@@.","@@@.@.@.@@","@@@@@.@.@@","@.@@@@..@.","@@.@@@@.@@",
        ".@@@@@@@.@",".@.@.@.@@@","@.@@@.@@@@",".@@@@@@@@.","@.@.@@@.@."]

def test_part1_returns_correct_result():
    assert part1(data) == 13

def test_part2_returns_correct_result():
    assert part2(data) == 43