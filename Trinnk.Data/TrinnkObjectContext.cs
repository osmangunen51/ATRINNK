using Trinnk.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace Trinnk.Data
{

    /// <summary>
    /// Object context
    /// </summary>
    public class TrinnkObjectContext : DbContext, IDbContext
    {
        #region Ctor

        public TrinnkObjectContext()
            : base("TrinnkDatabaseConnString")
        {
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = false;
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
        }

        public TrinnkObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = false;
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = false;

        }

        public TrinnkObjectContext(bool lazyLoadingEnabled, bool proxyCreationEnabled, bool autoDetectChangesEnabled, bool validateOnSaveEnabled)
            : base("TrinnkDatabaseConnString")
        {
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = lazyLoadingEnabled;
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = proxyCreationEnabled;
            this.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
            this.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            this.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            this.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;

        }

        public TrinnkObjectContext(string nameOrConnectionString, bool lazyLoadingEnabled, bool proxyCreationEnabled,
            bool autoDetectChangesEnabled, bool validateOnSaveEnabled)
            : base(nameOrConnectionString)
        {

            this.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
            this.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            this.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            this.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
        }

        #endregion

        #region Utilities

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //dynamically load all configuration
            //System.Type configType = typeof(LanguageMap);   //any of your configuration classes here
            //var typesToRegister = Assembly.GetAssembly(configType).GetTypes()

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
            type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            //...or do it manually below. For example,
            //modelBuilder.Configurations.Add(new LanguageMap());

            modelBuilder.HasDefaultSchema("dbo");

            base.OnModelCreating(modelBuilder);
        }


        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context

            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }
            else
            {
                //entity is already loaded.
                return alreadyAttached;
            }
        }

        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        public virtual TEntity AttachEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            int keyMemberValue = GetKeyPropertyValue(entity);
            var defaultEntity = Set<TEntity>().Find(keyMemberValue);

            bool changedProperties = SetChangedPropertyVales(defaultEntity, entity);
            if (!changedProperties)
            {
                Set<TEntity>().Attach(entity);
                return entity;
            }
            else
            {
                Set<TEntity>().Attach(defaultEntity);
                return defaultEntity;
            }
        }

        private int GetKeyPropertyValue<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
            ObjectSet<TEntity> customEntity = objectContext.CreateObjectSet<TEntity>();
            var keyMembers = customEntity.EntitySet.ElementType.KeyMembers;
            var keyMemberName = keyMembers[0].Name;

            int keyMemberValue = Convert.ToInt32(entity.GetType().GetProperty(keyMemberName).GetValue(entity, null));
            return keyMemberValue;
        }

        private bool SetChangedPropertyVales(BaseEntity defaultEntity, BaseEntity changeEntity)
        {
            bool result = false;
            var changedProperties = GetChangedProperties(changeEntity, defaultEntity);
            foreach (var changedProperty in changedProperties)
            {
                PropertyInfo changeEntityProperty = changeEntity.GetType().GetProperty(changedProperty);
                var changeEntityPropertyValue = changeEntityProperty.GetValue(changeEntity, null);

                PropertyInfo defaultEntityProperty = defaultEntity.GetType().GetProperty(changedProperty);
                defaultEntityProperty.SetValue(defaultEntity, changeEntityPropertyValue);

                result = true;
            }
            return result;
        }

        private List<string> GetChangedProperties(BaseEntity defaultEntity, BaseEntity changeEntity)
        {
            //var defaultEntityProperties = defaultEntity.GetType().GetProperties()
            //                              .Where(p => p.PropertyType.IsValueType).ToArray();

            //var changeEntityProperties = changeEntity.GetType().GetProperties()
            //                              .Where(p => p.PropertyType.IsValueType).ToArray();

            var defaultEntityProperties = defaultEntity.GetType().GetProperties();

            var changeEntityProperties = changeEntity.GetType().GetProperties();

            List<string> changedProperties = ElaborateChangedProperties(defaultEntityProperties, changeEntityProperties, defaultEntity, changeEntity);
            return changedProperties;
        }

        private List<string> ElaborateChangedProperties(PropertyInfo[] defaultEntityProperty, PropertyInfo[] changeEntityProperty,
                                                         BaseEntity defaultEntity, BaseEntity changeEntity)
        {
            List<string> changedProperties = new List<string>();
            foreach (PropertyInfo info in defaultEntityProperty)
            {
                object propValueA = info.GetValue(defaultEntity, null);
                object propValueB = info.GetValue(changeEntity, null);
                if (!object.Equals(propValueA, propValueB))
                {
                    changedProperties.Add(info.Name);
                }
            }
            return changedProperties;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create database script
        /// </summary>
        /// <returns>SQL to generate database</returns>
        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return base.Entry(entity);
        }

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            bool acd = this.Configuration.AutoDetectChangesEnabled;
            try
            {
                this.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                this.Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            //var transactionalBehavior = doNotEnsureTransaction
            //    ? TransactionalBehavior.DoNotEnsureTransaction
            //    : TransactionalBehavior.EnsureTransaction;

            var result = this.Database.ExecuteSqlCommand(sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }




        private DbCommand GetCommand(string commandText, CommandType commandType, params object[] parameters)
        {
            var connection = this.Database.Connection;
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");
                    command.Parameters.Add(p);

                }
            }
            return command;
        }

        public IDataReader ExecuteDataReader(string commandText, CommandType commandType, CommandBehavior commandBehavior, params object[] parameters)
        {
            var command = GetCommand(commandText, commandType, parameters);
            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
            IDataReader reader = command.ExecuteReader(commandBehavior);
            return reader;
        }


        public virtual DataTable ExecuteDataTable(string commandText, CommandType commandType, params object[] parameters)
        {
            var command = GetCommand(commandText, commandType, parameters);
            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var result = new DataTable();
            result.Load(reader);
            if (!reader.IsClosed)
                reader.Close();
            if (command.Connection.State == ConnectionState.Open)
                command.Connection.Close();
            return result;
        }

        #endregion
    }
}
