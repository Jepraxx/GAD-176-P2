using UnityEngine;
using UnityEngine.UIElements;

public class UIStatPanel : MonoBehaviour
{
    private Label _StrengthLabel, _IntelligenceLabel, _MagicLabel, _StaminaLabel, _DexterityLabel;
    private Label _DashLabel, _FireBallLabel, _InvisibilityLabel;
    private Label _SkillPointsLabel;

    private UIManager _UIManager;

    private void Awake()
    {
        _UIManager = GetComponent<UIManager>();
        if (_UIManager == null)
        {
            Debug.LogError("UIManager not found");
        }
    }

    private void OnEnable()
    {
        if (_UIManager != null && _UIManager.PlayerSkillManager != null)
        {
            _UIManager.PlayerSkillManager.OnSkillPointsChanged += PopulateLabelText;
        }
    }

    private void OnDisable()
    {
        if (_UIManager != null && _UIManager.PlayerSkillManager != null)
        {
            _UIManager.PlayerSkillManager.OnSkillPointsChanged -= PopulateLabelText;
        }
    }

    void Start()
    {
        GatherLabelReferences();
        PopulateLabelText();
    }

    private void GatherLabelReferences()
    {
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            _StrengthLabel = uiDocument.rootVisualElement.Q<Label>("StrengthLabel");
            _IntelligenceLabel = uiDocument.rootVisualElement.Q<Label>("IntelligenceLabel");
            _MagicLabel = uiDocument.rootVisualElement.Q<Label>("MagicLabel");
            _StaminaLabel = uiDocument.rootVisualElement.Q<Label>("StaminaLabel");
            _DexterityLabel = uiDocument.rootVisualElement.Q<Label>("DexterityLabel");

            _DashLabel = uiDocument.rootVisualElement.Q<Label>("DashLabel");
            _FireBallLabel = uiDocument.rootVisualElement.Q<Label>("FireBallLabel");
            _InvisibilityLabel = uiDocument.rootVisualElement.Q<Label>("InvisibilityLabel");

            _SkillPointsLabel = uiDocument.rootVisualElement.Q<Label>("SkillPointsLabel");

            if (_StrengthLabel == null || _IntelligenceLabel == null || _MagicLabel == null || _StaminaLabel == null || _DexterityLabel == null ||
                _DashLabel == null || _FireBallLabel == null || _InvisibilityLabel == null || _SkillPointsLabel == null)
            {
                Debug.LogError("One or more UI labels are missing in the UI document.");
            }
        }
        else
        {
            Debug.LogError("UIDocument not found on the GameObject");
        }
    }

    private void PopulateLabelText()
    {
        if (_UIManager != null && _UIManager.PlayerSkillManager != null)
        {
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
            Debug.LogError("PlayerSkillManager not found on the UIManager");
        }
    }

    void Update()
    {
        // Optionally update labels here
        // PopulateLabelText();
    }
}
