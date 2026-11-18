
namespace MarketplaceWithoutMediator;

// Продавец A
public class SellerA : ISeller
{
    public void ProcessOrder(string itemName)
    {
        // Обработка заказа продавцом A
    }
}

// Продавец B
public class SellerB : ISeller
{
    public void ProcessOrder(string itemName)
    {
        // Обработка заказа продавцом B
    }
}

// Почтовое отделение
public class PostOffice : IPickupMethod
{
    public void HandlePickup(string itemName)
    {
        // Передача заказа в почтовое отделение
    }
}

// Курьер
public class Courier : IPickupMethod
{
    public void HandlePickup(string itemName)
    {
        // Доставка заказа курьером
    }
}

// Пункт выдачи
public class PickupPoint : IPickupMethod
{
    public void HandlePickup(string itemName)
    {
        // Передача заказа в пункт выдачи
    }
}
