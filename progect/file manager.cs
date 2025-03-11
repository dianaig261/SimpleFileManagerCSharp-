using System;
using System.IO;

class FileManager
{
    private string currentDirectory; // Текущая директория

    public FileManager()
    {
        currentDirectory = Directory.GetCurrentDirectory(); // Устанавливаем текущую директорию при создании экземпляра класса
    }

    // Функция просмотра файловой структуры
    public void ListFiles()
    {
        Console.WriteLine("Содержимое директории: " + currentDirectory);

        foreach (var file in Directory.GetFiles(currentDirectory))
        {
            FileInfo info = new FileInfo(file);
            Console.WriteLine($"Файл: {info.Name}, Размер: {info.Length}, Дата создания: {info.CreationTime}");
        }

        foreach (var dir in Directory.GetDirectories(currentDirectory))
        {
            DirectoryInfo info = new DirectoryInfo(dir);
            Console.WriteLine($"Каталог: {info.Name}, Дата создания: {info.CreationTime}");
        }
    }

    // Функция перехода между каталогами
    public void ChangeDirectory(string newDirectory)
    {
        try
        {
            string path = Path.Combine(currentDirectory, newDirectory);

            if (Directory.Exists(path))
            {
                currentDirectory = path;
                Console.WriteLine("Текущая директория изменена на: " + currentDirectory);
            }
            else
            {
                Console.WriteLine("Директория не найдена: " + newDirectory);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
    }

    // Функция копирования файлов и каталогов
    public void CopyFileOrDirectory(string source, string destination)
    {
        try
        {
            if (File.Exists(source))
            {
                File.Copy(source, destination, true);
            }
            else if (Directory.Exists(source))
            {
                CopyDirectory(source, destination);
            }
            else
            {
                Console.WriteLine("Источник не найден.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
    }

    private void CopyDirectory(string source, string destination)
    {
        Directory.CreateDirectory(destination);

        foreach (string file in Directory.GetFiles(source))
        {
            string targetFile = Path.Combine(destination, Path.GetFileName(file));
            File.Copy(file, targetFile, true);
        }

        foreach (string subDir in Directory.GetDirectories(source))
        {
            string targetSubDir = Path.Combine(destination, Path.GetFileName(subDir));
            CopyDirectory(subDir, targetSubDir);
        }
    }

    // Функция удаления файлов и каталогов
    public void DeleteFileOrDirectory(string target)
    {
        try
        {
            if (File.Exists(target))
            {
                File.Delete(target);
            }
            else if (Directory.Exists(target))
            {
                Directory.Delete(target, true);
            }
            else
            {
                Console.WriteLine("Объект не найден.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
    }

    // Функция сохранения текущей директории при выходе
    public void SaveCurrentDirectory()
    {
        File.WriteAllText("current_directory.txt", currentDirectory);
    }
}

class Program
{
    static void Main(string[] args)
    {
        FileManager manager = new FileManager();
        manager.ListFiles();
        manager.ChangeDirectory("folder1");
        manager.ListFiles();
        manager.SaveCurrentDirectory(); // Вызываем функцию сохранения текущей директории
    }
}
