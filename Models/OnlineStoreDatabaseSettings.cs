namespace OnlineRetailShop.Models
{
    public class OnlineStoreDatabaseSettings : IOnlineStoreDatabaseSettings
    {
        public string ProductCollectionName { get; set; } = String.Empty;
        public string OrderCollectionName { get; set; } = string.Empty;
        public string ConnectionStrings { get; set; } = String.Empty;
        public string DataBaseName { get; set; } = String.Empty;
    }
}