using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DrawableItem
{
    public RedInkType type;
    public DrawableItem_SO item;

}
public enum TypeOfInk
{
    White, Red, Green, Last
}
public enum RedInkType
{
    Sword, Hammer,Arco
}
