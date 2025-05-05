using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public string Title { get; }
    public string Author { get; }
    public string ISBN { get; }
    public bool IsAvailable { get; set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
    }

    public override string ToString()
    {
        var status = IsAvailable ? "Available" : "Loaned out";
        return $"{Title} by {Author} (ISBN: {ISBN}) - {status}";
    }
}

class Member
{
    public string Name { get; }
    public string MemberID { get; }
    public List<Loan> Loans { get; }

    public Member(string name, string memberId)
    {
        Name = name;
        MemberID = memberId;
        Loans = new List<Loan>();
    }

    public override string ToString() => $"Member: {Name} (ID: {MemberID})";
}

class Loan
{
    public Book Book { get; }
    public Member Member { get; }
    public DateTime LoanDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; set; }

    public Loan(Book book, Member member)
    {
        Book = book;
        Member = member;
        LoanDate = DateTime.Today;
        DueDate = LoanDate.AddDays(14);
        ReturnDate = null;
    }

    public bool IsOverdue() => !ReturnDate.HasValue && DateTime.Today > DueDate;

    public override string ToString()
    {
        var status = ReturnDate.HasValue ? $"Returned on: {ReturnDate.Value.ToShortDateString()}" : $"Due: {DueDate.ToShortDateString()}";
        return $"{Book.Title} loaned to {Member.Name} ({status})";
    }
}

class Library
{
    private List<Book> books = new();
    private List<Member> members = new();
    private List<Loan> loans = new();

    public void AddBook(Book book) => books.Add(book);
    public void AddMember(Member member) => members.Add(member);

    public void LoanBook(string isbn, string memberId)
    {
        var book = books.FirstOrDefault(b => b.ISBN == isbn && b.IsAvailable);
        var member = members.FirstOrDefault(m => m.MemberID == memberId);

        if (book != null && member != null)
        {
            var loan = new Loan(book, member);
            book.IsAvailable = false;
            member.Loans.Add(loan);
            loans.Add(loan);
            Console.WriteLine($"Loan created: {loan}");
        }
        else
        {
            Console.WriteLine("Loan failed: book not available or member not found");
        }
    }

    public void ReturnBook(string isbn)
    {
        var loan = loans.FirstOrDefault(l => l.Book.ISBN == isbn && !l.ReturnDate.HasValue);
        if (loan != null)
        {
            loan.ReturnDate = DateTime.Today;
            loan.Book.IsAvailable = true;
            Console.WriteLine($"Book returned: {loan.Book.Title}");
        }
        else
        {
            Console.WriteLine("Return failed: active loan not found");
        }
    }

    public void ListBooks()
    {
        Console.WriteLine("\nLibrary books:");
        books.ForEach(Console.WriteLine);
    }

    public void ListOverdueLoans()
    {
        Console.WriteLine("\nOverdue loans:");
        var overdueLoans = loans.Where(l => l.IsOverdue()).ToList();
        if (overdueLoans.Any())
            overdueLoans.ForEach(Console.WriteLine);
        else
            Console.WriteLine("No overdue loans found.");
    }

    public void ListLoans()
    {
        Console.WriteLine("\nAll current and past loans:");
        loans.ForEach(Console.WriteLine);
    }
}

class Program
{
    static void Main()
    {
        var library = new Library();

        var books = new List<Book>
        {
            new("1984", "George Orwell", "12345"),
            new("Dune", "Frank Herbert", "67890"),
            new("To Kill a Mockingbird", "Harper Lee", "10001"),
            new("Pride and Prejudice", "Jane Austen", "10002"),
            new("The Great Gatsby", "F. Scott Fitzgerald", "10003"),
            new("Moby-Dick", "Herman Melville", "10004"),
            new("Brave New World", "Aldous Huxley", "10005"),
            new("Crime and Punishment", "Fyodor Dostoevsky", "10006"),
            new("War and Peace", "Leo Tolstoy", "10007"),
            new("The Catcher in the Rye", "J.D. Salinger", "10008"),
            new("Jane Eyre", "Charlotte Brontë", "10009"),
            new("Wuthering Heights", "Emily Brontë", "10010"),
            new("The Hobbit", "J.R.R. Tolkien", "10011"),
            new("Fahrenheit 451", "Ray Bradbury", "10012"),
            new("The Lord of the Rings", "J.R.R. Tolkien", "10013"),
            new("The Picture of Dorian Gray", "Oscar Wilde", "10014"),
            new("Dracula", "Bram Stoker", "10015"),
            new("Frankenstein", "Mary Shelley", "10016"),
            new("Anna Karenina", "Leo Tolstoy", "10017"),
            new("Les Misérables", "Victor Hugo", "10018"),
            new("Don Quixote", "Miguel de Cervantes", "10019"),
            new("The Brothers Karamazov", "Fyodor Dostoevsky", "10020"),
            new("The Count of Monte Cristo", "Alexandre Dumas", "10021"),
            new("The Stranger", "Albert Camus", "10022"),
            new("One Hundred Years of Solitude", "Gabriel García Márquez", "10023"),
            new("The Metamorphosis", "Franz Kafka", "10024"),
            new("Slaughterhouse-Five", "Kurt Vonnegut", "10025"),
            new("Beloved", "Toni Morrison", "10026"),
            new("Lolita", "Vladimir Nabokov", "10027"),
            new("A Tale of Two Cities", "Charles Dickens", "10028"),
            new("The Old Man and the Sea", "Ernest Hemingway", "10029"),
            new("The Sun Also Rises", "Ernest Hemingway", "10030")
        };

        books.ForEach(library.AddBook);

        var members = new List<Member>
        {
            new("Anna Kowalska", "M001"),
            new("Agnieszka Nowak", "AN012"),
            new("Andrzej Rybicki", "AR013"),
            new("Wioletta Olszewska", "WO023"),
            new("Tomasz Jasicki", "TJ025"),
            new("Helena Boznańska", "HB045")
        };

        members.ForEach(library.AddMember);

        library.LoanBook("12345", "M001");
        library.LoanBook("10001", "AN012");
        library.LoanBook("10002", "AR013");
        library.LoanBook("10003", "WO023");
        library.LoanBook("10004", "TJ025");
        library.LoanBook("10005", "HB045");

        library.ReturnBook("10003");

        library.ListBooks();
        library.ListLoans();
        library.ListOverdueLoans();
    }
}