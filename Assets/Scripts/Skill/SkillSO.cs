using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int damage;
    public float coolDown;
    public Sprite sprite;
    [TextArea(3, 5)]
    public string description;
    public KeyCode keyCode;
    public int unRockWave;


    public bool unRock;
}
