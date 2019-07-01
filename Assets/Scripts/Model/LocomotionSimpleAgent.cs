using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Model
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class LocomotionSimpleAgent : MonoBehaviour
    {
        private Animator anim;
        private NavMeshAgent agent;
        private Vector3 smoothDeltaPosition = Vector3.zero;
        private Vector3 velocity = Vector3.zero;

        private void Start()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            // Don’t update position automatically
            agent.updatePosition = false;
        }

        private void Update()
        {
            if (agent.enabled == false)
            {
                return;
            }

            Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

            // Map 'worldDeltaPosition' to local space
            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.up, worldDeltaPosition);
            float dz = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector3 deltaPosition = new Vector3(dx, dy, dz);

            // Low-pass filter the deltaMove
            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            smoothDeltaPosition = Vector3.Lerp(smoothDeltaPosition, deltaPosition, smooth);

            bool shouldJump = false;
            bool shouldMove = false;

            string linkType = GetComponent<NavMeshAgent>().currentOffMeshLinkData.linkType.ToString();

            if (linkType == "LinkTypeJumpAcross")
            {
                shouldJump = true;
                shouldMove = false;
            }
            else
            {
                // Update velocity if time advances
                if (Time.deltaTime > 1e-5f)
                {
                    velocity = smoothDeltaPosition / Time.deltaTime;

                    shouldJump = false;
                    shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;
                }
            }


            // Update animation parameters
            anim.SetBool("move", shouldMove);
            anim.SetBool("jump", shouldJump);
            anim.SetFloat("velx", velocity.x);
            anim.SetFloat("vely", velocity.y);
            anim.SetFloat("velz", velocity.z);

            // GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
        }

        private void OnAnimatorMove()
        {
            // Update position to agent position
            transform.position = agent.nextPosition;
        }
    }
}