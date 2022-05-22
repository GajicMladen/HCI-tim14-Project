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
    }
}
