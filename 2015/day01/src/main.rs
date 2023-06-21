use std::fs;

fn main() {
    let contents = fs::read_to_string("input.txt")
        .expect("Could not find file");
    
    let last_floor = contents.chars()
        .fold(0, |acc, x| if x == '(' { acc+1 } else { acc-1 });
    println!("Part 1 = {}", last_floor);

    let mut floor = 0;
    let idx = contents.chars()
        .map(|x| {
            if x == '(' { floor += 1; } else { floor -= 1; };
            floor
        })
        .position(|x| x == -1)
        .unwrap();
    println!("Part 2 = {}", idx + 1);
}