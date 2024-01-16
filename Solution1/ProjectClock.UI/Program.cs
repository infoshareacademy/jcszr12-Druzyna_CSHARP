
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;

namespace ProjectClock.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();

            menu.RunPositionMenu();

            switch (menu.SelectedIndex)
            {
                case 0:
                    menu.RunManager();
                    break;

                case 1:
                    menu.RunUser();
                    break;
               
                case 2:
                    menu.RunExit();
                    break;
            }






        }
    }
}
