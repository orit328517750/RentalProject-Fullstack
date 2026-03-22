using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBConection
    {
        public DBConection() { }
        public List<T> GetDbSet<T>() where T : class
        {
            using (CarsDBEntities1 carsDBEntities = new CarsDBEntities1())
            {
                return carsDBEntities.GetDbSet<T>().ToList();
            }
        }
        public enum ExecuteActions
        {
            Insert,
            Update,
            Delete
        }
        public void Execute<T>(T entity, ExecuteActions exAction) where T : class
        {
            using (CarsDBEntities1 carsDBEntities = new CarsDBEntities1())
            {
                var model = carsDBEntities.Set<T>();
                switch (exAction)
                {
                    case ExecuteActions.Insert:
                        model.Add(entity);
                        break;
                    case ExecuteActions.Update:
                        model.Attach(entity);
                        carsDBEntities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        break;
                       
                case ExecuteActions.Delete:
                        model.Attach(entity);
                        carsDBEntities.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                        break;
                    default:
                        break;
                }
                carsDBEntities.SaveChanges();
            }
        }
    }
}
