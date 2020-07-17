using ECom.Database;
using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ECom.Services.cs
{
    public class ProductsServices
    {
        private static ProductsServices instence
        { get; set; }
        public static ProductsServices Instence
        {
            get
            {
                if (instence == null)
                {
                    instence = new ProductsServices();
                }
                return instence;
            }
        }

        public int GetMaximunPrice()
        {
            try
            {
                using (var context = new CDbContext())
                {
                    return (int)context.products.Max(P => P.Price);
                }
            }
            catch
            {
                return 0;
            }
            
            
                
        }

   

        public List<Product> GetProducts(string searchTerm, int? maximumPrice, int? munimumPrice, int? CategoryID ,int? SortBy, int pageNo,int pageSize)
        {
            using (var context=new CDbContext())
            {
                var product = context.products.ToList();
                if(!string.IsNullOrEmpty(searchTerm))
                {
                    product = product.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if(CategoryID.HasValue)
                {
                    product = product.Where(p => p.CategoryID == CategoryID.Value).ToList();
                }
                if(maximumPrice.HasValue)
                {
                    product = product.Where(p => p.Price <= maximumPrice.Value).ToList();
                }
                if(munimumPrice.HasValue)
                {
                    product = product.Where(p=>p.Price>=munimumPrice.Value).ToList();
                }
                if(SortBy.HasValue)
                {
                    switch (SortBy.Value)
                    {
                        case 2:
                            product = product.OrderBy(p => p.ID).ToList();
                            break;
                        case 3:
                            product = product.OrderBy(p=>p.Price).ToList();
                            break;
                        case 4:
                            product = product.OrderByDescending(p => p.Price).ToList();
                            break;
                        default:
                            product = product.OrderByDescending(p => p.ID).ToList();
                            break;
                    }
                }
                return product.Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
            }
        }
        public int GetProductsCount(string searchTerm)
        {
            using (var context = new CDbContext())
            {
                var product = context.products.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    product = product.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                
                return product.Count();
            }
        }
            public int GetProductsCount(string searchTerm, int? maximumPrice, int? munimumPrice, int? CategoryID, int? SortBy)
        {
            using (var context = new CDbContext())
            {
                var product = context.products.ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    product = product.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (CategoryID.HasValue)
                {
                    product = product.Where(p => p.CategoryID == CategoryID.Value).ToList();
                    
                }
                if (maximumPrice.HasValue)
                {
                    product = product.Where(p => p.Price <= maximumPrice.Value).ToList();
                }
                if (munimumPrice.HasValue)
                {
                    product = product.Where(p => p.Price >= munimumPrice.Value).ToList();
                }
                if (SortBy.HasValue)
                {
                    switch (SortBy.Value)
                    {
                        case 2:
                            product = product.OrderBy(p => p.ID).ToList();
                            break;
                        case 3:
                            product = product.OrderBy(p => p.Price).ToList();
                            break;
                        case 4:
                            product = product.OrderByDescending(p => p.Price).ToList();
                            break;
                        default:
                            product = product.OrderByDescending(p => p.ID).ToList();
                            break;
                    }
                }
                return product.Count();
            }
        }



        public ProductsServices()
        {

        }
        public void SaveProduct(Product product)

        {
            using (var context = new CDbContext())
            {
                try
            {
                    context.Entry(product).State = System.Data.Entity.EntityState.Unchanged;

                    context.products.Add(product);
                    context.SaveChanges();
                    
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
                    
                }
            }
        public List<Product> GetProducts(int pageNo)
        {
            //int pageSize = 12;
            using (var context = new CDbContext())
            {
                return context.products.Include(x => x.category).ToList();
                //return context.products.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();


                //    return context.products.ToList();
            }
        }
        public List<Product> GetProducts(List<int> Ids)
        {
          
            using (var context = new CDbContext())
            {
                return context.products.Where(product => Ids.Contains(product.ID)).ToList();

            }
        }
        public List<Product> GetProducts(string Search,int pageNo,int pageSize)
            {
            using (var context = new CDbContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return context.products.Where(P=>P.Name !=null && P.Name.ToLower().Contains(Search.ToLower()) ).OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();
                }
                return context.products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();

            }


            }
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new CDbContext())
            {
               
                return context.products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();

            }


        }
        public List<Product> GetAllProduct(List<int> IDs)
            {
                using (var context = new CDbContext())
                {
                    return context.products.Where(product=>IDs.Contains(product.ID)).ToList();
                }
            }
        public List<Product> GetLatestProducts(int nProducts)
        {
            using (var context = new CDbContext())
            {
                return context.products.OrderByDescending(p=>p.ID).Include(p=>p.category).Take(nProducts).ToList();
            }
        }
        public List<Product> GetRelatedProducts(int? CategoryID, int nProducts)
        {
            using (var context = new CDbContext())
            {
                return context.products.Where(p=>p.category.ID==CategoryID).OrderByDescending(p => p.ID).Include(p => p.category).Take(nProducts).ToList();
            }
        }
        public Product GetProduct(int ID)
        {
            using (var context = new CDbContext())
            {
                return context.products.Where(p=>p.ID==ID).Include(p=>p.category).FirstOrDefault();
            }
        }
        public void UpdateProduct(Product product)
            {
            try
            {
                using (CDbContext context = new CDbContext())
                {
                    context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            }
            public void DeleteProduct(int ID)
            {
                using (CDbContext context = new CDbContext())
                {
                    Product product = context.products.Find(ID);
                    context.products.Remove(product);
                    context.SaveChanges();
                }
            }
        
    }
}
