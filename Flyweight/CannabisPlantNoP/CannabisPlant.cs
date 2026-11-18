namespace CannabisPlantNoP
{
    public class CannabisPlant
    {
        public int X, Y;
        public string Strain;
        public string Color;
        public float THC;

        public CannabisPlant(int x, int y, string strain, string color, float thc)
        {
            X = x;
            Y = y;
            Strain = strain;
            Color = color;
            THC = thc;
        }

        public string Describe()
        {
            return $"({X},{Y}): {Strain}, {Color}, THC: {THC}%";
        }
    }
}
