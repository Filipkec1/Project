using Project.Core.UnitsOfWork;
using System;

namespace Project.Core.Services
{
    /// <summary>
    /// Defines service base class.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public ServiceBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Gets or sets Unit of work.
        /// </summary>
        protected IUnitOfWork unitOfWork { get; set; }
    }
}
