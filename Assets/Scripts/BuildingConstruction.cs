using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform buildingConstructionTransform = Instantiate(GameAssets.Instance.pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }

    private float constructionTimer;
    private float constructionTimerMax;

    private BuildingTypeSO buildingType;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        if (constructionTimer <= 0f )
        {
            Debug.Log("Ding!");
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }

        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.constructionTimerMax = buildingType.constructionTimerMax;
        this.buildingType = buildingType;

        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType.sprite;
        buildingTypeHolder.buildingType = buildingType;

        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - constructionTimer / constructionTimerMax;
    }
}
