﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProjectClock.UI.Menu.Services
{
    internal class MenuServices
    {
        private void DisplayOptions(int selectedIndex, string prompt, string[] options)
        {
            WriteLine(prompt);

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;

                if (i == selectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                WriteLine($" {prefix} << {currentOption} >>");
            }

            ResetColor();

        }

        internal int MoveableMenu(string prompt, string[] options, bool wantClear, string introMenu = "")
        {
            ConsoleKey keyPressed;
            int selectedIndex = 0;

            do
            {
                if (wantClear)
                {
                    Clear();
                }                

                ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(introMenu);
                ResetColor();

                DisplayOptions(selectedIndex, prompt, options);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == options.Length)
                    {
                        selectedIndex = 0;
                    }
                }



            } while (keyPressed != ConsoleKey.Enter);

            return selectedIndex;
        }



    }
}