import sys
import os

def main(argv):
    print("")

    infile = "..\\days\\day1.txt"
    outfile = os.path.splitext(infile)[0] + '.dat'

    output = "day1_input:" + chr(10)
    with open(infile, 'r', encoding='utf-8-sig') as f:
        for line in f:
            line = line.strip()
            output += "    dc.w\t" + line + "\n"

    output += "day1_input_end:" + chr(10)

    # Write fixed file
    fout = open(outfile, "wt")
    fout.write(output)
    fout.close()

    print("Done!")

if __name__ == "__main__":
    main(sys.argv)
