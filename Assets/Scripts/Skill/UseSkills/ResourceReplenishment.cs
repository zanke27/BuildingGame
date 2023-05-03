using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceReplenishment : BaseSkill
{
    [SerializeField] private ResourceTypeSO woodResource;
    [SerializeField] private ResourceTypeSO stoneResource;
    [SerializeField] private ResourceTypeSO goldResource;

    public override void UseSkill()
    {
        ResourceManager.Instance.AddResource(woodResource, 50);
        ResourceManager.Instance.AddResource(stoneResource, 25);
        ResourceManager.Instance.AddResource(goldResource, 10);
    }

}
