using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECom.Database;
using System.Data.Entity;


namespace ECom.Services.cs
{
    public class CategoriesService
    {
        public static CategoriesService Instence
        {
            get
            {
                if(instence==null)
                {
                    instence = new CategoriesService();
                }
                return instence;
            }
        }
        private static CategoriesService instence { get; set; }
        public CategoriesService() { }
        public int GetCategoriesCount(string search)
        {
            using ( var context=new CDbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.categories.Where(x => x.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.categories.Count();

                }
            }
                
        }
        public void SaveCategory(ECom.Entities.Category category)
        {
           
                using (var context = new CDbContext())
                {
                    context.categories.Add(category);
                    context.SaveChanges();
                }
            
           
        }
        public List<ECom.Entities.Category> GetCategories(string search,int pageNo)
        {
            int pageSize = 2;
             using (var context = new CDbContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return context.categories.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.products).ToList();
                }             
                    return context.categories.Where(x => x.Name.ToLower().Contains(search.ToLower())).Include(x => x.products).OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            }
           

        }
        public List<ECom.Entities.Category> GetAllCategories()
        {
            using (var context = new CDbContext())
            {
                return context.categories.ToList();
            }
        }

        public List<ECom.Entities.Category> GetFeaturedCategories()
        {
            using (var context = new CDbContext())
            {
                return context.categories.Where(x=> x.isFeatured && x.ImageURL !=null).ToList();
            }
        }
        public ECom.Entities.Category GetCategory(int ID)
        {
            using (var context = new CDbContext())
            {
                return context.categories.Find(ID);
            }
        }
        public void UpdateCategory(ECom.Entities.Category category)
        {
            using (CDbContext context=new CDbContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using (CDbContext context = new CDbContext())
            {
                ECom.Entities.Category category = context.categories.Where(x => x.ID == ID).Include(x => x.products).FirstOrDefault();
                context.products.RemoveRange(category.products);
                context.categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
   
    
}
