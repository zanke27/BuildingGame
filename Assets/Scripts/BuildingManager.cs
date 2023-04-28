using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public event EventHandler<onActiveBuildingTypeEventArgs> onActiveBuildingTypeChanged;

    public class onActiveBuildingTypeEventArgs : EventArgs
    {
        public BuildingTypeSO buildingType;
    }


    [SerializeField] private Building hqBuilding;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;
    private Camera mainCamera;
    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        

    }
    private void Start()
    {
        mainCamera = Camera.main;

        hqBuilding.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
    }

    private void HQ_OnDied(object sender, EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
        GameOverUI.Instance.Show();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
            if (activeBuildingType != null){
                if(CanSpawnBuilding(activeBuildingType, UtilClass.GetMouseWorldPosition(),out string errorMessage)){
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionCostArray)){
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionCostArray);
                        //Instantiate(activeBuildingType.prefab, UtilClass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(UtilClass.GetMouseWorldPosition(), activeBuildingType);
                        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                    }
                    else{
                        TooltipUI.Instance.Show("�ڿ����� : " + activeBuildingType.GetConstructionCostString(), new TooltipUI.Timer { timer = 2f });
                    }
                }
                else{
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.Timer { timer = 2f });
                }
            }
        }
    }



    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        onActiveBuildingTypeChanged?.Invoke(this, new onActiveBuildingTypeEventArgs { buildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray=Physics2D.OverlapBoxAll(position+(Vector3)boxCollider2D.offset, boxCollider2D.size, 0f);

        bool isAreaClear= collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "�ǹ��� ���� �� ���� ��ġ�Դϴ�.";
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    errorMessage = "���� ������ �ǹ��� �ֺ��� �ֽ��ϴ�.";
                    return false;
                }
            }
        }

        if (buildingType.hasResourceGeneratorData)
        {
            ResourceGeneratorData resourceGeneratorData = buildingType.resourceGeneratorData;
            int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, position);

            if (nearbyResourceAmount == 0)
            {
                errorMessage = "�ֺ��� ���ҽ� ��尡 �����ϴ�.";
                return false;
            }
        }

        float maxConstructionRadius = 25;

        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                errorMessage = "";
                return true;
            }
        }
        errorMessage = "�ֺ��� �ǹ��� �־�� �մϴ�.";
        return false;
        
    }

    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
}
