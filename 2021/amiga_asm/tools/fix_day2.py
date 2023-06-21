import sys
import os

def main(argv):
    print("")

    infile = "..\\days\\day2.txt"
    outfile = os.path.splitext(infile)[0] + '.dat'

    output = "day2_input:" + chr(10)
    with open(infile, 'r', encoding='utf-8-sig') as f:
        for line in f:
            line = line.strip()
            cmd = line.split()[0][0]
            unit = line.split()[1][0]
            output += "    dc.b\t'" + cmd + "', " + unit + "\n"

    output += "day2_input_end:" + chr(10)

    # Write fixed file
    fout = open(outfile, "wt")
    fout.write(output)
    fout.close()

    print("Done!")

if __name__ == "__main__":
    main(sys.argv)
