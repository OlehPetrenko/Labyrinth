using Assets.Scripts;
using Assets.Scripts.Units;

namespace Assets.Interfaces
{
    /// <summary>
    /// Represents a behaviour to attack.
    /// </summary>
    public interface IAttackable
    {
        void Attack(Unit targetUnit);
    }
}
