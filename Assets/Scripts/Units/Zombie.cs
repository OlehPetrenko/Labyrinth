namespace Assets.Scripts.Units
{
    /// <summary>
    /// Provides logic for a Zombie.
    /// </summary>
    public sealed class Zombie : MovableEnemy
    {
        protected override void Awake()
        {
            base.Awake();

            Speed = 1.5f;
        }
    }
}
