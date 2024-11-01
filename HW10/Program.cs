using HW10.Entity;
using HW10.Enum;
using HW10.Services;
using System.Drawing;
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Microsoft Visual Studio [2022]");
UserService service = new UserService();
bool check = false;
do
{
    try
    {
        Console.ForegroundColor = ConsoleColor.Green;
        string result = service.CheckCommand(Console.ReadLine(), out List<User> users);
        Console.ForegroundColor= ConsoleColor.Gray;
        if (result == CommandEnum.help.ToString())
        {
            Guide();
        }
        else if (result == CommandEnum.cls.ToString())
        {
            Console.Clear();
        }
        else if (users is not null)
        {
            Console.WriteLine(result);
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
            }
        }
        else
        {
            Console.WriteLine(result);
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("FormatExceptionError: The request must be in Correct Format.use command *--help*.");
    }
}while(!check);
void Guide()
{
    Console.WriteLine();
    Console.WriteLine("Commands defined in the system: ",Console.ForegroundColor=ConsoleColor.Green);
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("--------------");
    Console.WriteLine("*Register --username [username] --password [password] *");
    Console.WriteLine("*Login --username [username] --password [password]*");
    Console.WriteLine("*Change --status Available*");
    Console.WriteLine("*Change --status UnAvailable*");
    Console.WriteLine("*Search --username [username]*");
    Console.WriteLine("*ChangePassword --old [myOldPassword] --new [myNewPassword]*");
    Console.WriteLine("*Logout*");
    Console.WriteLine("*cls*  :  Clears the window");
    Console.WriteLine("--------------");
    Console.ForegroundColor=ConsoleColor.Gray;
}