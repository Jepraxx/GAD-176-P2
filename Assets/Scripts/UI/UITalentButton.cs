using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using _Scripts.Skill_System;

[System.Serializable]
public class UITalentButton
{
    private Button _button; // The UI button associated with this talent
    private ScriptableSkill _skill; // The skill associated with this button
    private bool _isUnlocked = false; // Tracks if the skill is unlocked
    private UIManager _uiManager; // Reference to the UIManager for skill data and interactions

    // Static event that other classes can subscribe to for when a skill button is clicked
    public static UnityAction<ScriptableSkill> OnSkillButtonClicked;

    // Constructor for UITalentButton
    public UITalentButton(Button assignedButton, ScriptableSkill assignedSkill, UIManager uiManager)
    {
        _button = assignedButton; // Assigns the button to the private field
        _skill = assignedSkill; // Assigns the skill to the private field
        _uiManager = uiManager; // Assigns the UIManager to the private field

        _button.clicked += OnClick; // Adds the OnClick method as a listener for the button click event

        // If the skill has an icon, set it as the background image for the button
        if (assignedSkill.SkillIcon)
        {
            _button.style.backgroundImage = new StyleBackground(assignedSkill.SkillIcon); // Sets the button background image
        }
    }

    // Method called when the button is clicked
    public void OnClick()
    {
        // Checks if the skill's prerequisites are met
        if (_uiManager.PlayerSkillManager.PreReqMet(_skill))
        {
            // Invokes the OnSkillButtonClicked event with the associated skill
            OnSkillButtonClicked?.Invoke(_skill);
        }
        else
        {
            // Logs a message if prerequisites are not met
            Debug.Log("Prerequisites not met for this skill.");
        }
    }
}
