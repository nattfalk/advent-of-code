use std::fs;
use std::collections::HashMap;

#[derive(Debug, Clone)]
struct TokenEntry {
    tokens: Vec<String>,
    value: u16,
    calculated: bool
}

fn main() {
    let lines: Vec<String> = fs::read_to_string("input_test.txt")
        .expect("Could not find file")
        .split("\r\n")
        .map(|x| x.to_string())
        .collect();

    let mut map: HashMap<String, TokenEntry> = HashMap::new();

    for l in lines {
        let entry = TokenEntry {
            tokens: l.split_whitespace().map(|x| x.to_string()).collect(),
            value: 0,
            calculated: false
        };
        map.insert(entry.tokens[entry.tokens.len()-1].clone(), entry);
    }

    let mut all_calculated = false;
    let mut iter_cnt = 0;
    while !all_calculated {

        let t = map.get("x").unwrap();
        let u = &t.tokens[0];
        println!("{}", u);

        iter_cnt += 1;
        if iter_cnt == 10 {
            break;
        }
    };

    println!("map {} {:#?}", iter_cnt, map);
}
