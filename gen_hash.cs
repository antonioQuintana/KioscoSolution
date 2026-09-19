using System;
using System.Reflection;

class Program {
    static void Main() {
        var bcrypt = Assembly.LoadFrom("C:\Users\laran\.nuget\packages\bcrypt.net-next\4.0.3\lib\net6.0\BCrypt.Net-Next.dll");
        var type = bcrypt.GetType("BCrypt.Net.BCrypt");
        var method = type.GetMethod("HashPassword", new Type[] { typeof(string) });
        string hash = (string)method.Invoke(null, new object[] { "antonio" });
        Console.WriteLine(hash);
    }
}
