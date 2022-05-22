using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    public static class UsersDAO
    {
        public static User tryLogIn(string username, string password) {
            
            int userID = -1;
            try{
               userID = int.Parse(username);
            }
            catch{
                userID = -1;
            }

            using (var context = new SerbiaRailwayContext()) {

                return context.users.Where(u =>
               ( u.Email == username && u.Password == password) ||
               ( userID != -1 && u.UserID == userID && u.Password == password)).FirstOrDefault();
            }
        }

        public static int registerNewClient(string firsName, string lastName ,string email, string phone,string password) {

            using (var context = new SerbiaRailwayContext()) {

                User newUser = new User()
                {
                    FirstName = firsName,
                    LastName = lastName,
                    Password = password,
                    Email = email,
                    Phone = phone,
                    UserRole = UserRole.CLIENT
                };

                context.users.Add(newUser);
                context.SaveChanges();

                return newUser.UserID;

            }
        }
    }
}
