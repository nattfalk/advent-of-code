use std::{fs, num::ParseIntError};
use array2d::Array2D;

fn main() {
    let lines: Vec<String> = fs::read_to_string("input.txt")
        .expect("Could not find file")
        .split("\r\n")
        .map(|x| x.to_string())
        .collect();

    part1(&lines);
    part2(&lines);
}

fn part1(lines: &Vec<String>) {
    let mut lights: Array2D<u8> = Array2D::filled_with(0, 1000, 1000);

    for l in lines {
        let parts: Vec<&str> = l.split_whitespace().collect();

        match parts[0] {
            "turn" => turn_onoff(&parts, &mut lights),
            "toggle" => toggle(&parts, &mut lights),
            _ => ()
        }
    }

    println!("Part 1 - light count = {}", light_count(&lights));
}

fn part2(lines: &Vec<String>) {
    let mut lights: Array2D<u32> = Array2D::filled_with(0, 1000, 1000);

    for l in lines {
        let parts: Vec<&str> = l.split_whitespace().collect();

        match parts[0] {
            "turn" => turn_onoff_2(&parts, &mut lights),
            "toggle" => toggle_2(&parts, &mut lights),
            _ => ()
        }
    }

    println!("Part 2 - intensity = {}", sum_intensity(&lights));
}

fn sum_intensity(lights: &Array2D<u32>) -> u32 {
    let mut sum: u32 = 0;
    for x in 0..1000 {
        for y in 0..1000 {
            sum += lights[(x,y)]; 
        }
    }
    sum
}

fn light_count(lights: &Array2D<u8>) -> u32 {
    let mut cnt: u32 = 0;
    for x in 0..1000 {
        for y in 0..1000 {
            if lights[(x,y)] == 1 {
                cnt += 1;
            }
        }
    }
    cnt
}

fn turn_onoff_2(parts: &Vec<&str>, lights: &mut Array2D<u32>) {
    let state = parts[1] == "on";

    let (x1, y1) = parse_xy(parts[2]);
    let (x2, y2) = parse_xy(parts[4]);

    for x in x1..x2+1 {
        for y in y1..y2+1 {
            if state {
                lights[(x as usize, y as usize)] += 1;
            } else {
                if lights[(x as usize, y as usize)] >= 1 {
                    lights[(x as usize, y as usize)] -= 1;
                }
            }
        }
    }
}

fn toggle_2(parts: &Vec<&str>, lights: &mut Array2D<u32>) {
    let (x1, y1) = parse_xy(parts[1]);
    let (x2, y2) = parse_xy(parts[3]);

    for x in x1..x2+1 {
        for y in y1..y2+1 {
            lights[(x as usize, y as usize)] += 2;
        }
    }
}

fn turn_onoff(parts: &Vec<&str>, lights: &mut Array2D<u8>) {
    let state: u8 = if parts[1] == "on" { 1 } else { 0 };

    let (x1, y1) = parse_xy(parts[2]);
    let (x2, y2) = parse_xy(parts[4]);

    for x in x1..x2+1 {
        for y in y1..y2+1 {
            lights[(x as usize, y as usize)] = state;
        }
    }
}

fn toggle(parts: &Vec<&str>, lights: &mut Array2D<u8>) {
    let (x1, y1) = parse_xy(parts[1]);
    let (x2, y2) = parse_xy(parts[3]);

    for x in x1..x2+1 {
        for y in y1..y2+1 {
            lights[(x as usize, y as usize)] = 1 - lights[(x as usize, y as usize)];
        }
    }
}

fn parse_xy(s: &str) -> (u16, u16) {
    let parsed_result: Vec<u16> = s
        .split(',')
        .map(|x| x.parse::<u16>())
        .collect::<Result<Vec<u16>, ParseIntError>>()
        .unwrap();

    (parsed_result[0], parsed_result[1])
}