using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Godot;

public partial class WinTracker : Node
{
    private readonly string savePath = "C:/Users/pjkoy/Documents/Death-Bounce/stats.json";

    Dictionary<string, int> winHistory = new();

    public override void _Ready()
    {
        Load();     
    }

    public void AddWin(string weapon, int win_counter)
    {
        if (winHistory.ContainsKey(weapon))
        {
            winHistory[weapon] += win_counter;
        }
        else
        {
            winHistory[weapon] = win_counter;
        }
        Save();
    }

    public int GetWin(string weapon)
    {
        if (winHistory.ContainsKey(weapon))
        {
            return winHistory[weapon];
        }
        else
        {
            winHistory[weapon] = 0;
            return winHistory[weapon];
        }
    }

    private Dictionary<string, int> sort_dict()
    {
        return winHistory.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    private void Save()
    {
        Dictionary<string, int> sorted_dict = sort_dict();
        Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
        string json = JsonSerializer.Serialize(sorted_dict, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(savePath, json);
    }

    private void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            
            if (!string.IsNullOrWhiteSpace(json))
            {
                winHistory = JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new();
            }
        }
    }
}


