using System;
using System.Collections.Generic;
using System.Text;

namespace CameraBazaar2.Services.Interfaces
{
    /// <summary>
    /// We use this interface, so we can find the assmemlby that contains services. 
    /// And with reflection we register them in ServiceCollection.
    /// </summary>
    public interface IService
    {
    }
}
