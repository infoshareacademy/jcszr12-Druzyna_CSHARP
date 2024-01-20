﻿using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class ProjectEditor : General
    {
        public static void ModifyProject(int id,int idNew, string nameNew)
        {
            var projects = ProjectGetter.GetProjectList();         
            var project = GetProject(id);
            var path = GetDirectoryToFileFromDataFolder("projects.json");

            projects.Remove(project);
            SetID(project, idNew);
            SetName(project, nameNew);
            projects.Add(project);

            ProjectSaver.Save(projects, path);
        }


    }
}
