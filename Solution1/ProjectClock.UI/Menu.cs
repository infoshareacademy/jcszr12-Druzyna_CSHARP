using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ProjectClock.BusinessLogic.Models;
using static System.Console;

namespace ProjectClock.UI
{
    public class Menu
    {
        public int SelectedIndex { private set; get; }
        private string prompt;
        private string[] options;      
       
        public void DisplayIntro()
        {
            
            ForegroundColor = ConsoleColor.Cyan;

            string intro = @"
   ___              _              _      ___  _               _    
  / _ \ _ __  ___  (_)  ___   ___ | |_   / __\| |  ___    ___ | | __
 / /_)/| '__|/ _ \ | | / _ \ / __|| __| / /   | | / _ \  / __|| |/ /
/ ___/ | |  | (_) || ||  __/| (__ | |_ / /___ | || (_) || (__ |   < 
\/     |_|   \___/_/ | \___| \___| \__|\____/ |_| \___/  \___||_|\_\
                 |__/ 
";                                   
          
            WriteLine(intro);
            WriteLine("       ProjectClock allows to manage your employees worktime"); 
            WriteLine("    Use up and down to navigate and press the Enter key to select");
            WriteLine();
            ResetColor();           

        }

        public void RunMainMenu()
        {
            
            options = GetPositionsFromEnum();
            options[options.Length - 1] = "Exit";
            prompt = "Choose your position: ";                 
                        
            SelectedIndex = MoveableMenu(prompt, options);         

        }

        private string[] GetPositionsFromEnum()
        {

            Array? positionsFromEnum = Enum.GetValues(typeof(Position));
            int numberOfOptions = Enum.GetNames(typeof(Position)).Length + 1; //one extra for exit
            string[]? postionsToDisplayInMenu = new string[numberOfOptions];

            for (int i = 0; i < positionsFromEnum.Length; i++)
            {
                postionsToDisplayInMenu[i] = positionsFromEnum.GetValue(i)?.ToString() ?? String.Empty;
            }

            return postionsToDisplayInMenu;
        }

        private void DisplayOptions(string prompt, string[] options)
        {
            WriteLine(prompt);

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;

                if (i == SelectedIndex)
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

        private int MoveableMenu(string prompt, string[]options)
        {
            ConsoleKey keyPressed;
            SelectedIndex = 0;

            do
            {
                Clear();
                DisplayIntro();
                DisplayOptions(prompt, options);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }



            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }

        internal void RunManager()
        {
            WriteLine("\nInsert your name:");

            string name = ReadLine();

            WriteLine("\nInsert your surname:");

            string surname = ReadLine();
        }

        internal void RunUser()
        {
            WriteLine("\nInsert your name:");

            string name = ReadLine();

            WriteLine("\nInsert your surname:");

            string surname = ReadLine();
        }

        internal void RunExit()
        {
            WriteLine("\nPress any key to exit");
            ReadKey(true);
            Environment.Exit(0);
        }
    }
}
