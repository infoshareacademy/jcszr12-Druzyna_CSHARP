using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProjectClock.UI.Menu
{
    internal class UserMenu
    {
        internal User Run()
        {
            WriteLine("\nInsert your name:");

            string name = ReadLine();

            WriteLine("\nInsert your surname:");

            string surname = ReadLine();

            User user = new User();

            user.Name = name;
            user.Surname = surname;
            user.UserPosition = Position.User;

            return user;

        }
    }
}
