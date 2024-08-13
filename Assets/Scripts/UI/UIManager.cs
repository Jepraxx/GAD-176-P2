using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private PlayerSkillManager _playerSkillManager;
    public PlayerSkillManager PlayerSkillManager => _playerSkillManager; // Corrected Property Name

    private UIDocument _uiDocument;
    public UIDocument UIDocument => _uiDocument; // Corrected Property Name

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
}
