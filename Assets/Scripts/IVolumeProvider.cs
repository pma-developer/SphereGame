namespace SphereGame
{
    public interface IVolumeProvider
    {
        // TODO: replace this with GetRadius()
        public float GetVolume();
        public void IncreaseVolume(float volume);
    }
}