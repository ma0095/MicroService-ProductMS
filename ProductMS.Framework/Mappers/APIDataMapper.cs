using Microsoft.Extensions.DependencyInjection;
using ProductMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework.Mappers
{

    /// <summary>
    /// API DataMapper
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public abstract class APIDataMapper<TEntity, TObject>
    where TEntity : IEntity
    where TObject : class, new()
    {
        #region Private Members

        /// <summary>
        /// Gets or sets the services.
        /// </summary> 
        protected IServiceProvider Services { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="APIMapper{TEntity, TObject}"/> class.
        /// </summary>
        /// <param name="Services">The services.</param>
        protected APIDataMapper(IServiceProvider Services)
        {
            this.Services = Services;
        }

        #endregion

        /// <summary>
        /// To the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The object.</returns>
        public abstract TObject ToObject(TEntity? entity);

        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The TEntity.</returns>
        public abstract TEntity ToEntity(TObject value);

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <returns>The TEntity.</returns>
        protected TEntity? CreateEntity()
        {
            TEntity? entity = (TEntity?)Services.GetRequiredService(typeof(TEntity));
            return entity;
        }

        /// <summary>
        /// To the objects.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The TEntity.</returns>
        public IEnumerable<TObject> ToObjects(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                yield return ToObject(entity);
            }
        }

        /// <summary>
        /// To the entities.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The colection of TEntity.</returns>
        public IEnumerable<TEntity> ToEntities(IEnumerable<TObject> items)
        {
            foreach (TObject item in items)
            {
                yield return ToEntity(item);
            }
        }
    }
}
