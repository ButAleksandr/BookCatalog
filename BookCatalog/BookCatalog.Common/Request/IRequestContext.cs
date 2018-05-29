using BookCatalog.Common.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Common.Request
{
    public interface IRequestContext: IInternalRequestContext, IDisposable
    {
    }
}
