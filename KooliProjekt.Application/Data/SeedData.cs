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
            var users = new List<User>
            {
                new User { UserName = "user1", PasswordHash = "password1", Email = "Email1" },
                new User { UserName = "user2", PasswordHash = "password2", Email = "Email2" }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }

         private static void SeedAssignments(ApplicationDbContext context)
        {
            User leitudKasutaja = context.Users.First(u => u.UserName == "user1");
            var assignments = new List<Assignment>
            {
                new Assignment { FixedPrice = false, TeamLead = leitudKasutaja, AssignmentDesc = "Mingi töö jne jne" },
                new Assignment {  FixedPrice = false, TeamLead = leitudKasutaja, AssignmentDesc = "Mingi töö jne jne"}
            };
            context.Assignments.AddRange(assignments);
            context.SaveChanges();
        }

         private static void SeedAttachements(ApplicationDbContext context)
        {
            User leitudKasutaja = context.Users.First(u => u.UserName == "user1");
            User leitudAssignment = context.Assignments
            var attachments = new List<Attachement>
            {
              new Attachement {FileName = "file1",FilePath = "Desktop/File" , UploadedBy = leitudKasutaja, Assignment = }  
            };
        }
        
    }
}