// Версия с паттерном Builder
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class PizzaBuilder
{
    private bool hasBase = false;
    private bool hasSauce = false;
    private List<string> ingredients = new List<string>();
    private double cost = 0;

    public void AddBase()
    {
        if (!hasBase)
        {
            hasBase = true;
            ingredients.Add("Тесто");
            cost += 100;
        }
    }

    public void AddSauce()
    {
        if (hasBase && !hasSauce)
        {
            hasSauce = true;
            ingredients.Add("Соус");
            cost += 50;
        }
    }

    public void AddCheese()
    {
        if (hasBase && hasSauce)
        {
            ingredients.Add("Сыр");
            cost += 70;
        }
    }

    public void AddPepperoni()
    {
        if (hasBase && hasSauce)
        {
            ingredients.Add("Пепперони");
            cost += 90;
        }
    }

    public void Reset()
    {
        hasBase = hasSauce = false;
        ingredients.Clear();
        cost = 0;
    }

    public string GetSummary()
    {
        return "Ваш заказ: " + string.Join(", ", ingredients) + $"\nОбщая стоимость: {cost} рублей";
    }

    public string GetPreview()
    {
        return string.Join(", ", ingredients) + $"\nСтоимость: {cost} рублей";
    }

    public double GetCost() => cost;
}

public class PizzaForm : Form
{
    private PizzaBuilder builder = new PizzaBuilder();
    private Button btnBase, btnSauce, btnCheese, btnPepperoni, btnReset, btnTotal, btnPreview;
    private Label lblOrder;

    public PizzaForm()
    {
        Text = "Pizza Builder";
        Size = new System.Drawing.Size(400, 350);

        btnBase = new Button { Text = "Добавить тесто", Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(130, 40) };
        btnBase.Click += (sender, e) => { builder.AddBase(); UpdateUI(); };

        btnSauce = new Button { Text = "Добавить соус", Location = new System.Drawing.Point(150, 10), Size = new System.Drawing.Size(130, 40) };
        btnSauce.Click += (sender, e) => { builder.AddSauce(); UpdateUI(); };

        btnCheese = new Button { Text = "Добавить сыр", Location = new System.Drawing.Point(10, 60), Size = new System.Drawing.Size(130, 40) };
        btnCheese.Click += (sender, e) => { builder.AddCheese(); UpdateUI(); };

        btnPepperoni = new Button { Text = "Добавить пепперони", Location = new System.Drawing.Point(150, 60), Size = new System.Drawing.Size(130, 40) };
        btnPepperoni.Click += (sender, e) => { builder.AddPepperoni(); UpdateUI(); };

        btnReset = new Button { Text = "Сброс", Location = new System.Drawing.Point(10, 110), Size = new System.Drawing.Size(130, 40) };
        btnReset.Click += (sender, e) => { builder.Reset(); UpdateUI(); };

        btnTotal = new Button { Text = "Итог", Location = new System.Drawing.Point(150, 110), Size = new System.Drawing.Size(130, 40) };
        btnTotal.Click += (sender, e) => { MessageBox.Show(builder.GetSummary()); builder.Reset(); UpdateUI(); };

        lblOrder = new Label { Text = "Заказ: ", Location = new System.Drawing.Point(10, 210), Size = new System.Drawing.Size(350, 100) };

        Controls.AddRange(new Control[] { btnBase, btnSauce, btnCheese, btnPepperoni, btnReset, btnTotal, btnPreview, lblOrder });
    }

    private void UpdateUI()
    {
        lblOrder.Text = $"Заказ: {builder.GetPreview()}";
    }
}

public static class Program
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new PizzaForm());
    }
}