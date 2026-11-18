using System.Collections.Generic;

namespace CannabisPlantNoP
{
    public class Greenhouse
    {
        private List<CannabisPlant> plants = new List<CannabisPlant>();

        public void Plant(int x, int y, string strain, string color, float thc)
        {
            plants.Add(new CannabisPlant(x, y, strain, color, thc));
        }

        public IEnumerable<string> GetPlantInfo()
        {
            foreach (var plant in plants)
            {
                yield return plant.Describe();
            }
        }
    }
}