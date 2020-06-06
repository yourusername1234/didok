using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Username: ");
            String uname = Console.ReadLine();
            Console.Write("Password: ");
            String pass = Console.ReadLine();


            LoginValidation lv = new LoginValidation(uname, pass, errorFunc);
            User user = new User { };

            if (lv.ValidateUserInput(ref user))
            {
                if(LoginValidation.currentUserRole == UserRoles.ADMIN)
                {
                    adminPanel();
                }
                Console.WriteLine("\nИме: " + user.username);
                Console.WriteLine("Факултетен номер: " + user.fakNum);
                switch (LoginValidation.currentUserRole) 
                {
                    case UserRoles.ANONYMOUS:
                        Console.WriteLine("Роля: Анонимен");
                        break;
                    case UserRoles.ADMIN:
                        Console.WriteLine("Роля: Администратор");
                        break;
                    case UserRoles.INSPECTOR:
                        Console.WriteLine("Роля: Инспектор");
                        break;
                    case UserRoles.PROFESSOR:
                        Console.WriteLine("Роля: Професор");
                        break;
                    case UserRoles.STUDENT:
                        Console.WriteLine("Роля: Студент");
                        break;
                }
                Console.WriteLine("Дата на създаване: " + user.created);
                Console.WriteLine("Активност: " + user.isActive);
            }
        }

        static void errorFunc (String msg)
        {
            Console.WriteLine(msg);
        }

        static void adminPanel()
        {
            
            StringBuilder currentActivities = new StringBuilder();
            string choice;
            do 
            {
                Console.WriteLine("Избере опция:");
                Console.WriteLine("0: Изход");
                Console.WriteLine("1: Промяна на роля на потребител");
                Console.WriteLine("2: Промяна на активност на потребител");
                Console.WriteLine("3: Списък на потребителите");
                Console.WriteLine("4: Преглед на лог на активност");
                Console.WriteLine("5: Преглед на текуща активност");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Въведете име и нова роля.");
                        String name = Console.ReadLine();
                        int role = Convert.ToInt32(Console.ReadLine());
                        UserRoles newRole = (UserRoles)role;
                        UserData.AssignUserRole(name, newRole);
                        break;

                    case "2":
                        Console.WriteLine("Въведете име и нова дата.");
                        name = Console.ReadLine();
                        String date = Console.ReadLine();
                        DateTime dt = DateTime.Parse(date);
                        UserData.SetUserActiveTo(name, dt);
                        break;

                    case "3":
                        UserContext context = new UserContext();
                        foreach (User testUser in context.Users)
                        {
                            Console.WriteLine(testUser.username);
                            Console.WriteLine(testUser.fakNum);
                            Console.WriteLine((UserRoles)testUser.role);
                            Console.WriteLine(testUser.created);
                            Console.WriteLine(testUser.isActive);
                            Console.WriteLine("\n");
                        }
                        break;

                    case "4":
                        StreamReader sr = new StreamReader("C:/Users/Ivan/Desktop/PS_39_Ivan/UserLogin/log.txt");
                        StringBuilder log = new StringBuilder();
                        log.Append(sr.ReadToEnd());
                        Console.WriteLine(log);
                        sr.Close();
                        break;

                    case "5":
                        currentActivities.Append(Logger.GetCurrentSessionActivities());
                        Console.WriteLine(currentActivities);
                        break;

                    default:
                        break;
                }
            }while (choice != "0");
        }
    }
}
