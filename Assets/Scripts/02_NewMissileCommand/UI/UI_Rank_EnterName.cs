using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Rank_EnterName : MonoBehaviour
{
    public void SetScore(int _score)
    {
        textScore.text = _score.ToString("N0");
        score = _score;
    }

    public void SetActive(bool _isActive, VoidVoidDelegate _enterCallback = null)
    {
        gameObject.SetActive(_isActive);
        enterCallback = _enterCallback;
        //nameField.text = "";
        // �Ʒ����� �� ������ �������̴�.
        nameField.text = string.Empty;
    }


    private void Start()
    {
        nameField.onValueChanged.AddListener(
            delegate
            {
                OnValueChanged(nameField);
            }
            );

        buttonEnter.onClick.AddListener(
            ()=>
            {
                string name = nameField.text;
                if (name.Length == 0) return;

                if (PlayerPrefs.HasKey(name))
                {
                    int record = PlayerPrefs.GetInt(name);
                    if (record > score) return;
                }
                PlayerPrefs.SetInt(name, score);
                // Ű ���� ������ ������.
                Debug.Log(PlayerPrefs.GetInt(name));

                enterCallback?.Invoke();
            }
            );
    }

    private void OnValueChanged(TMP_InputField _nameField)
    {
        if (_nameField.text.Length > 3)
            _nameField.text = _nameField.text.Substring(0, 3);
    }


    [SerializeField]
    private TMP_Text textScore = null;
    [SerializeField]
    private TMP_InputField nameField = null;
    [SerializeField]
    private Button buttonEnter = null;

    private int score = 0;

    private VoidVoidDelegate enterCallback = null;
}