use std::{fs};

fn main() {
    let contents = fs::read_to_string("input.txt")
        .expect("Could not find file");
    let parsed_input = parse_input(&contents);

    println!("Part 1 = {}", calc_part1(&parsed_input));
    println!("Part 2 = {}", calc_part2(&parsed_input));
}

fn calc_part1(parsed_input: &Vec<Vec<u32>>) -> u32 {
    let res = parsed_input
        .into_iter()
        .map(|x| calc_area(&x))
        .sum();
    res
}

fn calc_part2(parsed_input: &Vec<Vec<u32>>) -> u32 {
    let res = parsed_input
        .into_iter()
        .map(|x| calc_ribbon(&x))
        .sum();
    res
}

fn parse_input(input: &String) -> Vec<Vec<u32>> {
    let mut res: Vec<Vec<u32>> = Vec::new();
    for line in input.lines() {
        let lwh: Vec<u32> = line.split('x')
            .into_iter()
            .map(|x| x.parse::<u32>().unwrap())
            .collect();
        res.push(lwh);
    }
    res
}

fn calc_area(lwh: &Vec<u32>) -> u32 {
    let mut res = vec![0; 3];

    res[0] = lwh[0] * lwh[1];
    res[1] = lwh[1] * lwh[2];
    res[2] = lwh[2] * lwh[0];

    let area = res.iter().sum::<u32>() * 2;
    let slack = res.iter().min().unwrap();

    area + slack
}

fn calc_ribbon(lwh: &Vec<u32>) -> u32 {
    let mut lwh_sorted = lwh.clone();
    lwh_sorted.sort();

    let ribbon_length = lwh_sorted[0]*2 + lwh_sorted[1]*2;
    let bow_length = lwh_sorted.into_iter().reduce(|a, b| a*b).unwrap();

    ribbon_length + bow_length
}