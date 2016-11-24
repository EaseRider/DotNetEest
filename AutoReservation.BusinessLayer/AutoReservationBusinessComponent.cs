using System;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        private AutoReservationContext context;
        public AutoReservationBusinessComponent()
        {
            context = new AutoReservationContext();
        }

        public T GetById<T>(int id)
            where T : class, IEntitiesInterface
        {
            return context.Set<T>().First(arg => arg.Id == id);
        }

        public List<T> GetAll<T>()
            where T : class, IEntitiesInterface
        {
            return context.Set<T>().ToList();
        } 

        public void SaveObject<T>(T obj)
            where T: class, IEntitiesInterface
        {
            try
            {
                bool bla = obj.Id == 0;
                var blub = context.Entry<T>(obj);
                context.Entry<T>(obj).State = obj.Id == 0 ? EntityState.Added : EntityState.Modified;
                /*if (bla)
                {
                    
                }
                else
                {
                    context.Set<T>().Attach(obj);
                }*/


            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
           
            
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw CreateLocalOptimisticConcurrencyException(context, obj);
            }
        }

        public void DeleteObject<T>(T obj)
            where T : class, IEntitiesInterface
        {
            context.Entry(obj).State = EntityState.Deleted;
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw CreateLocalOptimisticConcurrencyException(context, obj);
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