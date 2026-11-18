// Версия без паттерна Builder на C#
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

public class PizzaForm : Form
{
    private Button btnBase, btnSauce, btnCheese, btnPepperoni, btnReset, btnTotal, btnPreview;
    private Label lblOrder;
    private bool hasBase = false, hasSauce = false;
    private List<string> ingredients = new List<string>();
    private double cost = 0;

    public PizzaForm()
    {
        Text = "Pizza Order";
        Size = new Size(450, 350);

        btnBase = new Button { Text = "Добавить тесто", Location = new Point(10, 10), Size = new Size(200, 50) };
        btnBase.Click += (sender, e) => { AddBase(); UpdateUI(); };

        btnSauce = new Button { Text = "Добавить соус", Location = new Point(220, 10), Size = new Size(200, 50) };
        btnSauce.Click += (sender, e) => { AddSauce(); UpdateUI(); };

        btnCheese = new Button { Text = "Добавить сыр", Location = new Point(10, 70), Size = new Size(200, 50), Enabled = false };
        btnCheese.Click += (sender, e) => { AddCheese(); UpdateUI(); };

        btnPepperoni = new Button { Text = "Добавить пепперони", Location = new Point(220, 70), Size = new Size(200, 50), Enabled = false };
        btnPepperoni.Click += (sender, e) => { AddPepperoni(); UpdateUI(); };

        btnReset = new Button { Text = "Сброс", Location = new Point(10, 130), Size = new Size(200, 50) };
        btnReset.Click += (sender, e) => { ResetOrder(); UpdateUI(); };

        btnTotal = new Button { Text = "Итог", Location = new Point(220, 130), Size = new Size(200, 50) };
        btnTotal.Click += (sender, e) => MessageBox.Show(GetSummary(), "Заказ");

        lblOrder = new Label { Text = "Заказ: ", Location = new Point(10, 250), Size = new Size(400, 50) };

        Controls.AddRange(new Control[] { btnBase, btnSauce, btnCheese, btnPepperoni, btnReset, btnTotal, btnPreview, lblOrder });
    }

    private void AddBase()
    {
        if (!hasBase)
        {
            hasBase = true;
            ingredients.Add("Тесто");
            cost += 100;
        }
    }

    private void AddSauce()
    {
        if (hasBase && !hasSauce)
        {
            hasSauce = true;
            ingredients.Add("Соус");
            cost += 50;
            btnCheese.Enabled = btnPepperoni.Enabled = true;
        }
    }

    private void AddCheese()
    {
        ingredients.Add("Сыр");
        cost += 70;
    }

    private void AddPepperoni()
    {
        ingredients.Add("Пепперони");
        cost += 90;
    }

    private void ResetOrder()
    {
        hasBase = hasSauce = false;
        ingredients.Clear();
        cost = 0;
        btnCheese.Enabled = btnPepperoni.Enabled = false;
    }

    private string GetSummary()
    {
        return "Ваш заказ: " + string.Join(", ", ingredients) + $"\nОбщая стоимость: {cost} рублей";
    }

    private void UpdateUI()
    {
        lblOrder.Text = "Заказ: " + string.Join(", ", ingredients) + $"\nОбщая стоимость: {cost} рублей";
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
