using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawableInventory",menuName = "Player/DrawableInventory")]

public class DrawableObjectsInventory : ScriptableObject
{
    public RedDrawableItem[] RedItem;
    public WhiteDrawableItem[] WhiteItem;
    public GreenDrawableItem[] GreenItem;
}
