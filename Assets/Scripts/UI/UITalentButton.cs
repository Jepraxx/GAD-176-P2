using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using _Scripts.Skill_System;

[System.Serializable]
public class UITalentButton
{
    private Button _button;
    private ScriptableSkill _skill;
    private bool _isUnlocked = false;
    private UIManager _uiManager;

    public static UnityAction<ScriptableSkill> OnSkillButtonClicked;

    public UITalentButton(Button assignedButton, ScriptableSkill assignedSkill, UIManager uiManager)
    {
        _button = assignedButton;
        _skill = assignedSkill;
        _uiManager = uiManager;

        _button.clicked += OnClick;

        if (assignedSkill.SkillIcon)
        {
            _button.style.backgroundImage = new StyleBackground(assignedSkill.SkillIcon);
        }
    }

    public void OnClick()
    {
        if (_uiManager.PlayerSkillManager.PreReqMet(_skill))
        {
            OnSkillButtonClicked?.Invoke(_skill);
        }
        else
        {
            Debug.Log("Prerequisites not met for this skill.");
        }
    }
}
