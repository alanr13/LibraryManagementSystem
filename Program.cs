using static System.Reflection.Metadata.BlobBuilder;

Library library = new Library();

library.AddBooks(new PhysicalBook("One Piece"));
library.AddBooks(new PhysicalBook("Naruto"));
library.AddBooks(new DigitalBook("The Witcher"));
library.AddBooks(new DigitalBook("Game of Thrones"));
library.AddBooks(new PhysicalBook("Berserk"));
library.AddBooks(new PhysicalBook("Programming Book"));
library.AddBooks(new DigitalBook("Dragon Ball"));
library.AddBooks(new DigitalBook("Claymore"));
library.AddBooks(new PhysicalBook("Self-improvement book"));
library.AddBooks(new PhysicalBook("Cooking book"));

Console.WriteLine("What is your name?");
string? name = Console.ReadLine();
Member member = new Member(name);
Console.Clear();

while (true)
{
    Console.WriteLine("\nWhat would you like to do?");
    Console.WriteLine("1. Borrow book");
    Console.WriteLine("2. Return book");
    Console.WriteLine("3. View available books");
    Console.WriteLine("4. View borrowed books");
    Console.WriteLine("5. Read book description");
    Console.WriteLine("6. Exit");
    char input = Console.ReadKey().KeyChar;
    Console.WriteLine("\n");

    switch (input)
    {
        case '1':
            while (true)
            {
                Console.WriteLine("What a book would you like to borrow?");
                string? title = Console.ReadLine();

                if (title != null)
                {
                    if (!library.HasBook(title))
                        Console.WriteLine("There is no such book.");
                    library.BorrowBook(title, member);
                    break;
                }

                if (library.GetBooks().Count == 0)
                {
                    Console.WriteLine("There are no books in library to borrow.");
                    break;
                }
            }
            break;
        case '2':
            while (true)
            {
                Console.WriteLine("What book would you like to return?");
                string? title = Console.ReadLine();

                if (title != null)
                {
                    if (!member.HasBook(title))
                        Console.WriteLine("You can't return a book that you don't have.");
                    member.ReturnBook(title, member, library);
                    break;
                }

                if (member.GetBooks().Count == 0)
                {
                    Console.WriteLine("You haven't borrowed any books, so you can return anything.");
                    break;
                }
            }
            break;
        case '3':
            library.ViewAvailableBooks();
            break;
        case '4':
            member.ViewBorrowedBooks();
            break;
        case '5':
            
            break;
        default:
            Console.WriteLine("I don't know about such a command.");
            break;
    }
}

class Member
{
    private string? name;

    public string? Name
    {
        get => name;
        set
        {
            if (value != null)
                name = value.ToUpper();
            else
            {
                name = null;
                Console.WriteLine("Name is null");
            }
        }
    }
    private List<Book> BorrowedBooks { get; } = new List<Book>();
    public Member(string? name)
    {
        this.name = name;
    }

    public List<Book> GetBooks()
    {
        return BorrowedBooks;
    }

    public void ChangeName(string name)
    { 
        this.name = name;
    }

    public void ViewBorrowedBooks()
    {
        foreach(Book book in BorrowedBooks)
        {
            Console.WriteLine(book.Title);
        }
    }

    public void AddBorrowedBooks(Book book) => BorrowedBooks.Add(book);

    public void RemoveBorrowedBook(Book book) => BorrowedBooks.Remove(book);

    public void ReturnBook(string title, Member member, Library library)
    {
        foreach (Book book in BorrowedBooks)
        {
            if (book.Title == title)
            {
                library.AddBooks(book);
                member.RemoveBorrowedBook(book);
                break;
            }
        }
    }

    public bool HasBook(string title)
    {
        foreach (Book book in BorrowedBooks.ToList())
        {
            if (book.Title == title)
            {
                return true;
            }
        }

        return false;
    }
}

class Library
{
    private List<Book> Books { get; set; } = new List<Book>();

    public void ViewAvailableBooks()
    {
        foreach (Book book in Books.ToList())
        {
            Console.WriteLine($"{book.Title}");
        }
    }

    public List<Book> GetBooks()
    { 
        return Books; 
    }

    public void BorrowBook(string title, Member member)
    {
        foreach (Book book in Books.ToList())
        {
            if (book.Title == title)
            {
                member.AddBorrowedBooks(book);
                Books.Remove(book);
                break;
            }
        }
    }

    public void AddBooks(Book book) => Books.Add(book);

    public bool HasBook(string title)
    {
        foreach (Book book in Books.ToList())
        {
            if(book.Title == title)
            {
                return true;
            }
        }

        return false;
    }
}

abstract class Book
{
    private string? title;

    public string? Title
    {
        get => title;
        set
        {
            if (title != null)
            {
                if (title.Length < 300)
                {
                    title = value;
                }
                else
                {
                    title = "Error";
                }
            }
        }
    }

    public Book(string title)
    {
        this.title = title;
    }

    public abstract void ReadDescription(Library library);
}

class DigitalBook : Book
{
    public DigitalBook(string title) : base(title)
    {

    }

    public override void ReadDescription(Library library)
    {
        Console.WriteLine("Code: 3253532 blah, blah, blah");
    }
}

class PhysicalBook : Book
{
    public PhysicalBook(string title) : base(title)
    {

    }

    public override void ReadDescription(Library library)
    {
        Console.WriteLine("blah, blah, blah");
    }
}