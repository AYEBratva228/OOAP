using System;
using System.Windows.Forms;

namespace CannabisPlantNoP
{
    public partial class Form1 : Form
    {
        private Greenhouse greenhouse = new Greenhouse();
        private Random rand = new Random();

        TextBox strainBox;
        TextBox colorBox;
        NumericUpDown thcBox;
        Button plantButton;
        ListBox plantList;

        public Form1()
        {
            InitializeComponent();

            this.Text = "🌿 Flyweight";
            this.Width = 600;
            this.Height = 400;

            strainBox = new TextBox { Left = 10, Top = 10, Width = 100, Text = "Сорт" };
            colorBox = new TextBox { Left = 120, Top = 10, Width = 100, Text = "Цвет" };
            thcBox = new NumericUpDown { Left = 230, Top = 10, Width = 60, DecimalPlaces = 1, Minimum = 0, Maximum = 100, Value = 18 };
            plantButton = new Button { Left = 300, Top = 10, Width = 100, Text = "Посадить" };
            plantList = new ListBox { Left = 10, Top = 50, Width = 390, Height = 200 };

            plantButton.Click += PlantButton_Click;

            Controls.Add(strainBox);
            Controls.Add(colorBox);
            Controls.Add(thcBox);
            Controls.Add(plantButton);
            Controls.Add(plantList);
        }

        private void PlantButton_Click(object sender, EventArgs e)
        {
            string strain = strainBox.Text.Trim();
            string color = colorBox.Text.Trim();
            float thc = (float)thcBox.Value;

            if (string.IsNullOrEmpty(strain) || string.IsNullOrEmpty(color))
            {
                MessageBox.Show("Введите сорт и цвет!");
                return;
            }

            int x = rand.Next(0, 100);
            int y = rand.Next(0, 100);
            greenhouse.Plant(x, y, strain, color, thc);

            plantList.Items.Clear();
            foreach (var info in greenhouse.GetPlantInfo())
            {
                plantList.Items.Add(info);
            }
        }
    }
}
