using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerManager : MonoBehaviour, IDamageable
{
    
    [Header("Starting Weapons")]
    public Weapon[] startingWeapons;

    [Header("References")]
    public Transform gunHolder;
    public Transform PlayerLeftHandTarget;
    public Transform PlayerRightHandTarget;
    public Transform PlayerRightHand;
    public Animator animator;
    public int health = 100;

    [Header("Inventory")]
    public Transform weaponsParent;
    public Transform meleetransform;
    private List<Weapon> inventory = new List<Weapon>();

    private int activeWeaponIndex = -1;
    [HideInInspector] public Weapon currentWeapon;
    ThirdPersonShootingManager thirdPersonShootingManager;
    RigBuilder playerRig;
    PlayerIK playerik;
    Gun gun;
    public InventoryUI inventoryUI;
    private void Start()
    {
        gun = FindAnyObjectByType<Gun>();
        PlayerRightHandTarget = gunHolder;
        thirdPersonShootingManager = gameObject.GetComponent<ThirdPersonShootingManager>();
        playerik = gameObject.GetComponent<PlayerIK>();
        playerRig = gameObject.GetComponent<RigBuilder>();
        if (startingWeapons != null && startingWeapons.Length > 0)
        {
            foreach (var w in startingWeapons)
                AddWeapon(w);
            //  EquipWeapon(0);
        }
    }

    private void Update()
    {
        
        if (currentWeapon != null && Input.GetMouseButtonDown(0))
            currentWeapon.Use();

       
        if (Input.GetKeyDown(KeyCode.Tab) && inventory.Count > 0)
        {
            int next = (activeWeaponIndex + 1) % inventory.Count;
            EquipWeapon(next);
           
        }
    }

   public void AddWeapon(Weapon pickupWeapon)
{
    if (pickupWeapon == null) return;

    Weapon newWeapon = Instantiate(pickupWeapon, weaponsParent);
    newWeapon.gameObject.SetActive(false);

    var col = newWeapon.GetComponent<Collider>();
    if (col != null) col.enabled = false;

    inventory.Add(newWeapon);
        if (newWeapon is Gun gunComp)
        { 
        Sprite gunsprite = gunComp.gunsprite ;
        inventoryUI.AddImage(gunsprite);
        }
        else if (newWeapon is GrenadeWeapon grenadeComp)
        { 
        Sprite grenadeprite = grenadeComp.grenadesprite ;
        inventoryUI.AddImage(grenadeprite);
        }
        else if (newWeapon is MeleeWeapon meleecomp)
        { 
        Sprite meleesprite = meleecomp.meleesprite ;
        inventoryUI.AddImage(meleesprite);
        }
    
    if (inventory.Count == 1)
            EquipWeapon(0); 
}

    public void EquipWeapon(int index)
    {
        
        Debug.Log("Entered equpie ");
        if (index < 0 || index >= inventory.Count) return;

        thirdPersonShootingManager.enabled = true;

        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        currentWeapon = inventory[index];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.transform.SetParent(gunHolder);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        Debug.Log("Equped with " + currentWeapon.name);
        activeWeaponIndex = index;

        
        if (currentWeapon is Gun gunComp)
        {
           
            Debug.Log("Equiped with gun now ");
           
            
            SetRigLayersWeight(1f);
            if (playerRig != null)
            playerRig.Build();
            playerik.ikWeight = 1;
          
            PlayerLeftHandTarget = gunComp.LeftHandPosition;
            PlayerRightHandTarget = gunComp.RightHandPosition;
            gunComp.SetMagazine();

            SetWeaponLayer(WeaponType.Gun);
        }
        else if (currentWeapon is GrenadeWeapon grenadeComp)
        {
            
            Debug.Log("Equiped with grenade now ");
            thirdPersonShootingManager.enabled = false;
            playerik.ikWeight = 0;
            
             currentWeapon.transform.SetParent(PlayerRightHand.transform);
             currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
         
            SetRigLayersWeight(0f);

           // SetWeaponLayer(WeaponType.Grenade);
        }

        else if (currentWeapon is MeleeWeapon meleeWeapon )
        {
            PlayerLeftHandTarget = null;
            PlayerRightHandTarget = null;
            Debug.Log("Equiped with melee now ");
            thirdPersonShootingManager.enabled = false;
            playerik.ikWeight = 0;
            
             currentWeapon.transform.SetParent(meleetransform.transform);
             currentWeapon.transform.localPosition = Vector3.zero;
             currentWeapon.transform.localRotation = Quaternion.identity;
           
            SetRigLayersWeight(0f);

            SetWeaponLayer(WeaponType.Grenade);
        }
        else 
        {
           
            PlayerLeftHandTarget = null;
            PlayerRightHandTarget = null;
            SetWeaponLayer(WeaponType.Melee);
        }

        Debug.Log($"Equipped: {(currentWeapon.weaponData != null ? currentWeapon.weaponData.weaponName : currentWeapon.name)}");
    }

    public int GetActiveWeaponIndex()
    {
        return activeWeaponIndex;
    }

    public List<string> GetWeaponNames()
    {
        List<string> names = new List<string>();
        foreach (var weapon in inventory)
        {
            names.Add(weapon.weaponData != null ? weapon.weaponData.weaponName : weapon.name);
        }
        return names;
    }

    public void LoadWeapons(List<string> weaponNames, int activeIndex)
    {
        inventory.Clear();
        inventoryUI.ClearImages();
        
        foreach (string weaponName in weaponNames)
        {
            Weapon weaponToAdd = FindWeaponByName(weaponName);
            if (weaponToAdd != null)
            {
                AddWeapon(weaponToAdd);
            }
        }
        
        if (activeIndex >= 0 && activeIndex < inventory.Count)
        {
            EquipWeapon(activeIndex);
        }
    }

    private Weapon FindWeaponByName(string weaponName)
    {
        foreach (var weapon in startingWeapons)
        {
            if ((weapon.weaponData != null ? weapon.weaponData.weaponName : weapon.name) == weaponName)
            {
                return weapon;
            }
        }
        return null;
    }

public void TakeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log($"{gameObject.name} took {dmg} damage. HP: {health}");
        
         
    }

    private void SetWeaponLayer(WeaponType type)
    {
       
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 0);
        

        switch (type)
        {
                case WeaponType.Gun:
                animator.SetLayerWeight(0, 1);
                break;
                case WeaponType.Grenade:            
                animator.SetLayerWeight(1, 1);
                break;
            
            
        }
    }

     
    private void SetRigLayersWeight(float weight)
    {
        if (playerRig == null) return;
        
        var rigs = playerRig.GetComponentsInChildren<UnityEngine.Animations.Rigging.Rig>(true);
        foreach (var r in rigs)
        {
            
            r.weight = Mathf.Clamp01(weight);
        }
    }
}
