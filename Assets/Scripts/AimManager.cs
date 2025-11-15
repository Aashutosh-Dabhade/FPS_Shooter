using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimManager : MonoBehaviour
{
    public Ray aimRay;
    private Vector2 viewportCenter = new Vector2(0.5f, 0.5f);
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask = new LayerMask();

    Transform aimedGun;          
    public Transform GunTarget;
    public Vector3 aimTargetPosition;   
    public float aimRange = 900f;       
    public PlayerManager player;
    public GunData gunData;

    void Start()
    {

    }
    void Update()
    {
        
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(viewportCenter);
        aimRay = Camera.main.ScreenPointToRay(screenCenter);


        
        aimTargetPosition = aimRay.origin + aimRay.direction * aimRange;
        if (Physics.Raycast(aimRay, out hit, aimRange, layerMask))
        {
            aimTargetPosition = hit.point;
            GunTarget.transform.position = hit.point;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ICollectible collectible = hit.collider.GetComponent<ICollectible>();
                if (collectible != null)
                {
                    collectible.Collect(player.GetComponent<PlayerInventory>());
                }
            }
        }


        // if (Physics.Raycast(aimRay, out hit, aimRange, layerMask))
        // {
        //     aimTargetPosition = hit.point;
        //     GunTarget.transform.position = hit.point;

        //     if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         ICollectible collectible = hit.collider.GetComponent<ICollectible>();
        //         if (collectible != null)
        //         {
        //             collectible.Collect(player.GetComponent<PlayerInventory>());
        //         }
        //     }
        // }
        else
        {
            aimedGun = null;
            gunData.Aiming = false;
        }
    }


}
