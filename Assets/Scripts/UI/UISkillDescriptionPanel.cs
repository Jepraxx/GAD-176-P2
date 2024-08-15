using _Scripts.Skill_System; 
using System.Collections.Generic; 
using System.Linq; 
using UnityEngine; 
using UnityEngine.UIElements; 

public class UISkillDescriptionPanel : MonoBehaviour
{
    private UIManager _uiManager; // Reference to the UIManager, which handles skill data and UI
    private ScriptableSkill _assignedSkill; // The currently assigned skill to display
    private VisualElement _skillImage; // UI element for displaying the skill's image
    private Label _skillNameLabel, _skillDescriptionLabel, _skillCostLabel, _skillPreReqLabel; // UI labels for displaying skill details
    private Button _purchaseSkillButton; // Button for purchasing the skill

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>(); // Initializes _uiManager with the UIManager component attached to the same GameObject
    }

    private void OnEnable()
    {
        UITalentButton.OnSkillButtonClicked += PopulateLabeltext; // Subscribes to the event when a skill button is clicked
    }

    private void OnDisable()
    {
        UITalentButton.OnSkillButtonClicked -= PopulateLabeltext; // Unsubscribes from the event when a skill button is clicked
        if (_purchaseSkillButton != null)
        {
            _purchaseSkillButton.clicked -= PurchaseSkill; // Unsubscribes from the purchase button click event
        }
    }

    private void Start()
    {
        GatherLabelReferences(); // Calls method to get references to UI elements
        // Initialize with a default skill if needed
        var skill = _uiManager.SkillLibrary.GetSkillsOfTier(1).FirstOrDefault(); // Gets the first skill from tier 1
        if (skill != null)
        {
            PopulateLabeltext(skill); // Updates UI with the default skill's information
        }
    }

    private void GatherLabelReferences()
    {
        var root = _uiManager.UIDocument.rootVisualElement; // Gets the root element of the UI document
        _skillImage = root.Q<VisualElement>("Icon"); // Finds the UI element for skill icon
        _skillNameLabel = root.Q<Label>("SkillNameLabel"); // Finds the UI label for skill name
        _skillDescriptionLabel = root.Q<Label>("SkillDescriptionLabel"); // Finds the UI label for skill description
        _skillCostLabel = root.Q<Label>("SkillCostLabel"); // Finds the UI label for skill cost
        _skillPreReqLabel = root.Q<Label>("PreReqLabel"); // Finds the UI label for skill prerequisites
        _purchaseSkillButton = root.Q<Button>("BuySkillButton"); // Finds the UI button for purchasing skills

        if (_skillImage == null) Debug.LogError("SkillImage not found."); // Logs an error if the skill image element is not found
        if (_skillNameLabel == null) Debug.LogError("SkillNameLabel not found."); // Logs an error if the skill name label is not found
        if (_skillDescriptionLabel == null) Debug.LogError("SkillDescriptionLabel not found."); // Logs an error if the skill description label is not found
        if (_skillCostLabel == null) Debug.LogError("SkillCostLabel not found."); // Logs an error if the skill cost label is not found
        if (_skillPreReqLabel == null) Debug.LogError("PreReqLabel not found."); // Logs an error if the skill prerequisites label is not found
        if (_purchaseSkillButton == null) Debug.LogError("PurchaseSkillButton not found."); // Logs an error if the purchase button is not found
        else
        {
            _purchaseSkillButton.clicked += PurchaseSkill; // Subscribes to the purchase button click event
        }
    }

    private void PurchaseSkill()
    {
        if (_assignedSkill == null) return; // Exits the method if no skill is assigned

        if (_uiManager.PlayerSkillManager.CanBuySkill(_assignedSkill)) // Checks if the player can buy the assigned skill
        {
            _uiManager.PlayerSkillManager.UnlockSkill(_assignedSkill); // Unlocks the skill for the player
            PopulateLabeltext(_assignedSkill); // Updates UI with the skill's information
        }
    }

    private void PopulateLabeltext(ScriptableSkill skill)
    {
        if (skill == null)
        {
            Debug.LogError("Passed skill is null."); // Logs an error if the passed skill is null
            return;
        }

        _assignedSkill = skill; // Assigns the passed skill to _assignedSkill

        // Update the skill image
        if (_skillImage != null && _assignedSkill.SkillIcon != null)
        {
            _skillImage.style.backgroundImage = new StyleBackground(_assignedSkill.SkillIcon); // Sets the skill icon image
            _skillImage.MarkDirtyRepaint(); // Forces a repaint to ensure the image is updated
        }

        // Update the other labels
        if (_skillNameLabel != null) _skillNameLabel.text = _assignedSkill.SkillName; // Updates the skill name label
        if (_skillDescriptionLabel != null) _skillDescriptionLabel.text = _assignedSkill.SkillDescription; // Updates the skill description label
        if (_skillCostLabel != null) _skillCostLabel.text = $"Cost: {skill.Cost}"; // Updates the skill cost label

        if (_assignedSkill.SkillPrerequisites.Count > 0)
        {
            if (_skillPreReqLabel != null)
            {
                _skillPreReqLabel.text = "PreReqs: "; // Sets the label text to indicate prerequisites
                foreach (var preReq in skill.SkillPrerequisites)
                {
                    var lastIndex = _assignedSkill.SkillPrerequisites.Count - 1; // Gets the index of the last prerequisite
                    string punctuation = preReq == _assignedSkill.SkillPrerequisites[lastIndex] ? "" : ","; // Adds a comma unless it's the last prerequisite
                    _skillPreReqLabel.text += $" {preReq.SkillName}{punctuation}"; // Appends each prerequisite to the label
                }
            }
        }
        else
        {
            if (_skillPreReqLabel != null) _skillPreReqLabel.text = ""; // Clears the prerequisites label if there are no prerequisites
        }

        // Update the purchase button text and state
        if (_purchaseSkillButton != null)
        {
            if (_uiManager.PlayerSkillManager.IsSkillUnlocked(_assignedSkill))
            {
                _purchaseSkillButton.text = "Bought"; // Sets the button text to "Bought" if the skill is unlocked
                _purchaseSkillButton.SetEnabled(false); // Disables the button
            }
            else if (!_uiManager.PlayerSkillManager.PreReqMet(_assignedSkill))
            {
                _purchaseSkillButton.text = "PreReqs Have Not Been Met"; // Sets the button text if prerequisites are not met
                _purchaseSkillButton.SetEnabled(false); // Disables the button
            }
            else if (!_uiManager.PlayerSkillManager.CanBuySkill(_assignedSkill))
            {
                _purchaseSkillButton.text = "Cannot Buy This"; // Sets the button text if the skill cannot be bought
                _purchaseSkillButton.SetEnabled(false); // Disables the button
            }
            else
            {
                _purchaseSkillButton.text = "Buy"; // Sets the button text to "Buy" if the skill can be bought
                _purchaseSkillButton.SetEnabled(true); // Enables the button
            }
        }
    }
}
