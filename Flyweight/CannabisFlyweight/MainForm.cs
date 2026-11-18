using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CannabisFlyweightGUI
{
    public partial class MainForm : Form
    {
        private Greenhouse greenhouse = new Greenhouse();
        private Button plantButton;
        private ListBox plantList;
        private TextBox strainBox;
        private TextBox colorBox;
        private NumericUpDown thcBox;

        public MainForm()
        {
            InitializeComponent();



            this.Text = "🌿 Flyweight";
            this.Width = 600;
            this.Height = 400;

            plantButton = new Button();
            plantButton.Text = "Посадить растение";
            plantButton.Top = 10;
            plantButton.Left = 10;
            plantButton.Width = 150;
            plantButton.Click += PlantButton_Click;

            plantList = new ListBox();
            plantList.Top = 50;
            plantList.Left = 10;
            plantList.Width = 550;
            plantList.Height = 300;



            this.Controls.Add(plantButton);
            this.Controls.Add(plantList);

            strainBox = new TextBox { Left = 180, Top = 10, Width = 100, Text = "Сорт" };
            colorBox = new TextBox { Left = 290, Top = 10, Width = 100, Text = "Цвет" };
            thcBox = new NumericUpDown { Left = 400, Top = 10, Width = 60, DecimalPlaces = 1, Minimum = 0, Maximum = 100, Value = 18 };

            this.Controls.Add(strainBox);
            this.Controls.Add(colorBox);
            this.Controls.Add(thcBox);
        }

        private void PlantButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int x = rand.Next(100);
            int y = rand.Next(100);
            string strain = strainBox.Text.Trim();
            string color = colorBox.Text.Trim();
            float thc = (float)thcBox.Value;

            if (string.IsNullOrEmpty(strain) || string.IsNullOrEmpty(color))
            {
                MessageBox.Show("Введите сорт и цвет!");
                return;
            }

            greenhouse.Plant(x, y, strain, color, thc);
            UpdateList();
        }

        private void UpdateList()
        {
            plantList.Items.Clear();
            foreach (var desc in greenhouse.GetPlantInfo())
            {
                plantList.Items.Add(desc);
            }
        }
    }
}