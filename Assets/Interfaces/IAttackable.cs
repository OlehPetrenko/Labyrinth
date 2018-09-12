using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.Interfaces
{
    public interface IAttackable
    {
        void Attack(Unit targetUnit);
    }
}
