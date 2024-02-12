using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlProjectServices.SqlProjectInterfaces
{
    public interface ISqlProjectInterfaces
    {
    }

    public interface ISqlProjectGetter
    {
        List<Project> GetProjectList();
        Project GetProject(int id);
    }

    public interface ISqlProjectGeneral()
    {
        bool Exist(int id);
    }
}
