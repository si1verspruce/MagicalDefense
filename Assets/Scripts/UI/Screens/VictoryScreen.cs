using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : RestartScreen
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private Shop _shop;

    public void ActivateShopScreen()
    {
        _shop.ActivateScreen();
    }

    public override void CloseScreen()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;
    }
}
