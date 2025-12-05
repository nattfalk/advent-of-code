# Advent of Code 2025

## Structure
```
2025/
  aoclib/        # Shared utilities (I/O, timing, parsing)
  day01/         # Individual day folder (similar for day02, day03, ...)
  run_day.py     # Dispatcher to run any day
```

Each `dayXX/main.py` exposes `part1(data)` and `part2(data)`. Shared helpers live in `aoclib`.

## Setup
```pwsh
python -m venv .venv
.\.venv\Scripts\Activate.ps1
pip install -r requirements.txt
```

## Running a Day
```pwsh
python run_day.py --day 1
```
Or use VS Code debug config: "Python: Run Day (dispatcher)" and change the `--day` arg.

## Adding a New Day
You can use the scaffold task or do it manually. Each `dayXX/` folder should only contain `main.py` and (optionally) `input.txt` plus any day-specific tests.

### Via Task (Recommended)
Run VS Code task: `Scaffold New Day` and enter day number (e.g. 2). It creates:
- `day02/` directory
- `day02/main.py` with stubs
- `day02/input.txt` empty file
- `day02/tests/` folder

Then run: `python run_day.py --day 2` or a debug config with day=2.

### Manual Steps
1. Create folder `day02`.
2. Copy skeleton from `day01/main.py` and adjust `load_input(day=2)`.
3. Add `input.txt` with puzzle input.
4. Run: `python run_day.py --day 2`.

## Shared Library (`aoclib`)
The `aoclib` package centralizes common helpers.

## Testing
Place tests under a day folder (e.g. `day01/tests/`) or create a top-level `tests/` later. Pytest is enabled.

## Debugging
Configs live in root `./.vscode/launch.json`:
1. Python: Run Day (dispatcher) — runs `run_day.py --day <n>` using input prompt.
2. Python: Day Main (direct) — runs `dayNN/main.py` directly.
3. Python: Current File — runs the active editor file.

When launching, VS Code will prompt for the day number; default is `1`.
