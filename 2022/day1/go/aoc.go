package main

import (
	"fmt"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	content, err := os.ReadFile("../input1.txt")
	if err != nil {
		fmt.Print(err)
		os.Exit(1)
	}
	lines := strings.Split(string(content), "\r\n")

	var sums []int
	var current_total int = 0

	for _, line := range lines {
		if line == "" {
			sums = append(sums, current_total)
			current_total = 0
		} else {
			i, err := strconv.ParseInt(line, 10, 32)
			if err != nil {
				continue
			}
			current_total += int(i)
		}
	}
	sort.Ints(sums)

	l := len(sums)
	fmt.Println("Day 1-1: ", sums[l-1])
	fmt.Println("Day 1-2: ", sums[l-1]+sums[l-2]+sums[l-3])
}
