using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }
}

class Program
{
    static void AddBook()
    {
        Book book = new Book();

        Console.Write("Enter book ID: ");
        book.Id = int.Parse(Console.ReadLine()!);

        Console.Write("Enter title: ");
        book.Title = Console.ReadLine()!;

        Console.Write("Enter author: ");
        book.Author = Console.ReadLine()!;

        Console.Write("Enter publication year: ");
        book.Year = int.Parse(Console.ReadLine()!);

        books.Add(book);

        Console.WriteLine("Book added.");
    }

    static void SearchBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine()!;

        bool found = false;

        foreach (Book book in books)
        {
            if (book.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"ID: {book.Id}");
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Year: {book.Year}");

                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Book not found.");
        }
    }

    static void DeleteBook()
    {
        Console.Write("Enter book ID to delete: ");
        int id = int.Parse(Console.ReadLine()!);

        bool found = false;

        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].Id == id)
            {
                books.RemoveAt(i);
                Console.WriteLine("Book deleted.");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Book not found.");
        }
    }

    static void SaveBooks()
    {
        string json = JsonSerializer.Serialize(
            books,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(fileName, json);

        Console.WriteLine("Catalog saved.");
    }

    static void ShowBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Catalog is empty.");
            return;
        }

        foreach (Book book in books)
        {
            Console.WriteLine(
                $"{book.Id}. {book.Title} | {book.Author} | {book.Year}"
            );
        }
    }

    static void LoadBooks()
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);

            List<Book>? loadedBooks = JsonSerializer.Deserialize<List<Book>>(json);

            if (loadedBooks != null)
            {
                books = loadedBooks;
            }
        }
    }

    static string fileName = "books.json";
    static List<Book> books = new List<Book>();

    static void Main()
    {
        LoadBooks();

        while (true)
        {
            Console.WriteLine("\n=== Book Catalog ===");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Search book");
            Console.WriteLine("3. Delete book");
            Console.WriteLine("4. Show all books");
            Console.WriteLine("5. Save and exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;

                case "2":
                    SearchBook();
                    break;

                case "3":
                    DeleteBook();
                    break;

                case "4":
                    ShowBooks();
                    break;

                case "5":
                    SaveBooks();
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}