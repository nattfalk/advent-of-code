import time
from contextlib import contextmanager
from typing import Iterator

@contextmanager
def time_block(label: str) -> Iterator[None]:
    """Measure and print elapsed time for a code block.

    Prints a line like: `[timing] my-task: 1.23 ms`

    Example:
    >>> from aoclib.timing import time_block
    >>> with time_block("sum loop"):
    ...     s = sum(range(1000))
    """
    start = time.perf_counter()
    try:
        yield
    finally:
        end = time.perf_counter()
        print(f"[timing] {label}: {(end - start)*1000:.2f} ms")
