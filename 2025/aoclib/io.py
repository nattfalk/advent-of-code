import pathlib
from typing import Iterable, Callable

def load_input(day: int | None = None, filename: str = "input.txt", sep: str | None = None) -> list[str]:
    base = pathlib.Path(__file__).resolve().parent.parent
    if day is not None:
        folder = base / f"day{day:02d}"
        path = folder / filename
    else:
        path = pathlib.Path(filename)
    if not path.exists():
        return []
    text = path.read_text(encoding="utf-8")
    lines = []
    if sep:
        lines = text.split(sep)
    else:
        lines = text.splitlines()
    
    return [line.rstrip("\n") for line in lines]


def map_lines(lines: Iterable[str], fn: Callable[[str], object]) -> list[object]:
    return [fn(l) for l in lines]
