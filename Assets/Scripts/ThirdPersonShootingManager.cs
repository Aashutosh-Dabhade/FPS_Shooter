using UnityEngine;
using Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine.UI;
public class ThirdPersonShootingManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera aimcamera;
    StarterAssets.StarterAssetsInputs starterAssetsInputs;
    StarterAssets.ThirdPersonController thirdPersonController;
    Animator animator;
    public Image crosshair;
   public GunData gunData;
    
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.aim)
        {
            aimcamera.gameObject.SetActive(true);
            thirdPersonController.CameraSensitivity = 0.5f;
          //  animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
           // animator.SetBool(gunData.shootAnimation, true);
            crosshair.gameObject.transform.localScale = Vector3.Lerp(crosshair.gameObject.transform.localScale, new Vector3(5f, 5f, 5f), Time.deltaTime * 10f);

        }
        else
        {

            aimcamera.gameObject.SetActive(false);
            thirdPersonController.CameraSensitivity = 5.0f;
           // animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            crosshair.gameObject.transform.localScale = Vector3.Lerp(crosshair.gameObject.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);


        }

        
    }
}
