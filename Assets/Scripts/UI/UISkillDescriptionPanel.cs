using _Scripts.Skill_System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UISkillDescriptionPanel : MonoBehaviour
{
    private UIManager _uiManager;
    private ScriptableSkill _assignedSkill;
    private VisualElement _skillImage;
    private Label _skillNameLabel, _skillDescriptionLabel, _skillCostLabel, _skillPreReqLabel;
    private Button _purchaseSkillButton;

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
    }

    private void OnEnable()
    {
        UITalentButton.OnSkillButtonClicked += PopulateLabeltext;
    }

    private void OnDisable()
    {
        UITalentButton.OnSkillButtonClicked -= PopulateLabeltext;
        if (_purchaseSkillButton != null)
        {
            _purchaseSkillButton.clicked -= PurchaseSkill;
        }
    }

    private void Start()
    {
        GatherLabelReferences();
        // Initialize with a default skill if needed
        var skill = _uiManager.SkillLibrary.GetSkillsOfTier(1).FirstOrDefault();
        if (skill != null)
        {
            PopulateLabeltext(skill);
        }
    }

    private void GatherLabelReferences()
    {
        var root = _uiManager.UIDocument.rootVisualElement;
        _skillImage = root.Q<VisualElement>("Icon");
        _skillNameLabel = root.Q<Label>("SkillNameLabel");
        _skillDescriptionLabel = root.Q<Label>("SkillDescriptionLabel");
        _skillCostLabel = root.Q<Label>("SkillCostLabel");
        _skillPreReqLabel = root.Q<Label>("PreReqLabel");
        _purchaseSkillButton = root.Q<Button>("BuySkillButton");

        if (_skillImage == null) Debug.LogError("SkillImage not found.");
        if (_skillNameLabel == null) Debug.LogError("SkillNameLabel not found.");
        if (_skillDescriptionLabel == null) Debug.LogError("SkillDescriptionLabel not found.");
        if (_skillCostLabel == null) Debug.LogError("SkillCostLabel not found.");
        if (_skillPreReqLabel == null) Debug.LogError("PreReqLabel not found.");
        if (_purchaseSkillButton == null) Debug.LogError("PurchaseSkillButton not found.");
        else
        {
            _purchaseSkillButton.clicked += PurchaseSkill;
        }
    }

    private void PurchaseSkill()
    {
        if (_assignedSkill == null) return;

        if (_uiManager.PlayerSkillManager.CanBuySkill(_assignedSkill))
        {
            _uiManager.PlayerSkillManager.UnlockSkill(_assignedSkill);
            PopulateLabeltext(_assignedSkill);
        }
    }

    private void PopulateLabeltext(ScriptableSkill skill)
    {
        if (skill == null)
        {
            Debug.LogError("Passed skill is null.");
            return;
        }

        _assignedSkill = skill;

        // Update the skill image
        if (_skillImage != null && _assignedSkill.SkillIcon != null)
        {
            _skillImage.style.backgroundImage = new StyleBackground(_assignedSkill.SkillIcon);
            _skillImage.MarkDirtyRepaint(); // Force a repaint if necessary
        }

        // Update the other labels
        if (_skillNameLabel != null) _skillNameLabel.text = _assignedSkill.SkillName;
        if (_skillDescriptionLabel != null) _skillDescriptionLabel.text = _assignedSkill.SkillDescription;
        if (_skillCostLabel != null) _skillCostLabel.text = $"Cost: {skill.Cost}";

        if (_assignedSkill.SkillPrerequisites.Count > 0)
        {
            if (_skillPreReqLabel != null)
            {
                _skillPreReqLabel.text = "PreReqs: ";
                foreach (var preReq in skill.SkillPrerequisites)
                {
                    var lastIndex = _assignedSkill.SkillPrerequisites.Count - 1;
                    string punctuation = preReq == _assignedSkill.SkillPrerequisites[lastIndex] ? "" : ",";
                    _skillPreReqLabel.text += $" {preReq.SkillName}{punctuation}";
                }
            }
        }
        else
        {
            if (_skillPreReqLabel != null) _skillPreReqLabel.text = "";
        }

        // Update the purchase button text and state
        if (_purchaseSkillButton != null)
        {
            if (_uiManager.PlayerSkillManager.IsSkillUnlocked(_assignedSkill))
            {
                _purchaseSkillButton.text = "Bought";
                _purchaseSkillButton.SetEnabled(false);
            }
            else if (!_uiManager.PlayerSkillManager.PreReqMet(_assignedSkill))
            {
                _purchaseSkillButton.text = "PreReqs Have Not Been Met";
                _purchaseSkillButton.SetEnabled(false);
            }
            else if (!_uiManager.PlayerSkillManager.CanBuySkill(_assignedSkill))
            {
                _purchaseSkillButton.text = "Cannot Buy This";
                _purchaseSkillButton.SetEnabled(false);
            }
            else
            {
                _purchaseSkillButton.text = "Buy";
                _purchaseSkillButton.SetEnabled(true);
            }
        }
    }
}