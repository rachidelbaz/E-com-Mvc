using ECom.Database;
using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Services.cs
{
    public class ShopServices
    {
        public static ShopServices Instence
        {
            get
            {
                if (instence == null)
                {
                    instence = new ShopServices();
                }
                return instence;
            }
        }
        private static ShopServices instence { get; set; }
        public ShopServices() { }
        
        public int SaveOrder(Order order)
        {
            using (var Context=new CDbContext())
            {
                Context.orders.Add(order);
                return Context.SaveChanges();
            }
        }

    }
}
