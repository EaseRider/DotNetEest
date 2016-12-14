using System;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public T GetById<T>(int id)
            where T : class, IEntitiesInterface
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if (typeof (T) == typeof (Reservation))
                {
                    return GetFKPropertyNames<T>().Aggregate((IQueryable <T> )context.Set<T>(), 
                        (current, fk) => current.Include(fk)).First(arg => arg.Id == id);
                    //return context.Set<T>().Include("Auto").Include("Kunde").First(arg => arg.Id == id);
                }
                var set = context.Set<T>().ToList();
                return context.Set<T>().First(arg => arg.Id == id);
            }
        }
        
        public IEnumerable<T> GetAll<T>()
            where T : class, IEntitiesInterface
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if (typeof (T) != typeof (Reservation))
                {
                    return context.Set<T>().ToList();
                }
                else
                {
                    IQueryable<T> set = context.Set<T>();
                    set = GetFKPropertyNames<T>().Aggregate(set, (current, fk) => current.Include(fk));
                    return set.ToList();
                }
            }
        }

        public IEnumerable<string> GetFKPropertyNames<TEntity>() 
            where TEntity : class
        {
            using (var context = new AutoReservationContext())
            {
                ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
                ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();

                var navProperties = set.EntitySet.ElementType.NavigationProperties.Select(np => np.Name);
                return navProperties;
                // Code to get fk-ID fields
                //var Fks = set.EntitySet.ElementType.NavigationProperties.SelectMany(n => n.GetDependentProperties());
                //return Fks.Select(fk => fk.Name);
            }
        }

        public void SaveObject(Auto obj)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if (obj.Id != 0)
                {
                    var entity = context.Autos.Include("Reservationen").First(auto => auto.Id == obj.Id);
                    if (ObjectContext.GetObjectType(entity.GetType()) == ObjectContext.GetObjectType(obj.GetType()))
                    {
                        SaveObject<Auto>(obj);
                    }
                    else
                    {
                        obj.Id = 0;
                        Auto newAuto = SaveObject<Auto>(obj);
                        foreach (Reservation reservation in entity.Reservationen)
                        {
                            reservation.AutoId = newAuto.Id;
                            reservation.Auto = newAuto;
                            SaveObject(reservation);
                        }
                        entity.Reservationen.Clear();
                        DeleteObject(entity);
                    }
                }
                else
                {
                    SaveObject<Auto>(obj);
                }
            }
        }

        public T SaveObject<T>(T obj)
            where T: class, IEntitiesInterface
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(obj).State = obj.Id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(context, obj);
                }
                return obj;
            }
        }

        public void DeleteObject<T>(T obj)
            where T : class, IEntitiesInterface
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var entity = context.Set<T>().Find(obj.Id);
                if (entity != null)
                {
                    try
                    {
                        context.Entry(entity).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw CreateLocalOptimisticConcurrencyException(context, obj);
                    }
                }
            }
                
        }
        

        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(Auto).Name}: Concurrency-Fehler", dbEntity);
        }
    }
}