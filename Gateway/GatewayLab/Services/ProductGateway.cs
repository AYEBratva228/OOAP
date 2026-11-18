
using System.Collections.Generic;
using System.Linq;
using GatewayLab.Models;

namespace GatewayLab.Services
{
    public class ProductGateway
    {
        private readonly List<Product> products =
            new List<Product>();

        private int nextId = 1;

        // Получение всех товаров
        public List<Product> GetAll()
        {
            return products.ToList();
        }

        // Добавление товара
        public void Add(Product product)
        {
            product.Id = nextId++;
            products.Add(product);
        }

        // Удаление товара
        public void Delete(int id)
        {
            Product product = products
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                products.Remove(product);
            }
        }
        ы
        // Изменение количества товара
        public void UpdateQuantity(int id, int quantity)
        {
            Product product = products
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                product.Quantity = quantity;
            }
        }
    }
}
