using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyHealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthDisplayField;
    public Slider healthDisplaySlider;
    public GameObject healthDisplayCanvas;

    private Vector3 worldOffset = new Vector3(0, 2.5f, 0);
    private Vector2 screenOffset = new Vector2(0, 50);

    private RectTransform canvasRect;
    private RectTransform sliderRect;
    private RectTransform textfieldRect;
    private void Awake()
    {
        healthDisplaySlider.maxValue = GetComponent<EntityGeneralMechanics>().maxHealth;

        canvasRect = healthDisplayCanvas.GetComponent<RectTransform>();
        sliderRect = healthDisplaySlider.GetComponent<RectTransform>();
        textfieldRect = healthDisplayField.GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        Vector3 worldPosition = transform.position + worldOffset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        if (screenPosition.z < 0)
        {
            healthDisplayCanvas.SetActive(false);
            return;
        }
        healthDisplayCanvas.SetActive(true);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            new Vector2(screenPosition.x, screenPosition.y),
            Camera.main,
            out Vector2 anchoredPosition
        );

        sliderRect.anchoredPosition = anchoredPosition;
        anchoredPosition = anchoredPosition + new Vector2(0, 30);
        textfieldRect.anchoredPosition = anchoredPosition;
    }
    public void UpdateHealthDisplay()
    {
        healthDisplayField.text = Mathf.RoundToInt(GetComponent<EntityGeneralMechanics>().health).ToString() + "/" + GetComponent<EntityGeneralMechanics>().maxHealth.ToString();
        healthDisplaySlider.value = GetComponent<EntityGeneralMechanics>().health;
    }
}
