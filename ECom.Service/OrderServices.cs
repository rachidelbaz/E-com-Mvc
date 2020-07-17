using ECom.Database;
using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ECom.Services.cs
{
    public class OrderServices
    {
        public static OrderServices Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderServices();
                }
                return instance;
            }
        }



        private static OrderServices instance
        {
            get;
            set;
        }
        public OrderServices() { }

        public List<User> GetUsers(string userName)
        {
            using (var Context = new CDbContext())
            {
                var Users = Context.users.ToList();
                if (!string.IsNullOrEmpty(userName))
                {
                    Users = Context.users.Where(u => u.UserName.ToLower().Contains(userName.ToLower())).ToList();
                }
                //Users.Skip((PageNo - 1) * pageSize).Take(pageSize);
                return Users;
            }
        }

        public int GetOrdersCout(string UserName)
        {
            using (var Context = new CDbContext())
            {
                var orders = Context.orders.ToList();

                if (!string.IsNullOrEmpty(UserName))
                {
                    var User = Context.users.Where(U => U.UserName.ToLower().Contains(UserName.ToLower())).FirstOrDefault();
                    orders = Context.orders.Where(O => O.UserId == User.userId).ToList();
                }

                return orders.Count;
            }
        }

        public void UpdateOrderStatus(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderStatus(int id,string status)
        {
            using (var Context=new CDbContext())
            {
                var order=Context.orders.Find(id);
                order.Status = status;
                Context.Entry(order).State = EntityState.Modified;
                return Context.SaveChanges()>0;
            }
        }

        public User GetUserByOrderId(int id)
        {
            using (var Context = new CDbContext())
            {
               
                var Orderby = Context.orders.Find(id);
                return Context.users.Where(u => u.userId == Orderby.UserId).SingleOrDefault();
            }
        }

        public Order GetOrderById(int ID)
        {
            using (var Context = new CDbContext())
            {

                //return Context.orders.Where(o => o.Id == ID).Include(o => o.OrderItems.Select(oi => oi.Product)).SingleOrDefault();

                return Context.orders.Where(o => o.Id == ID).Include(o => o.OrderItems).Include("OrderItems.Product").SingleOrDefault();
            }
        }

        public List<Order> GetOrders()
        {
            using (var Context = new CDbContext())
            {
                return Context.orders.ToList();
            }
        }
        public List<Order> SearchOrders(string UserName, int PageNo, int PageSize)
        {
            using (var Context = new CDbContext())
            {
                var orders = Context.orders.ToList();
                List<Order> orders1 = new List<Order>();
                if (!string.IsNullOrEmpty(UserName))
                {
                    var User = Context.users.Where(U => U.UserName.ToLower().Contains(UserName.ToLower())).ToList();
                  
                    foreach (var user in User)
                    {
                        foreach(var order in orders)
                        {
                            if (user.userId == order.UserId)
                            {
                                orders1.Add(order);
                            }
                        }
                    }
                    orders.Clear();
                    orders.AddRange(orders1);
                    //orders = Context.orders.Where(O => O.UserId == User.userId).ToList();

                    //orders1.AddRange(orders => orders.Where(o == User.Select(u => u.userId).ToList()));
                }
                if(orders1.Count>0)
                {

                    return orders1.Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
                }
                else
                {
                    return orders.Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
                }
              
                //return orders;
            }
        }
    }
}
