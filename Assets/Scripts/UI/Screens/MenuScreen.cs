using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuScreen : PauseScreen
{
    [SerializeField] private Shop _shop;

    public event UnityAction<bool> Activated;

    public void ActivateShopScreen()
    {
        _shop.ActivateScreen();
    }

    public override void OpenScreen()
    {
        base.OpenScreen();
        OnScreenActivityChanged(true);
    }

    public override void CloseScreen()
    {
        base.CloseScreen();
        OnScreenActivityChanged(false);
    }

    private void OnScreenActivityChanged(bool isActivated)
    {
        gameObject.SetActive(isActivated);
        Activated?.Invoke(isActivated);
    }
}
