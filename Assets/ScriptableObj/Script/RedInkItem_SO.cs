using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "RedInkItem",menuName = "DrawableItem/RedInkItem")]
public class RedInkItem_SO : DrawableItem_SO
{
    //public TypeOfInk ink = TypeOfInk.Red;
    public RedItem TypeOfItem;


}
public enum RedItem { NoWeapon,Hammer,Sword,Bow,toAdd_1,toAdd_2,to_Add_3,Last}
