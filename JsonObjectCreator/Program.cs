Dictionary<string, string> replace = new Dictionary<string, string>();
replace.Add("String", "string");
replace.Add("Boolean", "bool");
replace.Add("Int", "int");
replace.Add("Double", "double");
replace.Add("Float", "float");
replace.Add("Text", "text");
replace.Add("ID", "string");

string path = Path.Combine(AppContext.BaseDirectory, "objects.txt");
string cpath = Path.Combine(AppContext.BaseDirectory, "class.txt");

if (!File.Exists(path)) File.CreateText(path);
string objList = File.ReadAllText(path);
string[] objs = objList.Split("\n");


string newClass = "";
foreach (string obj in objs)
{
    string clean = obj.TrimEnd();
    string[] s = clean.Split(": ");

    string type = s[1];
    if (type.EndsWith('!')) type = type.Remove(type.Length - 1);
    if (type.StartsWith('[')) type = type.Substring(1).Replace(']', '[') + "]";
    if (replace.ContainsKey(type)) type = replace[type];

    string up = s[0].Replace(s[0][0], s[0][0].ToString().ToUpper()[0]);

    string l1 = $"[JsonProperty(\"{s[0]}\")]";
    string l2 = $"public {type} {up} {{ get; private set; }}";

    newClass += l1 + "\n" + l2 + "\n\n";
}

if (!File.Exists(cpath)) File.CreateText(cpath);
File.WriteAllText(cpath, newClass);