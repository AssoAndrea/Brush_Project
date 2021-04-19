using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawableInventory",menuName = "Player/DrawableInventory")]

public class DrawableObjectsInventory : ScriptableObject
{
    public DrawableItem[] RedItem;
    public DrawableItem[] WhiteItem;
    public DrawableItem[] GreenItem;
}
