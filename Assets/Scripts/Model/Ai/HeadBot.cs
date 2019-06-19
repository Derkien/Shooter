using Shooter.Helper;
using Shooter.Interface;
using System;

namespace Shooter.Model.Ai
{
    public class HeadBot : BaseObjectScene, ISetDamage
    {
        public event Action<InfoCollision> OnApplyDamageChange;
        public void SetDamage(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(new InfoCollision(info.Damage * 500, info.Contact, info.ObjCollision, info.Dir));
        }
    }
}