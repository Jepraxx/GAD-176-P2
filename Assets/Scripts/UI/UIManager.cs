using UnityEngine; // Importing UnityEngine namespace for Unity-specific classes and functions
using UnityEngine.UIElements; // Importing UnityEngine.UIElements namespace for UI elements in Unity
using _Scripts.Skill_System; // Importing the Skill_System namespace to use its classes
using System.Collections.Generic; // Importing the System.Collections.Generic namespace for using collections like List
using System.Linq; // Importing the System.Linq namespace for LINQ queries

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScriptableSkillLibrary skillLibrary; // Reference to the ScriptableSkillLibrary to access skills
    public ScriptableSkillLibrary SkillLibrary => skillLibrary; // Public getter for the skillLibrary reference

    [SerializeField] private VisualTreeAsset uiTalentButton; // VisualTreeAsset for cloning talent buttons
    private PlayerSkillManager _playerSkillManager; // Reference to the PlayerSkillManager to handle skill data
    public PlayerSkillManager PlayerSkillManager => _playerSkillManager; // Public getter for the _playerSkillManager reference

    private UIDocument _uiDocument; // Reference to the UIDocument component for managing UI elements
    public UIDocument UIDocument => _uiDocument; // Public getter for the _uiDocument reference

    private VisualElement _skillTopRow, _skillMiddleRow, _skillBottomRow; // UI elements representing rows of skills
    [SerializeField] private List<UITalentButton> _talentButtons = new List<UITalentButton>(); // List of UITalentButton objects

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>(); // Initializes _uiDocument with the UIDocument component attached to the same GameObject
        if (_uiDocument == null)
        {
            Debug.LogError("UIDocument component is missing on the UIManager GameObject."); // Logs an error if UIDocument is missing
        }

        _playerSkillManager = FindObjectOfType<PlayerSkillManager>(); // Finds the PlayerSkillManager in the scene
        if (_playerSkillManager == null)
        {
            Debug.LogError("PlayerSkillManager component is not found in the scene."); // Logs an error if PlayerSkillManager is not found
        }
        TurnOff();
    }

    private void Start()
    {
        CreateSkillButtons(); // Calls method to create skill buttons
    }

    private void CreateSkillButtons()
    {
        var root = _uiDocument.rootVisualElement; // Gets the root element of the UI document
        _skillTopRow = root.Q<VisualElement>("1"); // Finds the top row element for skills
        _skillMiddleRow = root.Q<VisualElement>("2"); // Finds the middle row element for skills
        _skillBottomRow = root.Q<VisualElement>("3"); // Finds the bottom row element for skills

        // Creates and spawns buttons for each tier of skills
        SpawnButtons(_skillTopRow, skillLibrary.GetSkillsOfTier(1));
        SpawnButtons(_skillMiddleRow, skillLibrary.GetSkillsOfTier(2));
        SpawnButtons(_skillBottomRow, skillLibrary.GetSkillsOfTier(3));
    }

    private void SpawnButtons(VisualElement parent, List<ScriptableSkill> skills)
    {
        foreach (var skill in skills)
        {
            VisualElement clonedTree = uiTalentButton.CloneTree(); // Clones the VisualTreeAsset for each skill button
            Button clonedButton = clonedTree.Q<Button>(); // Finds the button in the cloned VisualTreeAsset

            if (clonedButton != null)
            {
                var button = new UITalentButton(clonedButton, skill, this); // Creates a UITalentButton instance
                _talentButtons.Add(button); // Adds the button to the list of talent buttons
                clonedButton.clicked += () => DisplaySkillDetails(skill); // Adds an event listener to display skill details when clicked
                parent.Add(clonedButton); // Adds the cloned button to the parent UI element
            }
            else
            {
                Debug.LogError("Button not found in the cloned VisualTreeAsset."); // Logs an error if the button is not found
            }
        }
    }

    public void DisplaySkillDetails(ScriptableSkill skill)
    {
        var root = _uiDocument.rootVisualElement; // Gets the root element of the UI document
        var skillPointsPanel = root.Q<VisualElement>("SkillPoints"); // Finds the panel for skill points

        var skillNameLabel = skillPointsPanel.Q<Label>("SkillNameLabel"); // Finds the label for skill name
        var skillDescriptionLabel = skillPointsPanel.Q<Label>("SkillDescriptionLabel"); // Finds the label for skill description
        var skillCostLabel = skillPointsPanel.Q<Label>("SkillCostLabel"); // Finds the label for skill cost
        var prereqLabel = skillPointsPanel.Q<Label>("PreReqLabel"); // Finds the label for skill prerequisites

        // Updates the labels with the skill's information
        skillNameLabel.text = skill.SkillName;
        skillDescriptionLabel.text = skill.SkillDescription;
        skillCostLabel.text = $"Skill Cost: {skill.Cost}";
        prereqLabel.text = "PreReqs: " + string.Join(", ", skill.SkillPrerequisites.Select(s => s.SkillName));

        // Remove or hide the icon element
        var icon = skillPointsPanel.Q<VisualElement>("icon"); // Finds the icon element
        icon.style.backgroundImage = new StyleBackground(Texture2D.whiteTexture); // Sets the icon to a white texture or remove element
        icon.style.display = DisplayStyle.None; // Hides the icon element
    }

    public void TurnOn()
    {
        _uiDocument.rootVisualElement.visible = true;
    }

        public void TurnOff()
    {
        _uiDocument.rootVisualElement.visible = false;
    }
}
