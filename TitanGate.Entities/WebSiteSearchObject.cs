using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TitanGate.WebSiteStore.Entities
{
    public class WebSiteSearchObject
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public Expression SortExpression { get; set; }
    }
}
