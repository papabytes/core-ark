using System;
using System.Collections.Generic;

namespace CoreArk.Packages.Models
{
    public class DeleteRequestViewModel
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}