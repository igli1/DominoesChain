namespace CircularDominoes;

public class Program
{
    static void Main()
    {
        int min = 1, max = 6;
        List<(int, int)> dominoes = new List<(int, int)>();
        bool hasMore = true;
        while (hasMore)
        {
            Console.Write("Enter dominoes (two numbers separated by comma): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
            {
                if (num1 < min || num1 > max || num2 < min || num2 > max)
                {
                    Console.WriteLine($"Numbers must be between {min} and {max}. Try again.");
                    continue;
                }
                
                bool exists = dominoes.Exists(pair => (pair.Item1 == num1 && pair.Item2 == num2)
                    || (pair.Item1 == num2 && pair.Item2 == num1));

                if (!exists)
                {
                    dominoes.Add((num1, num2));
                }
                else
                {
                    Console.WriteLine("This domino pair already exists. Please enter a unique pair.");
                }
            }
            
            if (dominoes.Count >= 3)
            {
                Console.Write("Do you want to add more dominoes? (y/n): ");
                string response = Console.ReadLine()?.Trim().ToLower();
                hasMore = response == "y" || response == "yes";
            }
        }
        
        var result = FindCircularDominoChain(dominoes);
        
        if (result != null)
        {
            Console.WriteLine("\nA valid circular domino chain:");
            foreach (var domino in result)
            {
                Console.Write($"[{domino.Item1}|{domino.Item2}] ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("\nNo valid circular domino chain can be formed.");
        }
        
        Console.ReadLine();
    }
    
    static List<(int, int)>? FindCircularDominoChain(List<(int, int)> dominoes)
    {
        var used = new bool[dominoes.Count];
        var chain = new List<(int, int)>();

        for (int i = 0; i < dominoes.Count; i++)
        {
            chain.Add(dominoes[i]);
            used[i] = true;

            if (Backtrack(dominoes, chain, used))
            {
                if (chain[0].Item1 == chain[^1].Item2)
                {
                    return chain;
                }
            }
            
            chain.RemoveAt(chain.Count - 1);
            used[i] = false;
        }

        return null;
    }
    
    static bool Backtrack(List<(int, int)> dominoes, List<(int, int)> chain, bool[] used)
    {
        if (chain.Count == dominoes.Count)
        {
            return true;
        }

        int lastRight = chain[^1].Item2; // Last domino's right number

        for (int i = 0; i < dominoes.Count; i++)
        {
            if (!used[i])
            {
                var domino = dominoes[i];
                if (domino.Item1 == lastRight || domino.Item2 == lastRight)
                {
                    // Match found; add to chain
                    chain.Add(domino.Item1 == lastRight ? domino : (domino.Item2, domino.Item1));
                    used[i] = true;

                    if (Backtrack(dominoes, chain, used))
                    {
                        return true;
                    }

                    chain.RemoveAt(chain.Count - 1);
                    used[i] = false;
                }
            }
        }

        return false;
    }
}