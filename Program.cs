using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

class Program
{
    private static List<Task> tasks = new List<Task>();
    private static string dataFilePath = "tasks.txt";

    static void Main(string[] args)
    {
        LoadTasks();

        while (true)
        {
            Console.WriteLine("Úkolový seznam aplikace");
            Console.WriteLine("1. Přidat úkol");
            Console.WriteLine("2. Smazat úkol");
            Console.WriteLine("3. Zobrazit úkoly");
            Console.WriteLine("4. Uložit a ukončit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    DeleteTask();
                    break;
                case "3":
                    DisplayTasks();
                    break;
                case "4":
                    SaveTasks();
                    return;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.WriteLine("Název úkolu:");
        string title = Console.ReadLine();

        Console.WriteLine("Popis úkolu:");
        string description = Console.ReadLine();

        Task newTask = new Task
        {
            Title = title,
            Description = description,
            IsCompleted = false
        };

        tasks.Add(newTask);
        Console.WriteLine("Úkol byl přidán.");
    }

    static void DeleteTask()
    {
        DisplayTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("Seznam úkolů je prázdný.");
            return;
        }

        Console.WriteLine("Zadejte číslo úkolu k odstranění:");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= tasks.Count)
        {
            Task removedTask = tasks[index - 1];
            tasks.Remove(removedTask);
            Console.WriteLine("Úkol byl odstraněn.");
        }
        else
        {
            Console.WriteLine("Neplatný vstup.");
        }
    }

    static void DisplayTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Seznam úkolů je prázdný.");
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            string status = tasks[i].IsCompleted ? "[x]" : "[ ]";
            Console.WriteLine($"{i + 1}. {status} {tasks[i].Title}: {tasks[i].Description}");
        }
    }

    static void LoadTasks()
    {
        if (File.Exists(dataFilePath))
        {
            string[] lines = File.ReadAllLines(dataFilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    Task task = new Task
                    {
                        Title = parts[0],
                        Description = parts[1],
                        IsCompleted = bool.Parse(parts[2])
                    };
                    tasks.Add(task);
                }
            }
        }
    }

    static void SaveTasks()
    {
        using (StreamWriter writer = new StreamWriter(dataFilePath))
        {
            foreach (Task task in tasks)
            {
                writer.WriteLine($"{task.Title}|{task.Description}|{task.IsCompleted}");
            }
        }
        Console.WriteLine("Seznam úkolů byl uložen.");
    }
}
