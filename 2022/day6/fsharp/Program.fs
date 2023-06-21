let input = System.IO.File.ReadAllText @"..\input.txt"

// let input = "bvwbjplbgvbhsrlpgdmjqwftvncz" // 5
// let input = "nppdvjthqldpwncqszvftbrmjlhg" // 6
// let input = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg" // 10
// let input = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw" // 11

let findMarker x =
    let distinctChars s =
        s
        |> Seq.distinct
        |> Seq.length

    let findMatch = 
        input
        |> Seq.windowed x
        |> Seq.map distinctChars
        |> Seq.findIndex (fun elm -> elm = x)

    (findMatch) + x

printfn "%d" (findMarker 4)
printfn "%d" (findMarker 14)
