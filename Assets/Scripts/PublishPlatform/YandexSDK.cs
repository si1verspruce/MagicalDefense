using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YandexSDK : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private RawImage _photo;

    /*[DllImport("__Internal")]
    private static extern void GetPlayerData();*/

    public void YandexGetPlayerData()
    {
        //GetPlayerData();
    }

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetPhoto(string photoURL)
    {
        StartCoroutine(photoURL, _photo);
    }

    private IEnumerator SetImageWhenDownloaded(string imageURL, Image image)
    {
        yield return null;
    }
}
