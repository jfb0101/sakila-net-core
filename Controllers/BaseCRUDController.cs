using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakila.DB;
using Sakila.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Sakila.Controllers {
    
    public class BaseCRUDController<TEntity,TKey> : ControllerBase where TEntity : class {

        public BaseCRUDController(SakilaDBContext dbContext) => this.dbContext = dbContext;

        private SakilaDBContext dbContext;

        [HttpGet]
        public virtual  IActionResult GetAll() {
            var dbSet = dbContext.Set<TEntity>();
            return Ok(dbSet.ToList());
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById(TKey id) {
            var dbSet = dbContext.Set<TEntity>();
            return Ok(dbSet.Find(id));
        }

        [HttpPost]
        public virtual IActionResult Save(TEntity entity) {
            dbContext.Add<TEntity>(entity);
            dbContext.SaveChanges();
            return Ok(entity);
        }


        [HttpDelete("{id}")]
        public virtual IActionResult Delete(TKey id) {
            var dbSet = dbContext.Set<TEntity>();
            dbSet.Remove(dbSet.Find(id));
            dbContext.SaveChanges();
            return StatusCode(204);
        }

        [HttpPut("{id}")]
        public virtual IActionResult Put([FromBody] TEntity entity, [FromRoute] TKey id) {
            var dbSet = dbContext.Set<TEntity>();
            var existingEntity = dbSet.Find(id);

            var idInEntity = getIdProp(entity).GetValue(entity);
            if (!idInEntity.Equals(id)) {
                throw new ArgumentException("ids not match");
            }
            
            dbContext.Entry(existingEntity).State = EntityState.Detached;

            Func<TEntity,TEntity> save = (_entity) => {
                dbSet.Update(_entity);
                dbContext.SaveChanges();
                return _entity;
            };

            return  existingEntity == null ? NotFound() :
                    Ok(save(entity));

        }

        [HttpPatch("{id}")]
        public virtual IActionResult Patch([FromBody] TEntity partialEntity, [FromRoute] TKey id) {
            var dbSet = dbContext.Set<TEntity>();
            var existingEntity = dbSet.Find(id);
            
            Action<object,object,PropertyInfo> copyPropertyIfNotNull = (source,target,propertyInfo) => {
                object value = propertyInfo.GetValue(source);

                if (value != null && propertyInfo.Name != getIdProp((TEntity)source).Name) {
                    propertyInfo.SetValue(target,value);
                }
            };       

            Func<object,PropertyInfo[]> getAllProperties = (obj) => obj.GetType().GetProperties(); 

            var properties = getAllProperties(existingEntity);
            properties.ToList().ForEach(p => copyPropertyIfNotNull(partialEntity,existingEntity,p));

            dbContext.SaveChanges();

            return Ok(existingEntity);
        }

        private PropertyInfo getIdProp(TEntity e)
        {
                return e.GetType().GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));
        }
    }
}