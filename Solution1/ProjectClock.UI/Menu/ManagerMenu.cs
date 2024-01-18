﻿using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProjectClock.UI.Menu
{
    internal class ManagerMenu
    {
        internal User Run()
        {
            WriteLine("\nInsert your name:");

            string name = ReadLine();

            WriteLine("\nInsert your surname:");

            string surname = ReadLine();

            User manager = new User();

            manager.Name = name;
            manager.Surname = surname;
            manager.UserPosition = Position.Manager;           

            return manager;
        }
    }
}
