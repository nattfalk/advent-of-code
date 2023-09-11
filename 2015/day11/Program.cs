const string chars = "abcdefghijklmnopqrstuvwxyz";

var substringLookup = new HashSet<string>();
for (var i = 0; i < chars.Length - 2; i++)
{
    substringLookup.Add(chars.Substring(i, 3));
}

var password = "hxbxwxba";
do
{
    password = NewPassword(password);
} while (!IsValid(password));
Console.WriteLine($"Part 1 = {password}");

do
{
    password = NewPassword(password);
} while (!IsValid(password));
Console.WriteLine($"Part 2 = {password}");

string NewPassword(string password)
{

    var increaseNext = true;
    for (var i = 0; i < password.Length; i++)
    {
        if (!increaseNext)
            break;

        increaseNext = false;
        var c = password[^(i+1)];
        var index = chars.IndexOf(c);
        index++;
        if (index < chars.Length && "ilo".Contains(chars[index]))
            index++;
        if (index == chars.Length)
        {
            index = 0;
            increaseNext = true;
        }

        password = ReplaceInString(password, password.Length - i - 1, chars[index]);
    }
    return password;
}


string ReplaceInString(string input, int position, char newChar)
{
    var newStr = "";
    if (position < input.Length)
        newStr = input[..position];
    newStr += newChar;
    if (position >= 0)
        newStr += input[(position + 1)..];
    return newStr;
}

bool IsValid(string input)
{
    var pairs = 0;
    var straightIncrease = false;
    var i = 0;
    do
    {
        if (input[i] == input[i + 1])
        {
            pairs++;
            i++;
        }
    } while (++i < input.Length - 1);

    foreach (var sequence in substringLookup)
    {
        if (!input.Contains(sequence)) continue;
        straightIncrease = true;
        break;
    }

    return pairs == 2 && straightIncrease;
}