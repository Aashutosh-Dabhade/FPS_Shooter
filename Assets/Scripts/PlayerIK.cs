using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerIK : MonoBehaviour
{
    private Animator animator;
    private PlayerManager playerManager;

    [Range(0, 1f)]
    public float ikWeight = 1f; 

    void Start()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
    }

   void OnAnimatorIK(int layerIndex)
{
    if (playerManager == null || playerManager.currentWeapon == null) return;

    if (playerManager.currentWeapon is Gun && ikWeight > 0)
    {
        if (playerManager.PlayerLeftHandTarget != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, playerManager.PlayerLeftHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, playerManager.PlayerLeftHandTarget.rotation);
        }

        if (playerManager.PlayerRightHandTarget != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, playerManager.PlayerRightHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, playerManager.PlayerRightHandTarget.rotation);
        }
    }
    else
    {
        // Reset IK if grenade or melee
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
    }
}

}


