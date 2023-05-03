using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseTouchAttackSkill : BaseSkill
{
    public override void UseSkill()
    {
        Instantiate(GameAssets.Instance.touchAttackPar, UtilClass.GetMouseWorldPosition(), Quaternion.identity);

        Collider2D[] col2Darr = Physics2D.OverlapCircleAll(UtilClass.GetMouseWorldPosition(), 0.5f);
        foreach (Collider2D col in col2Darr)
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.HealthSystem.Damage(skillSO.damage);
            }
        }
    }
}
