using _Scripts.Skill_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScriptableSkillLibrary skillLibrary;
    [SerializeField] private VisualTreeAsset uiTalentButton;
    private PlayerSkillManager _playerSkillManager;
    public PlayerSkillManager PlayerSkillManager => _playerSkillManager; // Corrected Property Name

    private UIDocument _uiDocument;
    public UIDocument UIDocument => _uiDocument; // Corrected Property Name
    private VisualElement _skillTopRow, _skillBottomRow, _skillMiddleRow;
    [SerializeField] private List<UITalentButton> _talentButtons = new List<UITalentButton>(); // Initialize the list

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
        var root = _uiDocument.rootVisualElement; // Corrected Syntax
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
            Button clonedButton = clonedTree.Q<Button>(); // Query the Button from the cloned VisualTreeAsset
            if (clonedButton != null)
            {
                _talentButtons.Add(new UITalentButton(clonedButton, skill));
                parent.Add(clonedButton);
            }
            else
            {
                Debug.LogError("Button not found in the cloned VisualTreeAsset.");
            }
        }
    }
}
