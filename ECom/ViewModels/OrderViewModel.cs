using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.ViewModels
{
    public class OrderSearchViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserName { get;  set; }
        public List<User> Users { get; set; }
        public int? PageNo { get;  set; }
        public Pager pager { get; set; }
    }
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }
        public User User { get; set; }
        public List<string> AvailableStatus { get; set; }
    }
}