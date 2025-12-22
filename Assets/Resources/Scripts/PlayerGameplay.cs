using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerGameplay : MonoBehaviour
{
    public float interactRange = 1000f;

    public bool isAbleToShoot = true;
    public bool isSprayAllowed = false;
    public string shootingType = "standard";
    public float shootingDelay = 0.1f;
    public float shootingTimer = 0.0f;
    public float reloadTime = 3.0f;
    public float reloadTimer = 0.0f;
    public int maxAmmo = 30;
    public int currAmmo = 30;
    public float explosionRadius = 9;
    public float explosionForce = 500f;

    public InputActionAsset InputActions;
    public TextMeshProUGUI AmmoCountField;
    public TextMeshProUGUI ReloadTimerField;
    public GameObject MainCameraObject;
    public GameObject ExplosionObject;

    private InputAction m_attackAction;
    private InputAction m_reloadAction;

    private void Awake()
    {
        m_attackAction = InputSystem.actions.FindAction("Attack");
        m_reloadAction = InputSystem.actions.FindAction("WeaponReload");
    }

    private void Update()
    {
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

        UpdateShootingTimer(Time.deltaTime);
        UpdateReloadTimer(Time.deltaTime);
        UpdateUI();
    }

    void Shoot()
    {
        shootingTimer = 0;
        currAmmo -= 1;
        
        if (shootingType == "standard")
        {
            RaycastHit hit;
            if (Physics.Raycast(MainCameraObject.transform.position, MainCameraObject.transform.forward, out hit, interactRange))
            {
                Debug.Log(hit.collider.ToString());
            }
        }
        if (shootingType == "rocket")
        {
            RaycastHit hit;
            if (Physics.Raycast(MainCameraObject.transform.position, MainCameraObject.transform.forward, out hit, interactRange))
            {
                ExplosionObject.GetComponent<Explosion>().radius = explosionRadius;
                ExplosionObject.GetComponent<Explosion>().force = explosionForce;
                var expl = Instantiate(ExplosionObject, hit.point, Quaternion.Euler(0, 0, 0));
                expl.SetActive(true);
            }
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