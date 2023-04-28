using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBuildingInvincibility : BaseSkill
{
    Transform invincivilityArea;
    Transform invincivilityPar;

    public override void UseSkill()
    {
        invincivilityPar = Instantiate(GameAssets.Instance.invincivilityPar, UtilClass.GetMouseWorldPosition(), Quaternion.identity);
        invincivilityArea = Instantiate(GameAssets.Instance.pfInvincibilityArea, UtilClass.GetMouseWorldPosition(), Quaternion.identity);

        Collider2D[] col2Darr = Physics2D.OverlapCircleAll(UtilClass.GetMouseWorldPosition(), 15f);
        foreach (Collider2D col in col2Darr)
        {
            Building building = col.gameObject.GetComponent<Building>();
            if (building != null)
            {
                building.GetComponent<HealthSystem>().OnInvincibility();
            }
        }

        StartCoroutine(DestroyArea());
    }
    IEnumerator DestroyArea()
    {
        yield return new WaitForSeconds(5f);
        Destroy(invincivilityArea.gameObject);
        Destroy(invincivilityPar.gameObject);

        Collider2D[] col2Darr = Physics2D.OverlapCircleAll(invincivilityArea.position, 15f);
        foreach (Collider2D col in col2Darr)
        {
            BuildingTypeHolder holder = col.gameObject.GetComponent<BuildingTypeHolder>();
            if (holder != null)
            {
                holder.GetComponent<HealthSystem>().OffInvincibility();
            }
        }
    }


}
