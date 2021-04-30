using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DrawableItem
{
    public DrawableItem_SO item;
    public bool isLocked;

}
[Serializable]
public class RedDrawableItem : DrawableItem
{
    public RedItem type;

}

[Serializable]
public class GreenDrawableItem : DrawableItem
{
    public GreenItem type;

}
[Serializable]
public class WhiteDrawableItem : DrawableItem
{
    public WhiteItem type;

}

public enum TypeOfInk
{
    White, Red, Green,Blue,Orange,Purple,Yellow, Last
}
public enum ItemType
{
    Sword, Hammer,Bow,NoWeapon
}
