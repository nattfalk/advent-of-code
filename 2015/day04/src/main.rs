use md5;

fn main() {
    // let key: String = "abcdef".to_string();
    let key: String = "ckczppom".to_string();
    let mut i: u32 = 0;

    let mut p1_set: bool = false;
    let mut p2_set: bool = false;

    loop {
        i += 1;
        let v = format!("{}{}", key, i);
        let md5_digest = md5::compute(v);

        if !p1_set && md5_digest[0] == 0 && md5_digest[1] == 0 && md5_digest[2] < 0x10 {
            println!("Part 1 :: Value is {}, Digest = {:x}", i, md5_digest);
            p1_set = true;
        }

        if !p2_set && md5_digest[0] == 0 && md5_digest[1] == 0 && md5_digest[2] == 0 {
            println!("Part 2 :: Value is {}, Digest = {:x}", i, md5_digest);
            p2_set = true;
        }

        if (p1_set && p2_set) || i > 10_000_000 { 
            break; 
        }
    }
}
