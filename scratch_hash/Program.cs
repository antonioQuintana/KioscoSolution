using System;
class Program {
    static void Main() {
        Console.WriteLine("admin: " + BCrypt.Net.BCrypt.HashPassword("admin123"));
        Console.WriteLine("antonio: " + BCrypt.Net.BCrypt.HashPassword("antonio"));
    }
}
