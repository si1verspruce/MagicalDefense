using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class YesNoPopupWindow : PopupWindow
{
    [SerializeField] private Button _reject;

    public event UnityAction RejectClick;

    protected override void Activate()
    {
        _reject.onClick.AddListener(OnRejectClick);
        base.Activate();
    }

    protected override void Deactivate()
    {
        _reject.onClick.RemoveListener(OnRejectClick);
        base.Deactivate();
    }

    private void OnRejectClick()
    {
        RejectClick?.Invoke();
        gameObject.SetActive(false);
    }
}
