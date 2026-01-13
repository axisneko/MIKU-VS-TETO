using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class PlayerGameplay : MonoBehaviour
{
    public float interactRange = 1000f;

    public bool isAbleToShoot = true;
    public bool isSprayAllowed = false;
    public string shootingType = "standard";
    public int damage = 8;
    public float shootingDelay = 0.1f;
    public float shootingTimer = 0.0f;
    public float reloadTime = 3.0f;
    public float reloadTimer = 0.0f;
    public int maxAmmo = 30;
    public int currAmmo = 30;
    public float explosionRadius = 9;
    public float explosionForce = 500f;

    public int itemScroll = 0;

    public InputActionAsset InputActions;
    public TextMeshProUGUI AmmoCountField;
    public TextMeshProUGUI ReloadTimerField;
    public TextMeshProUGUI CurrentWeaponField;
    public GameObject MainCameraObject;

    private InputAction m_attackAction;
    private InputAction m_reloadAction;
    private InputAction m_mouseScroll;
    private InputAction m_spawnScarecrow;

    private void Awake()
    {
        m_attackAction = InputSystem.actions.FindAction("Attack");
        m_reloadAction = InputSystem.actions.FindAction("WeaponReload");
        m_mouseScroll = InputSystem.actions.FindAction("ItemScroll");
        m_spawnScarecrow = InputSystem.actions.FindAction("SpawnScarecrow");
    }

    private void Update()
    {
        itemScroll = m_mouseScroll.ReadValue<Vector2>().y.ConvertTo<int>();

        CurrentWeaponField.text = GetComponent<PlayerInventory>().items[GetComponent<PlayerInventory>().currentSlot].name;

        if (currAmmo > 0 && reloadTimer <= 0 && isAbleToShoot && shootingTimer >= shootingDelay)
        {
            if (isSprayAllowed && m_attackAction.phase.ToString() == "Performed")
            {
                Shoot();
            }
            if (!isSprayAllowed && m_attackAction.WasPressedThisFrame())
            {
                Shoot();
            }
        }
        if (currAmmo < maxAmmo)
        {
            if (m_reloadAction.WasPressedThisFrame())
            {
                Reload();
            }
        }

        if (m_spawnScarecrow.WasPressedThisFrame())
        {
            SpawnScarecrow();
        }

        CheckItemScroll();
        UpdateShootingTimer(Time.deltaTime);
        UpdateReloadTimer(Time.deltaTime);
        UpdateUI();
    }

    void SpawnScarecrow()
    {
        RaycastHit hit;
        if (Physics.Raycast(MainCameraObject.transform.position, MainCameraObject.transform.forward, out hit, interactRange))
        {
            var tempScarecrow = Instantiate(Resources.Load<GameObject>("Prefabs/scarecrow"));
            tempScarecrow.transform.position = hit.point;
        }
    }

    void Shoot()
    {
        shootingTimer = 0;
        currAmmo -= 1;
        if (GetComponent<PlayerInventory>().items[GetComponent<PlayerInventory>().currentSlot] is WeaponItem tempWeapon)
        {
            tempWeapon.currAmmo = currAmmo;
        }


        if (shootingType == "standard")
        {
            RaycastHit hit;
            if (Physics.Raycast(MainCameraObject.transform.position, MainCameraObject.transform.forward, out hit, interactRange))
            {
                Debug.Log(hit.collider.ToString());

                if (hit.collider.tag == "hitbox_body")
                {
                    hit.collider.transform.parent.transform.parent.GetComponent<EnemyStandardMechanics>().dealDamage(damage);
                }
                if (hit.collider.tag == "hitbox_head")
                {
                    hit.collider.transform.parent.transform.parent.GetComponent<EnemyStandardMechanics>().dealDamage(2 * damage);
                }
            }
        }
        if (shootingType == "rocket")
        {
            var rocket = Instantiate(Resources.Load<GameObject>("Prefabs/RoLa Rocket"), MainCameraObject.transform.position + MainCameraObject.transform.forward*1.2f, Quaternion.Euler(0, 0, 0));
            rocket.GetComponent<Explosion>().radius = explosionRadius;
            rocket.GetComponent<Explosion>().force = explosionForce;
            rocket.transform.rotation = MainCameraObject.transform.rotation;
            rocket.SetActive(true);
            
        }
    }

    void Reload()
    {
        reloadTimer = reloadTime;
    }
    
    void UpdateShootingTimer(float value)
    {
        if (shootingTimer <= shootingDelay)
        {
            shootingTimer += value;
        }
    }

    void UpdateReloadTimer(float value)
    {
        if (reloadTimer > 0)
        {
            reloadTimer -= value;
            if (reloadTimer <= 0)
            {
                currAmmo = maxAmmo;
                ReloadTimerField.text = "";
            }
        }
    }

    void CheckItemScroll()
    {
        if (itemScroll < 0) {
            GetComponent<PlayerInventory>().changeSlotScroll(1);
        }if (itemScroll > 0) {
            GetComponent<PlayerInventory>().changeSlotScroll(-1);
        }
    }

    void UpdateUI()
    {
        AmmoCountField.text = currAmmo.ToString() + "/" + maxAmmo.ToString();
        if (reloadTimer > 0)
        {
            ReloadTimerField.text = reloadTimer.ToString();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}