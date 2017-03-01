using Flogging.Web.Filters;
using System.Web.Mvc;

namespace mvc_todo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TrackPerformanceAttribute(Constants.ProductName, Constants.LayerName));
        }
    }
}
