
using Newtonsoft.Json;
using ProjectClock.UI.Menu;
using ProjectClock.Database;
using ProjectClock.BusinessLogic.SqlServices;
using ProjectClock.Database.Entities;
using System.Data.SqlTypes;
using ProjectClock.BusinessLogic.SqlServices.SqlWorkingTimeServices;
using ProjectClock.BusinessLogic.SqlServices.SqlUserServices;
using Microsoft.EntityFrameworkCore;


namespace ProjectClock.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //MainMenu.RunMenu();

            var optionsBuilder = new DbContextOptionsBuilder<ProjectClockDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-0D8TD9S;Database=ProjectClockDb;Integrated Security=True;TrustServerCertificate=True"); // Zastąp to swoim połączeniem do bazy danych

            // Inicjalizacja DbContext z opcjami
            using (var dbContext = new ProjectClockDbContext(optionsBuilder.Options))
            {
                User user = new User("Stefania", "Ilowski");
                Project project = new Project() { Name = "Boobxxx.com" };
                WorkingTime workingTime = new WorkingTime() { Project = project, User = user, StartTime = DateTime.Now };
                workingTime.EndTime = DateTime.Now;

                SqlUserGeneral sqlUserGeneral = new SqlUserGeneral(dbContext);
                SqlUserGetter sqlUserGetter = new SqlUserGetter(dbContext, sqlUserGeneral);
                SqlWorkingTime sqlWorkingTime = new SqlWorkingTime(dbContext);
                SqlUserCreator sqlUserCreator = new SqlUserCreator(dbContext, sqlUserGeneral);

                sqlWorkingTime.PushToSql(workingTime);
                //sqlUserCreator.Create(user.Name, user.Surname);

                Console.WriteLine("End");
            }











        }

    }
}
