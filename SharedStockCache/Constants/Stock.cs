namespace SharedStockCache.Constants
{
    public static class Stock
    {
        public const string STOCK_CACHE_ITEM_KEY_PREFIX = "stock_item_";

        /// <summary>
        /// The max minutes a stock item can be reserved.
        /// </summary>
        public const int RESERVATION_DURATION = 10;
    }
}
