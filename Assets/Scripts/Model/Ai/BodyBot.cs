using Shooter.Helper;
using Shooter.Interface;
using System;

namespace Shooter.Model.Ai
{
    public class BodyBot : BaseObjectScene, ISetDamage
    {
        public event Action<InfoCollision> OnApplyDamageChange;
        public void SetDamage(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(info);
        }
    }
}