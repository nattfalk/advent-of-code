from aoclib import load_input
import pathlib

def test_load_input_reads_lines(tmp_path):
    day_dir = pathlib.Path(__file__).resolve().parent.parent / "day01"
    input_path = day_dir / "input.txt"
    input_path.write_text("a\nb\n", encoding="utf-8")
    try:
        data = load_input(day=1)
        assert data == ["a", "b"]
    finally:
        if input_path.exists():
            input_path.unlink()
