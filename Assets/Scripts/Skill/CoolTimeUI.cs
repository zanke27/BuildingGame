using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    private Image coolTimePanel;
    private CoolDown coolDown;

    private float maxCoolTime;
    private float nowCoolTime;

    private void Awake()
    {
        coolTimePanel = GetComponent<Image>();
        coolDown = GetComponentInParent<CoolDown>();
        coolTimePanel.fillAmount = 0;
    }

    private void Update()
    {
        if (coolDown.IsCoolDown == true)
        {
            maxCoolTime = coolDown.coolTime;
            nowCoolTime = coolDown.CheckTime;
            coolTimePanel.fillAmount = nowCoolTime / maxCoolTime;
        }
        else
            coolTimePanel.fillAmount = 0;
    }
}
