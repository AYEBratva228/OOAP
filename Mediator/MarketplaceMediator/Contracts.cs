
namespace MarketplaceMediator;

// Интерфейс посредника
public interface IMediator
{
    void Notify(
        object sender,
        EventArgs ev,
        string itemName,
        string sellerName,
        string pickupMethod);
}

// Интерфейс продавца
public interface ISeller
{
    void ProcessOrder(string itemName);
}

// Интерфейс способа получения заказа
public interface IPickupMethod
{
    void HandlePickup(string itemName);
}
