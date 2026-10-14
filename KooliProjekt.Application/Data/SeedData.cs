using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;


namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {

        public static void Generate(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            SeedUsers(context);
            SeedAssignments(context);
            SeedAttachements(context);
            SeedProjects(context);
            SeedTeams(context);
            SeedWorkLogs(context);
        }
        private static void SeedUsers(ApplicationDbContext context)
        {
            if (context.Users.Count() >= 35) return;

            var users = new List<User>
            {
                new User { UserName = "mari.tamm", PasswordHash = "sample1", Email = "mari@example.com" },
                new User { UserName = "jaan.kask", PasswordHash = "sample2", Email = "jaan@example.com" },
                new User { UserName = "kati.saar", PasswordHash = "sample3", Email = "kati@example.com" },
                new User { UserName = "martin.kuusk", PasswordHash = "sample4", Email = "martin@example.com" },
                new User { UserName = "liis.kaar", PasswordHash = "sample5", Email = "liis@example.com" },
                new User { UserName = "andres.pold", PasswordHash = "sample6", Email = "andres@example.com" },
                new User { UserName = "kadri.lepp", PasswordHash = "sample7", Email = "kadri@example.com" },
                new User { UserName = "peeter.ots", PasswordHash = "sample8", Email = "peeter@example.com" },
                new User { UserName = "anna.kukk", PasswordHash = "sample9", Email = "anna@example.com" },
                new User { UserName = "rainer.saar", PasswordHash = "sample10", Email = "rainer@example.com" }
            };

            for (int i = users.Count + 1; i <= 35; i++)
            {
                users.Add(new User
                {
                    UserName = $"employee{i}",
                    PasswordHash = $"sample{i}",
                    Email = $"employee{i}@example.com"
                });
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }


        private static void SeedAssignments(ApplicationDbContext context)
        {
            if (context.Assignments.Count() >= 35) return;

            var users = context.Users.ToList();

            var descriptions = new[]
            {
                "Develop a customer management module",
                "Fix authentication and login issues",
                "Create a reporting dashboard",
                "Improve application performance",
                "Implement database validation",
                "Design a responsive user interface",
                "Add document upload functionality",
                "Prepare automated tests",
                "Update technical documentation",
                "Implement task tracking"
            };

            var assignments = new List<Assignment>();

            for (int i = 1; i <= 35; i++)
            {
                assignments.Add(new Assignment
                {
                    FixedPrice = i % 2 == 0,
                    TeamLead = users[(i - 1) % users.Count],
                    AssignmentDesc = descriptions[(i - 1) % descriptions.Length]
                });
            }

            context.Assignments.AddRange(assignments);
            context.SaveChanges();
        }


        private static void SeedProjects(ApplicationDbContext context)
        {
            if (context.Projects.Count() >= 35) return;

            var names = new[]
            {
                "Customer Portal",
                "Inventory Management System",
                "Task Tracking Application",
                "Reporting Platform",
                "Employee Management System",
                "Document Management Portal",
                "Online Booking System",
                "Project Planning Tool",
                "Service Desk Application",
                "Sales Dashboard"
            };

            var projects = new List<Project>();

            for (int i = 1; i <= 35; i++)
            {
                projects.Add(new Project
                {
                    ProjectName = $"{names[(i - 1) % names.Length]} {i}"
                });
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();
        }
        private static void SeedTeams(ApplicationDbContext context)
        {
            if (context.Teams.Count() >= 35) return;

            var users = context.Users.ToList();
            var teams = new List<Team>();

            for (int i = 1; i <= 35; i++)
            {
                teams.Add(new Team
                {
                    TeamLead = users[(i - 1) % users.Count]
                });
            }

            context.Teams.AddRange(teams);
            context.SaveChanges();
        }
        private static void SeedAttachements(ApplicationDbContext context)
        {
            if (context.Attachements.Count() >= 35) return;

            var users = context.Users.ToList();
            var assignments = context.Assignments.ToList();
            var attachments = new List<Attachement>();

            for (int i = 1; i <= 35; i++)
            {
                attachments.Add(new Attachement
                {
                    FileName = $"ProjectDocument{i}.pdf",
                    FilePath = $"Documents/ProjectDocument{i}.pdf",
                    UploadedBy = users[(i - 1) % users.Count],
                    Assignment = assignments[(i - 1) % assignments.Count]
                });
            }

            context.Attachements.AddRange(attachments);
            context.SaveChanges();
        }

        private static void SeedWorkLogs(ApplicationDbContext context)
        {
            if (context.WorkLogs.Count() >= 35) return;

            var users = context.Users.ToList();
            var assignments = context.Assignments.ToList();

            var descriptions = new[]
            {
                "Implemented and tested requested changes",
                "Fixed a bug and verified the solution",
                "Reviewed code and updated documentation",
                "Investigated a technical issue",
                "Completed development and testing",
                "Improved validation and error handling",
                "Discussed requirements with the team",
                "Updated database integration",
                "Performed regression testing",
                "Reviewed the implementation"
            };

            var worklogs = new List<WorkLog>();

            for (int i = 1; i <= 35; i++)
            {
                worklogs.Add(new WorkLog
                {
                    Assignment = assignments[(i - 1) % assignments.Count],
                    Implementer = users[(i - 1) % users.Count],
                    Description = descriptions[(i - 1) % descriptions.Length]
                });
            }

            context.WorkLogs.AddRange(worklogs);
            context.SaveChanges();
        }

    }
}