using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ProjectClock.BusinessLogic.Models;
using ProjectClock.UI.Menu.Services;
using static System.Console;

namespace ProjectClock.UI.Menu
{
    public class MainMenu
    {
        public int SelectedIndex { private set; get; }
        private string prompt;
        private string[] options;

        public static string Intro()
        {
            StringBuilder sb = new StringBuilder();

            

            string intro = @"
   ___              _              _      ___  _               _    
  / _ \ _ __  ___  (_)  ___   ___ | |_   / __\| |  ___    ___ | | __
 / /_)/| '__|/ _ \ | | / _ \ / __|| __| / /   | | / _ \  / __|| |/ /
/ ___/ | |  | (_) || ||  __/| (__ | |_ / /___ | || (_) || (__ |   < 
\/     |_|   \___/_/ | \___| \___| \__|\____/ |_| \___/  \___||_|\_\
                 |__/ 
";


            sb.Append(intro);

            string introTextFirstLine = ("       ProjectClock allows to manage your employees worktime");
            string introTextSecondLine = ("    Use up and down to navigate and press the Enter key to select");
            string introTextThirdLine = ("                                                                      ");

            sb.AppendLine(introTextFirstLine);
            sb.AppendLine(introTextSecondLine);
            sb.AppendLine(introTextThirdLine);


            return sb.ToString();

        }

        public void RunMainMenu()
        {

            options = GetPositionsFromEnum();
            options[options.Length - 1] = "Exit";
            prompt = "Choose your position: ";

            MenuServices menuService = new MenuServices();

            SelectedIndex = menuService.MoveableMenu(prompt, options, Intro());

            switch (SelectedIndex)
            {
                case 0:

                    ManagerMenu managerMenu = new ManagerMenu();
                    managerMenu.Run();
                    break;

                case 1:

                    UserMenu userMenu = new UserMenu();
                    userMenu.Run();
                    break;

                case 2:

                    ExitMenu exitMenu = new ExitMenu();
                    exitMenu.Run();
                    break;
            }

        }

        private string[] GetPositionsFromEnum()
        {

            Array? positionsFromEnum = Enum.GetValues(typeof(Position));
            int numberOfOptions = Enum.GetNames(typeof(Position)).Length + 1; //one extra for exit
            string[]? postionsToDisplayInMenu = new string[numberOfOptions];

            for (int i = 0; i < positionsFromEnum.Length; i++)
            {
                postionsToDisplayInMenu[i] = positionsFromEnum.GetValue(i)?.ToString() ?? string.Empty;
            }

            return postionsToDisplayInMenu;
        }

      

       

      

      

     








    }
}
