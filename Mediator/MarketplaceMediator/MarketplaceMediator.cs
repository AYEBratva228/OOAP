
namespace MarketplaceMediator;

public class MarketplaceMediator : IMediator
{
    private readonly Dictionary<string, ISeller> sellers;

    private readonly Dictionary<string, IPickupMethod> pickupMethods;

    public string LastResult { get; private set; } = "";

    public MarketplaceMediator()
    {
        sellers = new Dictionary<string, ISeller>
        {
            { "Продавец A", new SellerA() },
            { "Продавец B", new SellerB() }
        };

        pickupMethods = new Dictionary<string, IPickupMethod>
        {
            { "Почтовое отделение", new PostOffice() },
            { "Курьер", new Courier() },
            { "Пункт выдачи", new PickupPoint() }
        };
    }

    public void Notify(
        object sender,
        EventArgs ev,
        string itemName,
        string sellerName,
        string pickupMethod)
    {
        LastResult = "Посредник получил запрос на оформление заказа.\r\n";

        // Поиск продавца
        if (!sellers.TryGetValue(sellerName, out ISeller? seller))
        {
            LastResult += "Ошибка: продавец не найден.";
            return;
        }

        // Поиск способа доставки
        if (!pickupMethods.TryGetValue(
            pickupMethod, out IPickupMethod? method))
        {
            LastResult += "Ошибка: способ доставки не найден.";
            return;
        }

        // Передача заказа продавцу
        seller.ProcessOrder(itemName);

        LastResult += $"Продавец {sellerName} обрабатывает товар '{itemName}'.\r\n";

        // Передача заказа службе доставки
        method.HandlePickup(itemName);

        LastResult += $"Способ получения: {pickupMethod}.\r\n";

        LastResult += "Заказ успешно оформлен!";
    }
}
