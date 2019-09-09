using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDBProviderPlatform
    {
        IDBProvider Connection();
    }
}
