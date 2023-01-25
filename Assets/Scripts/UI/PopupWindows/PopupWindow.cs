using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _confirm;
    [SerializeField] private GamePause _pause;

    public event UnityAction ConfirmClick;

    public void SetMessage(string message, string[] values, string valueTag)
    {
        var regex = new Regex(Regex.Escape(valueTag));

        for (int i = 0; i < values.Length; i++)
            message = regex.Replace(message, values[i], 1);

        _text.text = message;
    }

    public void SetMessage(string message)
    {
        _text.text = message;
    }

    private void OnEnable()
    {
        Activate();
    }

    private void OnDisable()
    {
        Deactivate();
    }

    protected virtual void OnConfirmClick()
    {
        gameObject.SetActive(false);
        ConfirmClick?.Invoke();
    }

    protected virtual void Activate()
    {
        _confirm.onClick.AddListener(OnConfirmClick);
        _pause.RequestPause(gameObject);
    }

    protected virtual void Deactivate()
    {
        _confirm.onClick.RemoveListener(OnConfirmClick);
        _pause.RequestPlay(gameObject);
    }
}
