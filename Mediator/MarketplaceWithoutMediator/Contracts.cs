
namespace MarketplaceWithoutMediator;

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
