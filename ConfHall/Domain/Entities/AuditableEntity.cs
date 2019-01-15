namespace ConfHall.Domain.Entities
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    public class AuditableEntity<Tkey> : BaseEntity<Tkey>
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public AuditableEntity() { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the date on which object was created.
        /// </summary>
        /// <value>The creation date.</value>
        public virtual DateTime CreatedAt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public virtual string CreatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>
        /// The updated at.
        /// </value>
        public virtual DateTime UpdatedAt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public virtual string UpdatedBy
        {
            get;
            set;
        }

        #endregion
    }
}
