using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;

    private Image construtionProgressImage;

    private void Awake()
    {
        construtionProgressImage = transform.Find("Mask").Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        construtionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
