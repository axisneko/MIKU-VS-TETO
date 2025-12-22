using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<string> itemsStructure = new List<string>();

    int currentSlot = 0;
    private void Awake()
    {
        getItem("ak47");
        equipWeapon();
        Invoke("testfunction", 2f);
    }

    void equipWeapon()
    {
        if (items[currentSlot] is StandardWeapon equippingStandardWeapon)
        {
            GetComponent<PlayerGameplay>().isAbleToShoot = false;
            GetComponent<PlayerGameplay>().isSprayAllowed = equippingStandardWeapon.isSprayAllowed;
            GetComponent<PlayerGameplay>().shootingType = "standard";
            GetComponent<PlayerGameplay>().shootingDelay = equippingStandardWeapon.shootingDelay;
            GetComponent<PlayerGameplay>().shootingTimer = 0f;
            GetComponent<PlayerGameplay>().reloadTime = equippingStandardWeapon.reloadTime;
            GetComponent<PlayerGameplay>().reloadTimer = 0.0f;
            GetComponent<PlayerGameplay>().maxAmmo = equippingStandardWeapon.maxAmmo;
            GetComponent<PlayerGameplay>().currAmmo = equippingStandardWeapon.maxAmmo;
            Invoke("LetGunShoot", 2f);
        }
        if (items[currentSlot] is RocketWeapon equippingRocketWeapon)
        {
            GetComponent<PlayerGameplay>().isAbleToShoot = false;
            GetComponent<PlayerGameplay>().isSprayAllowed = equippingRocketWeapon.isSprayAllowed;
            GetComponent<PlayerGameplay>().shootingType = "rocket";
            GetComponent<PlayerGameplay>().shootingDelay = equippingRocketWeapon.shootingDelay;
            GetComponent<PlayerGameplay>().shootingTimer = 0f;
            GetComponent<PlayerGameplay>().reloadTime = equippingRocketWeapon.reloadTime;
            GetComponent<PlayerGameplay>().reloadTimer = 0.0f;
            GetComponent<PlayerGameplay>().maxAmmo = equippingRocketWeapon.maxAmmo;
            GetComponent<PlayerGameplay>().currAmmo = equippingRocketWeapon.maxAmmo;
            GetComponent<PlayerGameplay>().explosionRadius = equippingRocketWeapon.explosionRadius;
            GetComponent<PlayerGameplay>().explosionForce = equippingRocketWeapon.explosionForce;
            Invoke("LetGunShoot", 2f);
        }
    }

    void getItem(string name) 
    {
        if (name == "ak47") 
        {
            StandardWeapon temp = new StandardWeapon(
                _name: "AK-47",
                _icon: "",
                _slot: 1,
                _isSprayAllowed: true,
                _isAbleToAim: false,
                _shootingDelay: 0.3f,
                _reloadTime: 2f, 
                _maxAmmo: 30
                );
            items.Add(temp);
        }
        if (name == "RoLa")
        {
            RocketWeapon temp = new RocketWeapon(
                _name: "Rocket Launcher",
                _icon: "",
                _slot: 2,
                _isSprayAllowed: false,
                _isAbleToAim: false,
                _shootingDelay: 0.3f,
                _reloadTime: 5f,
                _maxAmmo: 1,
                _explosionRadius: 15,
                _explosionForce: 700
                );
            items.Add(temp);
        }
    }

    void LetGunShoot()
    {
        GetComponent<PlayerGameplay>().isAbleToShoot = true;
    }

    void testfunction()
    {
        getItem("RoLa");
        currentSlot = 1;
        equipWeapon();
    }
}
