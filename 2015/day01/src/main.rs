use std::fs;

fn main() {
    let contents = fs::read_to_string("input.txt")
        .expect("Could not find file");
    
    let final_floor: i16 = calc_part1(&contents);
    println!("Part 1 = {}", final_floor);

    let index = calc_part2(&contents);
    match index {
        Some(val) => println!("Part 2 = {}", val + 1),
        None => println!("Part 2 = -1 NOT FOUND!")
    }

    // "match xx" used to handle result position() gracefully.
    // Could be solved by appending .unwrap() after position() but
    // that would cause a panic to occur if -1 is not found 
}

fn calc_part1(input: &String) -> i16 {
    let result: i16 = input.chars()
        .fold(0, |acc, x| if x == '(' { acc+1 } else { acc-1 }) as i16;
    result
}

fn calc_part2(input: &String) -> Option<usize> {
    let mut floor: i16 = 0;
    let result = input.chars()
        .map(|x| {
            if x == '(' { floor += 1; } else { floor -= 1; };
            floor
        })
        .position(|x| x == -1);
    result
}