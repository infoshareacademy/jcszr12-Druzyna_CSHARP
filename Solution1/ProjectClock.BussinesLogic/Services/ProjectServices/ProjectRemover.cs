﻿using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class ProjectRemover : General
    {
        public static bool RemoveProject(int id)
        {
            var projects = ProjectGetter.GetProjectList();
            var project = GetProject(id);
            if (project == null)
            {
                return false;
            }
            var path = GetDirectoryToFileFromDataFolder("projects.json");

            projects.Remove(project);
            ProjectSaver.Save(projects, path);
            return true;
        }


    }
}
