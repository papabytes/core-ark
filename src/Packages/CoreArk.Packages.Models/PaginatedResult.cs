using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;

namespace CoreArk.Packages.Models
{
    public class PaginatedResult<TModel> 
    {
        /// <summary>
        /// Page being displayed
        /// </summary>
        public int Page { get; set; }
        
        /// <summary>
        /// Items displayed per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page content
        /// </summary>
        public IEnumerable<TModel> Data { get; set; }

        /// <summary>
        /// Number of pages available
        /// </summary>
        public int PageCount { get; set; }
    }
}