using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Model;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Project.Data
{
    public class ProjectSeedData : DbMigrationsConfiguration<ProjectEntities>
    {
        public ProjectSeedData()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(ProjectEntities context)
        {
            
        }
    }
}