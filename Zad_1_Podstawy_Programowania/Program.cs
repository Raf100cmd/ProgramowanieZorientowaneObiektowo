using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1 - Kalkulator");
            Console.WriteLine("2 - Konwerter temperatur");
            Console.WriteLine("3 - Średnia ocen");
            Console.WriteLine("0 - Wyjście");
            Console.Write("Wybierz opcję: ");

            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    Kalkulator();
                    break;
                case "2":
                    KonwerterTemperatur();
                    break;
                case "3":
                    SredniaOcen();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
        }
    }

    static void Kalkulator()
    {
        double num1 = PobierzLiczbe("Podaj pierwszą liczbę: ");
        double num2 = PobierzLiczbe("Podaj drugą liczbę: ");
        string operation;

        while (true)
        {
            Console.Write("Wybierz operację (+, -, *, /): ");
            operation = Console.ReadLine();
            if (operation == "+" || operation == "-" || operation == "*" || operation == "/")
                break;
            Console.WriteLine("Błąd: Nieznana operacja. Spróbuj ponownie.");
        }

        double result = 0;
        switch (operation)
        {
            case "+": result = num1 + num2; break;
            case "-": result = num1 - num2; break;
            case "*": result = num1 * num2; break;
            case "/":
                if (num2 != 0)
                    result = num1 / num2;
                else
                {
                    Console.WriteLine("Błąd: dzielenie przez zero!");
                    return;
                }
                break;
        }

        Console.WriteLine($"Wynik: {result}");
    }

    static void KonwerterTemperatur()
    {
        string choice;
        while (true)
        {
            Console.Write("Wybierz konwersję (C - Celsjusz → Fahrenheit, F - Fahrenheit → Celsjusz): ");
            choice = Console.ReadLine().ToUpper();
            if (choice == "C" || choice == "F")
                break;
            Console.WriteLine("Błąd: Wybierz 'C' lub 'F'.");
        }

        double temp = PobierzLiczbe("Podaj temperaturę: ");

        if (choice == "C")
            Console.WriteLine($"{temp}°C = {temp * 1.8 + 32:F2}°F");
        else
            Console.WriteLine($"{temp}°F = {(temp - 32) / 1.8:F2}°C");
    }

    static void SredniaOcen()
    {
        int liczbaOcen;
        while (true)
        {
            liczbaOcen = PobierzLiczbeCalkowita("Podaj liczbę ocen: ");
            if (liczbaOcen > 0)
                break;
            Console.WriteLine("Błąd: Liczba ocen musi być większa od zera.");
        }

        double suma = 0;
        for (int i = 0; i < liczbaOcen; i++)
        {
            while (true)
            {
                double ocena = PobierzLiczbe($"Podaj ocenę {i + 1}: ");
                if (ocena >= 1.0 && ocena <= 6.0)
                {
                    suma += ocena;
                    break;
                }
                Console.WriteLine("Błąd: Ocena musi być w zakresie od 1 do 6.");
            }
        }

        double srednia = suma / liczbaOcen;
        Console.WriteLine($"Średnia: {srednia:F2}");
        Console.WriteLine(srednia >= 3.0 ? "Uczeń zdał." : "Uczeń nie zdał.");
    }

    static double PobierzLiczbe(string komunikat)
    {
        while (true)
        {
            Console.Write(komunikat);
            if (double.TryParse(Console.ReadLine(), out double liczba))
                return liczba;
            Console.WriteLine("Błąd: Wprowadź poprawną liczbę.");
        }
    }

    static int PobierzLiczbeCalkowita(string komunikat)
    {
        while (true)
        {
            Console.Write(komunikat);
            if (int.TryParse(Console.ReadLine(), out int liczba))
                return liczba;
            Console.WriteLine("Błąd: Wprowadź poprawną liczbę całkowitą.");
        }
    }
}