using Marvel.Api.Filters;

namespace marvel
{
    public static class Extensions
    {
        public static OrderResult GetOrderResult(int orderBy)
        {
            switch (orderBy)
            {
                case 0:
                    return OrderResult.NameAscending;
                case 1:
                    return OrderResult.ModifiedAscending;
                case 2:
                    return OrderResult.NameDescending;
                case 4:
                    return OrderResult.ModifiedDescending;
                default:
                    return OrderResult.NameAscending;
            }
        }
    }
}
