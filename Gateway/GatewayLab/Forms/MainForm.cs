
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GatewayLab.Models;
using GatewayLab.Services;

namespace GatewayLab.Forms
{
    public class MainForm : Form
    {
        // Вариант БЕЗ Gateway:
        // форма самостоятельно хранит список товаров
        private readonly List<Product> productsWithoutGateway =
            new List<Product>();

        private int nextId = 1;

        // Вариант С Gateway:
        // форма обращается к отдельному объекту
        private readonly ProductGateway gateway =
            new ProductGateway();

        private DataGridView gridWithout;
        private DataGridView gridWith;

        private TextBox nameWithout;
        private TextBox priceWithout;
        private TextBox quantityWithout;

        private TextBox nameWith;
        private TextBox priceWith;
        private TextBox quantityWith;

        private TextBox updateWithout;
        private TextBox updateWith;

        public MainForm()
        {
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(800, 500);

            InitializeData();
            InitializeInterface();
        }

        // Начальные данные
        private void InitializeData()
        {
            // Заполняем список без Gateway
            productsWithoutGateway.Add(new Product
            {
                Id = nextId++,
                Name = "Ноутбук",
                Price = 50000,
                Quantity = 5
            });

            productsWithoutGateway.Add(new Product
            {
                Id = nextId++,
                Name = "Мышь",
                Price = 1200,
                Quantity = 15
            });

            // Заполняем данные через Gateway
            gateway.Add(new Product
            {
                Name = "Ноутбук",
                Price = 50000,
                Quantity = 5
            });

            gateway.Add(new Product
            {
                Name = "Мышь",
                Price = 1200,
                Quantity = 15
            });
        }

        // Создание интерфейса
        private void InitializeInterface()
        {
            TabControl tabs = new TabControl
            {
                Dock = DockStyle.Fill
            };

            TabPage withoutTab = new TabPage("Без Gateway");
            TabPage withTab = new TabPage("С Gateway");

            tabs.TabPages.Add(withoutTab);
            tabs.TabPages.Add(withTab);

            Controls.Add(tabs);

            CreateInventoryTab(withoutTab, false);
            CreateInventoryTab(withTab, true);
        }

