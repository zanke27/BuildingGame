using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private InputActionMap skillInputActionMap;

    private InputAction alphaOnePressAction;
    private InputAction alphaTwoPressAction;
    private InputAction alphaThreePressAction;
    private InputAction alphaFourPressAction;

    private InputAction currentAction;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        skillInputActionMap = GetComponent<PlayerInput>().actions.FindActionMap("SkillMap");

        alphaOnePressAction = skillInputActionMap.FindAction("Skill1");
        alphaTwoPressAction = skillInputActionMap.FindAction("Skill2");
        alphaThreePressAction = skillInputActionMap.FindAction("Skill3");
        alphaFourPressAction = skillInputActionMap.FindAction("Skill4");
    }

    public void SetSkillInput(KeyCode keyCode, BaseSkill skill)
    {
        switch(keyCode)
        {
            case KeyCode.Alpha1:
                currentAction = alphaOnePressAction;
                break;
            case KeyCode.Alpha2:
                currentAction = alphaTwoPressAction;
                break;
            case KeyCode.Alpha3:
                currentAction = alphaThreePressAction;
                break;
            case KeyCode.Alpha4:
                currentAction = alphaFourPressAction;
                break;
        }
        currentAction.performed += _ => skill.CanUseSkill();
    }
}
