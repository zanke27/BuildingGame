using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SkillList")]
public class SkillListSO : ScriptableObject
{
    public List<GameObject> list;
}
