using UnityEngine;
using UnityEngine.UIElements;

public class UIStatPanel : MonoBehaviour
{
    // Private fields for UI labels
    private Label _StrengthLabel, _IntelligenceLabel, _MagicLabel, _StaminaLabel, _DexterityLabel;
    private Label _DashLabel, _FireBallLabel, _SkillPointsLabel;

    private UIManager _UIManager; // Reference to the UIManager component

    // Called when the script instance is being loaded
    private void Awake()
    {
        _UIManager = GetComponent<UIManager>(); // Initializes _UIManager with the UIManager component attached to the same GameObject
        if (_UIManager == null)
        {
            Debug.LogError("UIManager not found"); // Logs an error if UIManager is missing
        }
    }

    // Called when the object becomes enabled and active
    private void OnEnable()
    {
        // Subscribes to the OnSkillPointsChanged event if UIManager and PlayerSkillManager are valid
        if (_UIManager != null && _UIManager.PlayerSkillManager != null)
        {
            _UIManager.PlayerSkillManager.OnSkillPointsChanged += PopulateLabelText; // Subscribes to the event
        }
    }

    // Called when the object becomes disabled or inactive
    private void OnDisable()
    {
        // Unsubscribes from the OnSkillPointsChanged event to avoid memory leaks
        if (_UIManager != null && _UIManager.PlayerSkillManager != null)
        {
            _UIManager.PlayerSkillManager.OnSkillPointsChanged -= PopulateLabelText; // Unsubscribes from the event
        }
    }

    // Called when the script instance is being loaded
    void Start()
    {
        GatherLabelReferences(); // Finds and assigns references to UI labels
        PopulateLabelText(); // Updates label texts with initial values
    }

    // Finds and assigns references to UI labels
    private void GatherLabelReferences()
    {
        var uiDocument = GetComponent<UIDocument>(); // Gets the UIDocument component
        if (uiDocument != null)
        {
            // Finds UI labels by their names in the UI document
            _StrengthLabel = uiDocument.rootVisualElement.Q<Label>("StrengthLabel");
            _IntelligenceLabel = uiDocument.rootVisualElement.Q<Label>("IntelligenceLabel");
            _MagicLabel = uiDocument.rootVisualElement.Q<Label>("MagicLabel");
            _StaminaLabel = uiDocument.rootVisualElement.Q<Label>("staminaLabel");
            _DexterityLabel = uiDocument.rootVisualElement.Q<Label>("DexterityLabel");

            _DashLabel = uiDocument.rootVisualElement.Q<Label>("DashLabel");
            _FireBallLabel = uiDocument.rootVisualElement.Q<Label>("FireBallLabel");

            _SkillPointsLabel = uiDocument.rootVisualElement.Q<Label>("SkillPoints_Label");

            // Logs an error if any of the labels are not found
            if (_StrengthLabel == null || _IntelligenceLabel == null || _MagicLabel == null || _StaminaLabel == null || _DexterityLabel == null ||
                _DashLabel == null || _FireBallLabel == null || _SkillPointsLabel == null)
            {
                Debug.LogError("One or more UI labels are missing in the UI document.");
            }
        }
        else
        {
            Debug.LogError("UIDocument not found on the GameObject"); // Logs an error if UIDocument is missing
        }
    }

    // Updates the text of all UI labels with the current player skill data
    private void PopulateLabelText()
    {
        if (_UIManager != null && _UIManager.PlayerSkillManager != null)
        {
            // Updates label texts with values from PlayerSkillManager
            _StrengthLabel.text = "Strength = " + _UIManager.PlayerSkillManager.Strength.ToString();
            _IntelligenceLabel.text = "Intelligence = " + _UIManager.PlayerSkillManager.Intelligence.ToString();
            _MagicLabel.text = "Magic = " + _UIManager.PlayerSkillManager.Magic.ToString();
            _StaminaLabel.text = "Stamina = " + _UIManager.PlayerSkillManager.Stamina.ToString();
            _DexterityLabel.text = "Dexterity = " + _UIManager.PlayerSkillManager.Dexterity.ToString();

            _DashLabel.text = "Dash = " + (_UIManager.PlayerSkillManager.Dash ? "Unlocked" : "Locked");
            _FireBallLabel.text = "FireBall = " + (_UIManager.PlayerSkillManager.FireBall ? "Unlocked" : "Locked");
            _SkillPointsLabel.text = "Skill Points = " + _UIManager.PlayerSkillManager.SkillPoints.ToString();
        }
        else
        {
            Debug.LogError("PlayerSkillManager not found on the UIManager"); // Logs an error if PlayerSkillManager is missing
        }
    }

    void Update()
    {
        PopulateLabelText();
    }
}
