using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    private SkillListSO skillList;
    private Dictionary<SkillSO, Transform> btnTransformDic;

    private void Awake()
    {
        skillList = Resources.Load<SkillListSO>(typeof(SkillListSO).Name);
        btnTransformDic = new Dictionary<SkillSO, Transform>();

        int index = 0;

        float offsetAmount = +130f;

        MouseEnterExitEvents mouseEnterExitEvents;

        foreach (GameObject skill in skillList.list)
        {
            Transform btnTransform = Instantiate(skill.transform, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            BaseSkill baseSkill = btnTransform.GetComponent<BaseSkill>();
            SkillSO skillSO = baseSkill.skillSO;

            mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(skillSO.skillName + "\n" + skillSO.description + "\n" + "»ç¿ë: " + skillSO.keyName); //+ buildingType.GetConstructionCostString());
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            btnTransformDic[skillSO] = btnTransform;
            index++;

            InputManager.Instance.SetSkillInput(skillSO.keyCode, baseSkill);
        }
    }

    private void Start()
    {
        EnemyWaveManager.Instance.OnWaveNumberChanged += UpdateSkillUnRock;
    }

    private void UpdateSkillUnRock(object sender, EventArgs e)
    {
        foreach (SkillSO skillSO in btnTransformDic.Keys)
        {
            if (EnemyWaveManager.Instance.GetWaveNum() >= skillSO.unRockWave)
            {
                Transform btnTransform = btnTransformDic[skillSO];
                btnTransform.GetComponent<BaseSkill>().UnRock();
            }
        }
    }
}
