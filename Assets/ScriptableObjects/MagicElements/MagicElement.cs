using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ElementType
{
    Fire,
    Lightning,
    Earth
}

[CreateAssetMenu(fileName = "Magic Element", menuName = "Scriptable Objects/Magic Element")]
public class MagicElement : ScriptableObject
{
    [SerializeField] private ElementType _type;
    [SerializeField] private Sprite _sprite;

    public ElementType Type => _type;
    public Sprite Sprite => _sprite;
}
