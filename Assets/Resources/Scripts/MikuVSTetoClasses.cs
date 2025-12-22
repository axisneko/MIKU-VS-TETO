using System.IO;
using UnityEngine;


public class Item
{
    public string itemType;
    public string name;
    public string icon;
    public int slot;
}

public class WeaponItem : Item
{
    public bool isSprayAllowed;
    public bool isAbleToAim;
    public float shootingDelay;
    public float reloadTime;
    public int maxAmmo;
}

public class StandardWeapon : WeaponItem {
    public override string ToString()
    {
        return $"StandardWeapon_{name}";
    }
    public StandardWeapon(string _name, string _icon, int _slot, bool _isSprayAllowed, bool _isAbleToAim, float _shootingDelay, float _reloadTime, int _maxAmmo)
    {
        itemType = "weapon";
        name = _name;
        icon = _icon;
        slot = _slot;

        isSprayAllowed = _isSprayAllowed;
        isAbleToAim = _isAbleToAim;
        shootingDelay = _shootingDelay;
        reloadTime = _reloadTime;
        maxAmmo = _maxAmmo;
    }
}

public class RocketWeapon : WeaponItem
{
    public float explosionRadius;
    public float explosionForce;
    public override string ToString()
    {
        return $"StandardWeapon_{name}";
    }
    public RocketWeapon(string _name, string _icon, int _slot, bool _isSprayAllowed, bool _isAbleToAim, float _shootingDelay, float _reloadTime, int _maxAmmo, float _explosionRadius, float _explosionForce)
    {
        itemType = "weapon";
        name = _name;
        icon = _icon;
        slot = _slot;

        isSprayAllowed = _isSprayAllowed;
        isAbleToAim = _isAbleToAim;
        shootingDelay = _shootingDelay;
        reloadTime = _reloadTime;
        maxAmmo = _maxAmmo;

        explosionRadius = _explosionRadius;
        explosionForce = _explosionForce;
    }
}

