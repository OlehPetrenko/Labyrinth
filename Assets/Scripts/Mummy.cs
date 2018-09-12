namespace Assets.Scripts
{
    public class Mummy : MovableEnemy
    {
        protected override void Awake()
        {
            Speed = 3.0F;

            base.Awake();
        }
    }
}
