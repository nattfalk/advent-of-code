use std::fs;

fn main() {
    let contents = fs::read_to_string("../../input1.txt")
        .expect("Could not find file");

    let lines = contents.lines();
    let mut sum_list: Vec<i32> = Vec::new();
    let mut current: i32 = 0;
    for l in lines {
        if l.is_empty() {
            sum_list.push(current);
            current = 0;
        } else {
            current += l.parse::<i32>().unwrap();
        }
    }
        
    let res_max: Option<&i32> = sum_list.iter().max();
    println!("Day 1-1: {}", res_max.unwrap());

    sum_list.sort();
    sum_list.reverse();
    let res_top3: i32 = sum_list.iter().take(3).sum();
    println!("Day 1-2: {}", res_top3);
}
