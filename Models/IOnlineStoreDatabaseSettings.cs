namespace OnlineRetailShop.Models
{
    public interface IOnlineStoreDatabaseSettings
    {
        string ProductCollectionName { get; set; }
        string OrderCollectionName { get; set; }
        string ConnectionStrings { get; set; }
        string DataBaseName { get; set; }
    }
}