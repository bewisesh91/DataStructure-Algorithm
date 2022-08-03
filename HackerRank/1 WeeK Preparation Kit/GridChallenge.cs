public class Program
{
    private static void Main(string[] args)
    {
    }

    private static string gridChallenge(List<string> grid)
    {
        var temp = new List<string>();
        foreach (var info in grid)
        {
            var infoList = info.ToList();
            infoList.Sort();
            var newString = String.Join("", infoList);
            temp.Add(newString);
        }

        var result = temp
                    .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                    .GroupBy(i => i.index, i => i.item)
                    .Select(g => g.ToList())
                    .ToList();

        foreach (var info in result)
        {
            var check = info.OrderBy(x => x).ToList();
            var sorted = String.Join("", check);
            var original = String.Join("", info);

            if (original != sorted)
                return "NO";
        }

        return "YES";
    }
}