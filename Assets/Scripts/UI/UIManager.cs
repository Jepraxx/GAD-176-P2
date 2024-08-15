using UnityEngine;
using UnityEngine.UIElements;
using _Scripts.Skill_System;
using System.Collections.Generic;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScriptableSkillLibrary skillLibrary;
    public ScriptableSkillLibrary SkillLibrary => skillLibrary;
    [SerializeField] private VisualTreeAsset uiTalentButton;
    private PlayerSkillManager _playerSkillManager;
    public PlayerSkillManager PlayerSkillManager => _playerSkillManager;

    private UIDocument _uiDocument;
    public UIDocument UIDocument => _uiDocument;
    private VisualElement _skillTopRow, _skillBottomRow, _skillMiddleRow;
    [SerializeField] private List<UITalentButton> _talentButtons = new List<UITalentButton>();

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null)
        {
            Debug.LogError("UIDocument component is missing on the UIManager GameObject.");
        }

        _playerSkillManager = FindObjectOfType<PlayerSkillManager>();
        if (_playerSkillManager == null)
        {
            Debug.LogError("PlayerSkillManager component is not found in the scene.");
        }
    }

    private void Start()
    {
        CreateSkillButtons();
    }

    private void CreateSkillButtons()
    {
        var root = _uiDocument.rootVisualElement;
        _skillTopRow = root.Q<VisualElement>("1");
        _skillMiddleRow = root.Q<VisualElement>("2");
        _skillBottomRow = root.Q<VisualElement>("3");
        SpawnButtons(_skillTopRow, skillLibrary.GetSkillsOfTier(1));
        SpawnButtons(_skillMiddleRow, skillLibrary.GetSkillsOfTier(2));
        SpawnButtons(_skillBottomRow, skillLibrary.GetSkillsOfTier(3));
    }

    private void SpawnButtons(VisualElement parent, List<ScriptableSkill> skills)
    {
        foreach (var skill in skills)
        {
            VisualElement clonedTree = uiTalentButton.CloneTree();
            Button clonedButton = clonedTree.Q<Button>();
            if (clonedButton != null)
            {
                var button = new UITalentButton(clonedButton, skill, this);
                _talentButtons.Add(button);
                clonedButton.clicked += () => DisplaySkillDetails(skill);
                parent.Add(clonedButton);
            }
            else
            {
                Debug.LogError("Button not found in the cloned VisualTreeAsset.");
            }
        }
    }

    public void DisplaySkillDetails(ScriptableSkill skill)
    {
        var root = _uiDocument.rootVisualElement;
        var skillPointsPanel = root.Q<VisualElement>("SkillPoints");

        var skillNameLabel = skillPointsPanel.Q<Label>("SkillNameLabel");
        var skillDescriptionLabel = skillPointsPanel.Q<Label>("SkillDescriptionLabel");
        var skillCostLabel = skillPointsPanel.Q<Label>("SkillCostLabel");
        var prereqLabel = skillPointsPanel.Q<Label>("PreReqLabel");

        skillNameLabel.text = skill.SkillName;
        skillDescriptionLabel.text = skill.SkillDescription;
        skillCostLabel.text = $"Skill Cost: {skill.Cost}";
        prereqLabel.text = "PreReqs: " + string.Join(", ", skill.SkillPrerequisites.Select(s => s.SkillName));

        // Remove or hide the icon element
        var icon = skillPointsPanel.Q<VisualElement>("icon");
        icon.style.backgroundImage = new StyleBackground(Texture2D.whiteTexture); // Set to white texture or remove element
        icon.style.display = DisplayStyle.None;
    }
}