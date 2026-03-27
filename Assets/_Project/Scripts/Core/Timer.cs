namespace WarAquaDrone.Core
{
    public sealed class Timer
    {
        public float Remaining { get; private set; }
        public bool IsRunning => Remaining > 0f;

        public void Start(float seconds) => Remaining = seconds;

        public void Tick(float dt)
        {
            if (Remaining <= 0f) return;
            Remaining -= dt;
        }
    }
}
