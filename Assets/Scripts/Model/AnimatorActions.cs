using UnityEngine;

namespace Shooter.Model
{
    public class AnimatorActions : MonoBehaviour
    {
        private ParticleSystem ParticleSystem;

        private void Start()
        {
            ParticleSystem = gameObject.transform.Find("CannonMachine").Find("CannonMachine_FireBurster").Find("Smoke").GetComponent<ParticleSystem>();
        }

        public void PlayFlameParticles()
        {
            ParticleSystem.Play();
        }

        public void StopPlayFlameParticles()
        {
            ParticleSystem.Stop();
        }
    }
}