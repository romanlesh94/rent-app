using HouseApi.Models;
using HouseApi.Models.Options;
using System.Linq;

namespace HouseApi.Helpers
{
    public static class SearchHelper
    {
        public static IQueryable<House> BuildSearchQuery(IQueryable<House> query,
            HouseSearchOptions houseSearchOptions)
        {
            if (houseSearchOptions == null)
            {
                return query;   
            }

            var newQuery = query;

            if (!string.IsNullOrEmpty(houseSearchOptions.City))
            {
                houseSearchOptions.City = houseSearchOptions.City.Trim();

                newQuery = newQuery.Where(x => x.Address.Contains(houseSearchOptions.City));
            }

            return newQuery;
        }
    }
}
