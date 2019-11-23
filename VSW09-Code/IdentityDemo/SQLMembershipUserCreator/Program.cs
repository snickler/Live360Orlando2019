using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Profile;
using System.Web.Security;

namespace SQLMembershipUserCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create SQLMembership Database
            Process.Start("C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\aspnet_regsql.exe", "-S (localdb)\\mssqllocaldb -A all -E -d sqldb");
            Thread.Sleep(6000);
       
            Membership.CreateUser("TestUser2@address.com", "!TestUser2!", "TestUser2@address.com", "Who Am I?", "The User", true, out _); ;
            Membership.CreateUser("TestAdmin2@address.com", "!TestAdmin2!", "TestAdmin2@address.com", "Who am I?", "The Admin", true, out _);
           var user =  Membership.GetUser("TestUser2@address.com");
            var user2 = Membership.GetUser("TestAdmin2@address.com"); 
        
            var profile1 = ProfileBase.Create(user.UserName);
            profile1.SetPropertyValue("PhoneNumber", "333-333-3333");
            profile1.SetPropertyValue("FirstName","Test");
            profile1.SetPropertyValue("LastName", "User");
            profile1.Save();
        
            var profile2 = ProfileBase.Create(user2.UserName);
            profile2.SetPropertyValue("PhoneNumber", "444-444-4444");
            profile2.SetPropertyValue("FirstName", "Test");
            profile2.SetPropertyValue("LastName", "Admin");
            profile2.Save();
        
            Roles.CreateRole("User");
            Roles.CreateRole("Admin");
            
            Roles.AddUserToRole(user.UserName, "User");
            Roles.AddUserToRole(user2.UserName, "Admin");
  

            Console.WriteLine("Users Created");
        }
    }
}
