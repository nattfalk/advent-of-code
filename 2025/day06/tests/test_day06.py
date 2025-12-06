from day06.main import part1, part2, parse_data

data = ["123 328  51 64 ",
        " 45 64  387 23 ",
        "  6 98  215 314",
        "*   +   *   +  "]

def test_parse_data_returns_correct_result():
    values, operators = parse_data(data)
    assert values[0] == [123,45,6]
    assert operators[1] == "+"

def test_part1_returns_correct_result():
    assert part1(data) == 4277556

def test_part2_returns_correct_result():
    assert part2(data) == 3263827
