using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorInventory", menuName = "Player/ColorInventory")]
public class ColorsInventory : ScriptableObject
{
    public Color LockedColor;
    [Space(40)]
    public Color_Item[] colors;

    //da aggiungere se c'è tempo
    public Sprite LockedSprite;
}

[Serializable]
public struct Color_Item
{
    public TypeOfInk inkType;
    public Color color;

    public bool IsLocked;


}
