namespace StoreAgent.WinApp.Domain;

using StoreAgent.WinApp.Infra;
using System;
using System.Collections.Generic;
using System.Text;


public class PlayerStats
{
    private readonly MemoryManager _memoryManager;
    private readonly Dictionary<string, int> _offsets;
    private readonly IntPtr _baseAddress;

    public PlayerStats(MemoryManager memoryManager, Dictionary<string, int> offsets)
    {
        _memoryManager = memoryManager ?? throw new ArgumentNullException(nameof(memoryManager));
        _offsets = offsets ?? throw new ArgumentNullException(nameof(offsets));

        var process = memoryManager.GetProcess();
        if (process?.MainModule == null)
        {
            throw new InvalidOperationException("Processo ou módulo principal não encontrado.");
        }
        _baseAddress = process.MainModule.BaseAddress;
    }

    private int GetValue(string key)
    {
        if (!_offsets.ContainsKey(key)) return 0;
        return _memoryManager.ReadMemory<int>(_baseAddress + _offsets[key]);
    }

    private string GetString(string key, int maxLength = 24)
    {
        if (!_offsets.ContainsKey(key)) return string.Empty;

        byte[] buffer = new byte[maxLength];
        _memoryManager.ReadProcessMemory(_baseAddress + _offsets[key], buffer, buffer.Length, out int _);
        return Encoding.ASCII.GetString(buffer).TrimEnd('\0');
    }

    public string GetCharacterName() => GetString("CharacterName");
    public int GetCurrentHP() => GetValue("CurrentHP");
    public int GetMaxHP() => GetValue("MaxHP");
    public int GetCurrentSP() => GetValue("CurrentSP");
    public int GetMaxSP() => GetValue("MaxSP");
    public int GetBaseLevel() => GetValue("BaseLevel");
    public int GetJobLevel() => GetValue("JobLevel");
    public int GetStrength() => GetValue("Strength");
    public int GetVitality() => GetValue("Vitality");
    public int GetLuck() => GetValue("Luck");
    public int GetAttack() => GetValue("Attack");
    public int GetCritical() => GetValue("Critical");
    public int GetCurrentWeight() => GetValue("CurrentWeight");
    public int GetMaxWeight() => GetValue("MaxWeight");
    public int GetZeny() => GetValue("Zeny");

    public void ShowAllStats()
    {
        Console.WriteLine("\n=== Estatísticas do Personagem ===");

        // Nome do personagem
        string name = GetCharacterName();
        Console.WriteLine($"Nome: {name}");

        // Níveis
        Console.WriteLine($"\nNível Base: {GetBaseLevel()}");
        Console.WriteLine($"Nível de Classe: {GetJobLevel()}");

        // HP/SP
        int currentHP = GetCurrentHP();
        int maxHP = GetMaxHP();
        int currentSP = GetCurrentSP();
        int maxSP = GetMaxSP();
        Console.WriteLine($"\nHP: {currentHP}/{maxHP} ({( currentHP * 100.0 / maxHP ):F1}%)");
        Console.WriteLine($"SP: {currentSP}/{maxSP} ({( currentSP * 100.0 / maxSP ):F1}%)");

        // Atributos Base
        Console.WriteLine("\nAtributos Base:");
        Console.WriteLine($"FOR: {GetStrength()}");
        Console.WriteLine($"VIT: {GetVitality()}");
        Console.WriteLine($"SOR: {GetLuck()}");

        // Status
        Console.WriteLine("\nStatus:");
        Console.WriteLine($"Ataque: {GetAttack()}");
        Console.WriteLine($"Crítico: {GetCritical()}");

        // Peso e Dinheiro
        int currentWeight = GetCurrentWeight();
        int maxWeight = GetMaxWeight();
        Console.WriteLine($"\nPeso: {currentWeight}/{maxWeight} ({( currentWeight * 100.0 / maxWeight ):F1}%)");
        Console.WriteLine($"Zeny: {GetZeny():N0}");

        Console.WriteLine("=================================");
    }
}
