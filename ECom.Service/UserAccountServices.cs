using ECom.Database;
using ECom.Entities;
using System.Linq;

namespace ECom.Services.cs
{
    public class UserAccountServices
    {
       private static UserAccountServices instence
        { 
            get;
            set;
        }
        public static UserAccountServices Instence
        {
            get
            {
                if(instence==null)
                {
                    instence = new UserAccountServices();
                }
                return instence;
            }
            
        }

        public void AddUser(User user)
        {
            using (var context=new CDbContext())
            {
                context.users.Add(user);
                context.SaveChanges();
            }
        }
        public User GetUser(User user)
        {
            var Xuser = new User();
            using (var Context =new CDbContext())
            {
                 Xuser = Context.users.SingleOrDefault(x=>x.UserName==user.UserName && x.PassWord==user.PassWord);
                return Xuser;
            }
        }



    }
}
