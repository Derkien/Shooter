using Shooter.Model;
using Shooter.View;

namespace Shooter.Controller
{
    public abstract class BaseController
    {
        protected UiInterface UiInterface;
        protected BaseController()
        {
            UiInterface = new UiInterface();
        }

        public bool IsActive { get; private set; }

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(BaseObjectScene obj)
        {
            IsActive = true;
        }

        public virtual void Off()
        {
            IsActive = false;
        }

        public void Switch()
        {
            if (!IsActive)
            {
                On();
            }
            else
            {
                Off();
            }
        }
    }
}