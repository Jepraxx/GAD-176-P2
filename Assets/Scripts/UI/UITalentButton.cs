using UnityEngine;
using UnityEngine.UIElements; // Use UI Toolkit's Button
using _Scripts.Skill_System;
using UnityEngine.Events;


[System.Serializable]
public class UITalentButton
{
    private Button _button; // Make sure this is UIElements.Button
    private ScriptableSkill _skill;
    private bool _isUnlocked = false;

    public static UnityAction<ScriptableSkill> OnSkillButtonClicked;

    public UITalentButton(Button assignedButton, ScriptableSkill assignedSkill)
    {
        _button = assignedButton;
        _button.clicked += OnClick;
        _skill = assignedSkill;

        if (assignedSkill.SkillIcon)
        {
            _button.style.backgroundImage = new StyleBackground(assignedSkill.SkillIcon);
        }
    }

    public void OnClick()
    {
        OnSkillButtonClicked?.Invoke(_skill);
    }
}
