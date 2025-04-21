using static System.Reflection.Metadata.BlobBuilder;

Library library = new Library();

Console.WriteLine("What books, would you add to the library?");
for (;;)
{
    string? title = Console.ReadLine();
    if (title != null)
        library.Books.Add(new Book(title));
    if (title == "x")
        break;
}

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
    Console.WriteLine("5. Exit");
    char input = Console.ReadKey().KeyChar;
    Console.WriteLine("\n");

    switch (input)
    {
        case '1':
            Console.WriteLine("What a book would you like to borrow?");
            string? title = Console.ReadLine();
            if (title != null)
                library.BorrowBook(title, member);
            break;
        case '2':
            Console.WriteLine("What book would you like to return?");
            title = Console.ReadLine();
            if(title != null)
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
        foreach (Book book in BorrowedBooks.ToList())
        {
            if (book.Title == title)
            {
                BorrowedBooks.Remove(book);
                break;
            }
        }
    }
}

class Library
{
    public List<Book> Books { get; set; } = new List<Book>();

    public void ViewAvailableBooks()
    {
        foreach (Book book in Books.ToList())
        {
            Console.WriteLine($"{book.Title}");
        }
    }

    public void BorrowBook(string title, Member member)
    {
        foreach (Book book in Books.ToList())
        {
            if (book.Title == title)
            {
                member.AddBorrowedBooks(title);
                Books.Remove(book);
                break;
            }
        }
    }

    public void ReturnBook(string title, Member member)
    {
        foreach (Book book in Books)
        {
            if (book.Title == title)
            {
                Books.Add(book);
                member.RemoveBorrowedBook(title);
                break;
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