using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces
{
    public interface IMovable
    {
        float Speed { get; set; }
        void Move();
    }
}
