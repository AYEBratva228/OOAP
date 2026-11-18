using System.Collections.Generic;

namespace CannabisFlyweightGUI
{
    public class Greenhouse
    {
        private List<CannabisPlant> plants = new List<CannabisPlant>();

        public void Plant(int x, int y, string strain, string color, float thc)
        {
            bool isNew;
            var type = CannabisFactory.GetType(strain, color, thc, out isNew);
            if (isNew)
            {
                System.Windows.Forms.MessageBox.Show($"Создан новый тип: {strain}, {color}, THC: {thc}%");
            }
            plants.Add(new CannabisPlant(x, y, type));
        }

        public IEnumerable<string> GetPlantInfo()
        {
            foreach (var plant in plants)
            {
                yield return plant.Describe();
            }
        }
    }

    public class CannabisPlant
    {
        public int X, Y;
        public CannabisType Type;

        public CannabisPlant(int x, int y, CannabisType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public string Describe()
        {
            return Type.Describe(X, Y);
        }
    }

    public class CannabisType
    {
        public string Strain;
        public string Color;
        public float THC;

        public CannabisType(string strain, string color, float thc)
        {
            Strain = strain;
            Color = color;
            THC = thc;
        }

        public string Describe(int x, int y)
        {
            return $"🌿  ({x},{y}): {Strain}, {Color}, THC: {THC}%";
        }
    }

    public static class CannabisFactory
    {
        private static Dictionary<string, CannabisType> types = new Dictionary<string, CannabisType>();

        public static CannabisType GetType(string strain, string color, float thc, out bool isNew)
        {
            string key = strain + "|" + color + "|" + thc;
            if (!types.ContainsKey(key))
            {
                types[key] = new CannabisType(strain, color, thc);
                isNew = true;
            }
            else
            {
                isNew = false;
            }
            return types[key];
        }
    }
}