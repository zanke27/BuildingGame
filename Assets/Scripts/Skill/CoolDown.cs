using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CoolDown : MonoBehaviour
{
    [SerializeField]
    private bool isCoolDown = false;
    public bool IsCoolDown => isCoolDown;

    public float coolTime = 0;
    private float time = 0;
    public float CheckTime => time;

    private void Update()
    {
        if (isCoolDown == true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
                isCoolDown = false;
        }
    }

    public void CoolDownStart(SkillSO skillSO)
    {
        coolTime = skillSO.coolDown;
        time = coolTime;
        isCoolDown = true;
    }
}
