from day05.main import part1, part2, parse_data

data = ["3-5","10-14","16-20","12-18",
        "",
        "1","5","8","11","17","32"]

def test_parse_data():
    data = ["1-2", "3-4", "", "1", "2"]
    a, b = parse_data(data)

    assert a == [(1, 2), (3,4)]
    assert b == [1,2]

def test_part1_returns_correct_result():
    assert part1(data) == 3

def test_part2_returns_correct_result():
    assert part2(data) == 14