        // Создание содержимого вкладки
        private void CreateInventoryTab(TabPage tab, bool useGateway)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(15)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 45));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 130));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100));

            tab.Controls.Add(layout);

            Label title = new Label
            {
                Text = useGateway
                    ? "Управление товарами через Gateway"
                    : "Управление товарами без Gateway",
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            layout.Controls.Add(title, 0, 0);

            // Таблица товаров
            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = true
            };

            layout.Controls.Add(grid, 0, 1);

            // Поля добавления
            FlowLayoutPanel addPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5)
            };

            TextBox nameBox = CreateTextBox("Название");
            TextBox priceBox = CreateTextBox("Цена");
            TextBox quantityBox = CreateTextBox("Количество");

            Button addButton = new Button
            {
                Text = "Добавить товар",
                Width = 130,
                Height = 30,
                Margin = new Padding(10)
            };

            addPanel.Controls.Add(CreateField(
                "Название", nameBox));

            addPanel.Controls.Add(CreateField(
                "Цена", priceBox));

            addPanel.Controls.Add(CreateField(
                "Количество", quantityBox));

            addPanel.Controls.Add(addButton);

            layout.Controls.Add(addPanel, 0, 2);

            // Панель удаления и изменения количества
            FlowLayoutPanel actionPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5)
            };

            Button deleteButton = new Button
            {
                Text = "Удалить выбранный товар",
                Width = 190,
                Height = 30,
                Margin = new Padding(10)
            };

            TextBox updateBox = CreateTextBox(
                "Новое количество");

            Button updateButton = new Button
            {
                Text = "Изменить количество",
                Width = 170,
                Height = 30,
                Margin = new Padding(10)
            };

            actionPanel.Controls.Add(deleteButton);

            actionPanel.Controls.Add(CreateField(
                "Новое количество", updateBox));

            actionPanel.Controls.Add(updateButton);

            layout.Controls.Add(actionPanel, 0, 3);

            // Сохраняем ссылки на элементы интерфейса
            if (useGateway)
            {
                gridWith = grid;
                nameWith = nameBox;
                priceWith = priceBox;
                quantityWith = quantityBox;
                updateWith = updateBox;
            }
            else
            {
                gridWithout = grid;
                nameWithout = nameBox;
                priceWithout = priceBox;
                quantityWithout = quantityBox;
                updateWithout = updateBox;
            }

            // Обработчик добавления
            addButton.Click += (sender, e) =>
            {
                string name = nameBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) ||
                    !decimal.TryParse(priceBox.Text, out decimal price) ||
                    !int.TryParse(quantityBox.Text, out int quantity) ||
                    price < 0 || quantity < 0)
                {
                    MessageBox.Show(
                        "Введите корректные данные товара!");
                    return;
                }

                Product product = new Product
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity
                };

                if (useGateway)
                {
                    // Работа через Gateway
                    gateway.Add(product);
                }
                else
                {
                    // Работа напрямую со списком
                    product.Id = nextId++;
                    productsWithoutGateway.Add(product);
                }

                nameBox.Clear();
                priceBox.Clear();
                quantityBox.Clear();

                RefreshGrid(grid, useGateway);

                MessageBox.Show("Товар успешно добавлен!");
            };

            // Обработчик удаления
            deleteButton.Click += (sender, e) =>
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите товар!");
                    return;
                }

                int id = Convert.ToInt32(
                    grid.SelectedRows[0]
                        .Cells["Id"].Value);

                if (useGateway)
                {
                    // Удаление через Gateway
                    gateway.Delete(id);
                }
                else
                {
                    // Удаление напрямую
                    Product product = productsWithoutGateway
                        .FirstOrDefault(p => p.Id == id);

                    if (product != null)
                    {
                        productsWithoutGateway.Remove(product);
                    }
                }

                RefreshGrid(grid, useGateway);
            };

            // Обработчик изменения количества
            updateButton.Click += (sender, e) =>
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите товар!");
                    return;
                }

                if (!int.TryParse(
                    updateBox.Text, out int newQuantity) ||
                    newQuantity < 0)
                {
                    MessageBox.Show(
                        "Введите корректное количество!");
                    return;
                }

                int id = Convert.ToInt32(
                    grid.SelectedRows[0]
                        .Cells["Id"].Value);

                if (useGateway)
                {
                    // Изменение через Gateway
                    gateway.UpdateQuantity(id, newQuantity);
                }
                else
                {
                    // Изменение напрямую
                    Product product = productsWithoutGateway
                        .FirstOrDefault(p => p.Id == id);

                    if (product != null)
                    {
                        product.Quantity = newQuantity;
                    }
                }

                updateBox.Clear();

                RefreshGrid(grid, useGateway);
            };

            RefreshGrid(grid, useGateway);
        }

        // Создание текстового поля
        private TextBox CreateTextBox(string placeholder)
        {
            return new TextBox
            {
                Width = 140,
                Height = 25
            };
        }

        // Создание поля с подписью
        private Panel CreateField(string caption, TextBox textBox)
        {
            Panel panel = new Panel
            {
                Width = 155,
                Height = 75
            };

            Label label = new Label
            {
                Text = caption,
                Dock = DockStyle.Top,
                Height = 25
            };

            textBox.Location = new Point(0, 27);

            panel.Controls.Add(label);
            panel.Controls.Add(textBox);

            return panel;
        }

        // Обновление таблицы
        private void RefreshGrid(
            DataGridView grid, bool useGateway)
        {
            grid.DataSource = null;

            if (useGateway)
            {
                grid.DataSource = gateway.GetAll();
            }
            else
            {
                grid.DataSource = productsWithoutGateway.ToList();
            }

            if (grid.Columns["Id"] != null)
                grid.Columns["Id"].HeaderText = "ID";

            if (grid.Columns["Name"] != null)
                grid.Columns["Name"].HeaderText = "Название";

            if (grid.Columns["Price"] != null)
                grid.Columns["Price"].HeaderText = "Цена";

            if (grid.Columns["Quantity"] != null)
                grid.Columns["Quantity"].HeaderText = "Количество";
        }
    }
}
