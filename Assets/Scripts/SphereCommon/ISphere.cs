namespace SphereGame
{
    public interface ISphere
    {
        public float Radius { get; }
        public void IncreaseVolume(float radius);
    }
}