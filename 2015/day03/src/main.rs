use std::{fs};
use std::collections::HashMap;

struct Point {
    x: i32,
    y: i32
}

fn main() {
    let contents = fs::read_to_string("input.txt")
        .expect("Could not find file");

    println!("Part 1 = {}", calc_part1(&contents));
    println!("Part 1 = {}", calc_part2(&contents));
}

fn calc_part1(input: &String) -> usize {
    let mut houses: HashMap<String, u16> = HashMap::new();

    let mut p: Point = Point { x: 0, y: 0 };
    inc_house(&mut houses, &p);

    for c in input.chars() {
        calc_movement(&c, &mut p);
        inc_house(&mut houses, &p);
    }
    houses.len()
}

fn calc_part2(input: &String) -> usize {
    let mut houses: HashMap<String, u16> = HashMap::new();

    let mut p_santa: Point = Point { x: 0, y: 0 };
    let mut p_robot: Point = Point { x: 0, y: 0 };

    inc_house(&mut houses, &p_santa);
    inc_house(&mut houses, &p_robot);

    let mut state = true;
    for c in input.chars() {
        if state {
            calc_movement(&c, &mut p_santa);
            inc_house(&mut houses, &p_santa);
        } else {
            calc_movement(&c, &mut p_robot);
            inc_house(&mut houses, &p_robot);
        }
        state = !state;
    }
    houses.len()
}

fn inc_house(houses: &mut HashMap<String, u16>, p: &Point) {
    let key = format!("{}|{}", p.x.to_string(), p.y.to_string());
    let house = houses.entry(key).or_insert(0);
    *house += 1;
}

fn calc_movement(c: &char, p: &mut Point) {
    match c {
        '^' => p.y += 1,
        'v' => p.y -= 1,
        '>' => p.x += 1,
        '<' => p.x -= 1,
        _ => ()
    }
}
