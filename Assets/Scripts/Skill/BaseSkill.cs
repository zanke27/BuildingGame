using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BaseSkill : MonoBehaviour
{
    public SkillSO skillSO;
    
    private GameObject unRockObject;
    private Image image;

    [HideInInspector]
    public CoolDown coolDownManager;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        coolDownManager = GetComponent<CoolDown>();
        unRockObject = transform.Find("unRock").gameObject;
        image = transform.Find("image").GetComponent<Image>();

        image.sprite = skillSO.sprite;
        skillSO.unRock = false;
    }

    // UseSkill만 override하고base.UseSkill사용해서 만들자
    public virtual void CanUseSkill()
    {
        if (skillSO.unRock == false) return;
        if (coolDownManager.IsCoolDown == true) return;

        coolDownManager.CoolDownStart(skillSO);

        UseSkill();
    }

    public virtual void UseSkill()
    {

    }

    public virtual void Rock()
    {
        skillSO.unRock = false;
        unRockObject.SetActive(true);
    }

    public virtual void UnRock()
    {
        if (skillSO.unRock == true) return;
        skillSO.unRock = true;
        unRockObject.SetActive(false);
    }

    public bool CoolDownCheck()
    {
        return coolDownManager.IsCoolDown; 
    }
}
