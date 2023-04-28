using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public bool hasResourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionCostArray;
    public int healthAmountMax;
    public float constructionTimerMax;

    public string GetConstructionCostString()
    {
        string str = "";
        foreach(ResourceAmount resourceAmount in constructionCostArray)
        {
            str +="<color=#"+ resourceAmount.resourceType.colorHex+">"+
                resourceAmount.resourceType.nameShort + resourceAmount.amount
                +"</color>";
        }

        return str;
    }
}
