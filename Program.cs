using static System.Reflection.Metadata.BlobBuilder;

Library library = new Library();

Console.WriteLine("What books, would you add to the library?\n");
for (;;)
{
    string? title = Console.ReadLine();
    if (title != null)
        library.AddBooks(title);
    if (title == "x")
        break;
}

Console.WriteLine("What is your name?\n");
string? name = Console.ReadLine();
Member member = new Member(name);
Console.Clear();

while (true)
{
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("1. Borrow book");
    Console.WriteLine("2. Return book");
    Console.WriteLine("3. View available books");
    Console.WriteLine("4. View borrowed books");
    Console.WriteLine("5. Exit");
    char input = Console.ReadKey().KeyChar;

    switch (input)
    {
        case '1':
            Console.WriteLine("What a book would you like to borrow?");
            string title = Console.ReadLine();
            library.BorrowBook(title, member);
            break;
        case '2':
            Console.WriteLine("What book would you like to return?");
            title = Console.ReadLine();
            library.ReturnBook(title, member);
            break;
        case '3':
            library.ViewAvailableBooks();
            break;
        case '4':
            member.ViewBorrowedBooks();
            break;
        case '5':
            Environment.ExitCode = 0;
            break;
        default:
            break;
    }
}
class Book
{
    public string? Title { get; set; }

    public Book(string title)
    {
        Title = title;
    }
}

class Member
{
    public string? Name { get; private set; }
    private List<Book> BorrowedBooks { get; } = new List<Book>();
    public Member(string? name)
    {
        Name = name;
    }

    public void ChangeName(string name)
    { 
        Name = name;
    }

    public void ViewBorrowedBooks()
    {
        foreach(Book book in BorrowedBooks)
        {
            Console.WriteLine(book.Title);
        }
    }

    public void AddBorrowedBooks(string title)
    {
        BorrowedBooks.Add(new Book(title));
    }

    public void RemoveBorrowedBook(string title)
    {
        foreach (Book book in BorrowedBooks)
        {
            if (book.Title == title)
            {
                BorrowedBooks.Remove(book);
            }
        }
    }
}

class Library
{
    public List<Book> Books { get; set; } = new List<Book>();

    public void ViewAvailableBooks()
    {
        foreach (Book book in Books)
        {
            Console.WriteLine($"{book.Title}");
        }
    }

    public void AddBooks(string title)
    {
        Books.Add(new Book(title));
    }

    public void BorrowBook(string title, Member member)
    {
        foreach (Book book in Books)
        {
            if (book.Title == title)
            {
                member.AddBorrowedBooks(title);
                Books.Remove(book);
            }
        }
    }

    public void ReturnBook(string title, Member member)
    {
        foreach (Book book in Books)
        {
            if (book.Title == title)
            {
                AddBooks(title);
                member.RemoveBorrowedBook(title);
            }
        }
    }
}

//class DigitalBook : Book
//{
//    public DigitalBook(string title)
//    {
//        base.Title = title;
//    }
//}

//class PhysicalBook : Book
//{
//    public PhysicalBook(string title)
//    {
//        base.Title = title;
//    }
//}