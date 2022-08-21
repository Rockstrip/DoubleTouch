using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    private void Awake()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }

    public IEnumerator SetTimer(int time)
    {
        while (time > 0)
        {
            _textField.text = time.ToString();
            time--;
            yield return new WaitForSeconds(1f);
        }

        _textField.text = "";
    }
}
