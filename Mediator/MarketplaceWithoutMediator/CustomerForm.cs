
namespace MarketplaceWithoutMediator;

public class CustomerForm : Form
{
    private TextBox itemTextBox;
    private ComboBox sellerComboBox;
    private ComboBox pickupComboBox;
    private Button orderButton;
    private TextBox resultTextBox;

    // Словарь продавцов
    private Dictionary<string, ISeller> sellers;

    // Словарь способов доставки
    private Dictionary<string, IPickupMethod> pickupMethods;

    public CustomerForm()
    {
        // Создание продавцов
        sellers = new Dictionary<string, ISeller>
        {
            { "Продавец A", new SellerA() },
            { "Продавец B", new SellerB() }
        };

        // Создание способов доставки
        pickupMethods = new Dictionary<string, IPickupMethod>
        {
            { "Почтовое отделение", new PostOffice() },
            { "Курьер", new Courier() },
            { "Пункт выдачи", new PickupPoint() }
        };

        InitializeForm();
    }

    private void InitializeForm()
    {
        Text = "Маркетплейс — без Mediator";
        Size = new Size(600, 480);
        StartPosition = FormStartPosition.CenterScreen;

        // Заголовок
        Label titleLabel = new Label();
        titleLabel.Text = "Оформление заказа";
        titleLabel.Location = new Point(20, 20);
        titleLabel.Size = new Size(400, 30);
        titleLabel.Font = new Font("Arial", 16, FontStyle.Bold);

        // Поле названия товара
        Label itemLabel = new Label();
        itemLabel.Text = "Название товара:";
        itemLabel.Location = new Point(20, 80);
        itemLabel.Size = new Size(150, 25);

        itemTextBox = new TextBox();
        itemTextBox.Location = new Point(190, 80);
        itemTextBox.Size = new Size(300, 25);

        // Выбор продавца
        Label sellerLabel = new Label();
        sellerLabel.Text = "Продавец:";
        sellerLabel.Location = new Point(20, 130);
        sellerLabel.Size = new Size(150, 25);

        sellerComboBox = new ComboBox();
        sellerComboBox.Location = new Point(190, 130);
        sellerComboBox.Size = new Size(300, 25);
        sellerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        sellerComboBox.Items.AddRange(new object[]
        {
            "Продавец A",
            "Продавец B"
        });

        sellerComboBox.SelectedIndex = 0;

        // Выбор способа доставки
        Label pickupLabel = new Label();
        pickupLabel.Text = "Способ получения:";
        pickupLabel.Location = new Point(20, 180);
        pickupLabel.Size = new Size(160, 25);

        pickupComboBox = new ComboBox();
        pickupComboBox.Location = new Point(190, 180);
        pickupComboBox.Size = new Size(300, 25);
        pickupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        pickupComboBox.Items.AddRange(new object[]
        {
            "Почтовое отделение",
            "Курьер",
            "Пункт выдачи"
        });

        pickupComboBox.SelectedIndex = 0;

        // Кнопка оформления
        orderButton = new Button();
        orderButton.Text = "Оформить заказ";
        orderButton.Location = new Point(190, 230);
        orderButton.Size = new Size(180, 40);

        orderButton.Click += orderButton_Click;

        // Поле результата
        Label resultLabel = new Label();
        resultLabel.Text = "Результат оформления:";
        resultLabel.Location = new Point(20, 290);
        resultLabel.Size = new Size(250, 25);

        resultTextBox = new TextBox();
        resultTextBox.Location = new Point(20, 320);
        resultTextBox.Size = new Size(530, 100);
        resultTextBox.Multiline = true;
        resultTextBox.ReadOnly = true;
        resultTextBox.ScrollBars = ScrollBars.Vertical;

        // Добавление элементов на форму
        Controls.Add(titleLabel);
        Controls.Add(itemLabel);
        Controls.Add(itemTextBox);
        Controls.Add(sellerLabel);
        Controls.Add(sellerComboBox);
        Controls.Add(pickupLabel);
        Controls.Add(pickupComboBox);
        Controls.Add(orderButton);
        Controls.Add(resultLabel);
        Controls.Add(resultTextBox);
    }

    // Обработка нажатия кнопки
    private void orderButton_Click(object? sender, EventArgs e)
    {
        string itemName = itemTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(itemName))
        {
            MessageBox.Show(
                "Введите название товара!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        resultTextBox.Clear();

        resultTextBox.AppendText(
            "Форма самостоятельно обрабатывает заказ.\r\n");

        // Поиск продавца
        if (!sellers.TryGetValue(
            sellerComboBox.Text, out ISeller? seller))
        {
            resultTextBox.AppendText("Ошибка: продавец не найден.");
            return;
        }

        // Поиск способа доставки
        if (!pickupMethods.TryGetValue(
            pickupComboBox.Text, out IPickupMethod? method))
        {
            resultTextBox.AppendText(
                "Ошибка: способ доставки не найден.");

            return;
        }

        // Прямой вызов метода продавца
        seller.ProcessOrder(itemName);

        resultTextBox.AppendText(
            $"Продавец {sellerComboBox.Text} обрабатывает товар '{itemName}'.\r\n");

        // Прямой вызов метода доставки
        method.HandlePickup(itemName);

        resultTextBox.AppendText(
            $"Способ получения: {pickupComboBox.Text}.\r\n");

        resultTextBox.AppendText(
            "Заказ успешно оформлен!");
    }
}
