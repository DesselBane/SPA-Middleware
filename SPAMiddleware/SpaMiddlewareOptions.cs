using System.Collections.Generic;
using System.Linq;

namespace SPAMiddleware
{
    public sealed class SpaMiddlewareOptions
    {
        #region Properties

        public string PathToIndex { get; }

        public IEnumerable<string> SpecialRoutes { get; }
        
        #endregion

        public SpaMiddlewareOptions(string pathToIndex, IEnumerable<string> specialRoutes = null)
        {
            PathToIndex = pathToIndex;
            SpecialRoutes = specialRoutes == null ? new string[0] : specialRoutes.ToArray();
        }
    }
}