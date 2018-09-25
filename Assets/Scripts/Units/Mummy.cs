using Assets.Classes;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Provides logic for a Mummy.
    /// </summary>
    public sealed class Mummy : MovableEnemy
    {
        protected override void Awake()
        {
            base.Awake();

            Speed = 2.6f;
        }

        public override void Attack(Unit targetUnit)
        {
            GameSessionData.Instance.Score = 0;

            base.Attack(targetUnit);
        }
    }
}
