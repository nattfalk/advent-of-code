use std::{ fs };

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
    let mut i = 0;
    for l in lines {
        let res = validate_string_part1(&l);
        if res {
            i += 1;
        }
    }
    println!("Part 1 = {}", i);
}

fn part2(lines: &Vec<String>) {
    let mut i = 0;
    for l in lines {
        let res = validate_string_part2(&l);
        if res {
            i += 1;
        }
    }
    println!("Part 2 = {}", i);
}

fn validate_string_part1(s: &String) -> bool {
    let chars = s.chars().collect::<Vec<char>>();
    
    let v = vec!['a','e','i','o','u'];
    let mut i = 0;
    for c in chars.iter() {
        if v.contains(c) { i += 1; };
    }
    if i < 3 {
        return false;
    }

    let mut double_char = false;
    for i in 0..(chars.len()-1) {
        if chars[i] == chars[i+1] {
            double_char = true;
            break;
        }
    }
    if !double_char {
        return false;
    }

    let v2 = vec!["ab", "cd", "pq", "xy"];
    for invalid_text in v2 {
        if s.contains(invalid_text) {
            return false;
        }
    }

    true
}

fn validate_string_part2(s: &String) -> bool {
    let mut found: bool = false;
    for i in 0..s.len()-3 {
        for j in i+2..s.len()-1 {
            if s[i..i+2] == s[j..j+2] {
                found = true;
                break;
            }
        }
        if found { break; }
    }
    if !found { return false; }

    let chars = s.as_bytes();
    for i in 0..s.len()-2 {
        if chars[i] == chars[i+2] { return true; }
    }

    false
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_validate_part1() {
        assert_eq!(true, validate_string_part1(&"ugknbfddgicrmopn".to_string()));
        assert_eq!(true, validate_string_part1(&"aaa".to_string()));
        assert_eq!(false, validate_string_part1(&"jchzalrnumimnmhp".to_string()));
        assert_eq!(false, validate_string_part1(&"haegwjzuvuyypxyu".to_string()));
        assert_eq!(false, validate_string_part1(&"dvszwmarrgswjxmb".to_string()));
    }

    #[test]
    fn test_validate_part2() {
        assert_eq!(true, validate_string_part2(&"qjhvhtzxzqqjkmpb".to_string()));
        assert_eq!(true, validate_string_part2(&"xxyxx".to_string()));
        assert_eq!(true, validate_string_part2(&"yxxxx".to_string()));
        assert_eq!(false, validate_string_part2(&"xxx".to_string()));
        assert_eq!(false, validate_string_part2(&"uurcxstgmygtbstg".to_string()));
        assert_eq!(false, validate_string_part2(&"ieodomkazucvgmuy".to_string()));
    }
}