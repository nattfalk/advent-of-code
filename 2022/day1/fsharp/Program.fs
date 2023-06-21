open System

let input = System.IO.File.ReadAllText @"..\input1.txt"
// let input = @"1000
// 2000
// 3000

// 4000

// 5000
// 6000

// 7000
// 8000
// 9000

// 10000"

let calc (inp:string) =
    let sumGroup (grp:string) =
        grp.Split Environment.NewLine
        |> Seq.map int
        |> Seq.sum
    
    let groupTotals =
        inp.Split $"{Environment.NewLine}{Environment.NewLine}"
        |> Seq.map sumGroup

    printfn "Part 1 : %i" (groupTotals |> Seq.max)

    let part2 =
        groupTotals
        |> Seq.sortDescending
        |> Seq.take 3
        |> Seq.sum
    printfn "Part 2 : %i" part2

calc input