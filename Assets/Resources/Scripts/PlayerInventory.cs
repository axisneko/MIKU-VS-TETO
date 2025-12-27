using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlayerInventory : MonoBehaviour
{
    public Item[] items = new Item[5];

    public int currentSlot = 0;
    private void Awake()
    {
        getItem("ak47");
        equipWeapon();
        getItem("RoLa");
        Invoke("testfunc", 40f);
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
            GetComponent<PlayerGameplay>().currAmmo = equippingStandardWeapon.currAmmo;
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
            GetComponent<PlayerGameplay>().currAmmo = equippingRocketWeapon.currAmmo;
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
                _shootingDelay: 0.1f,
                _reloadTime: 2f, 
                _maxAmmo: 30,
                _currAmmo: 30
                );
            changeSlot(temp, temp.slot);
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
                _currAmmo: 1,
                _explosionRadius: 15,
                _explosionForce: 700
                );
            changeSlot(temp, temp.slot);
        }
        if (name == "MP5")
        {
            StandardWeapon temp = new StandardWeapon(
                _name: "MP5-SD",
                _icon: "",
                _slot: 1,
                _isSprayAllowed: true,
                _isAbleToAim: false,
                _shootingDelay: 0.05f,
                _reloadTime: 1.5f,
                _maxAmmo: 25,
                _currAmmo: 25
                );
            changeSlot(temp, temp.slot);
        }
    }

    void changeSlot(Item temp, int tempSlot)
    {
        if (tempSlot < 3)
        {
            items[tempSlot - 1] = temp;
            currentSlot = tempSlot-1;
            equipWeapon();
        }
    }

    public void changeSlotScroll(int direction)
    {
        currentSlot += direction;
        if (currentSlot > 1) {
            currentSlot = 0;
        }
        if (currentSlot < 0) {
            currentSlot = 1;
        }
        equipWeapon();
    }

    void LetGunShoot()
    {
        GetComponent<PlayerGameplay>().isAbleToShoot = true;
    }

    void testfunc()
    {
        getItem("MP5");
    }
}
