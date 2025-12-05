from day02.main import part1, part2

data = ["11-22","95-115","998-1012","1188511880-1188511890","222220-222224",
        "1698522-1698528","446443-446449","38593856-38593862","565653-565659",
        "824824821-824824827","2121212118-2121212124"]

def test_part1_returns_correct_result():
    assert part1(data) == 1227775554

def test_part2_returns_correct_result():
    assert part2(data) == 4174379265

