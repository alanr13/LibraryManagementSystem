abstract class Book
{
    public string? Title { get; }
}

class Member
{
    public string? Name { get; private set; }
    private Book[] BorrowedBooks { get; }
    public Member(string? name)
    {
        Name = name;
    }

    public void ChangeName(string name)
    { 
        Name = name;
    }

    public void ViewBorrowedBooks(Book[] borrowedBooks)
    {
        for(int i = 0; i < borrowedBooks.Length; i++)
        {
            Console.WriteLine($"{i + 1} {borrowedBooks[i].Title}");
        }
    }
}

class Library
{
    public Book[] Books { get; private set; }

    public void ViewAvailableBooks(Book[] books)
    {
        for(int i = 0; i < books.Length; i++)
        {
            Console.WriteLine($"{i + 1} {books[i].Title}");
        }
    }
}

class DigitalBook : Book
{

}

class PhysicalBook : Book
{

}