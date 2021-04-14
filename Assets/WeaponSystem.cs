using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { NoWeapon,Sword,Hammer,Bow,Last}

public class WeaponSystem : MonoBehaviour
{
    public WeaponType WeaponHandle;
    public GameObject SwordOBJ;
    public GameObject HammerOBJ;
    public GameObject BowOBJ;

    // Start is called before the first frame update
    void Start()
    {
        WeaponHandle = WeaponType.NoWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        switch (WeaponHandle)
        {
            case WeaponType.NoWeapon:
                break;
            case WeaponType.Sword:
                Sword();
                break;
            case WeaponType.Hammer:
                Hammer();
                break;
            case WeaponType.Bow:
                Bow();
                break;
            case WeaponType.Last:
                break;
            default:
                break;
        }
    }

    public void Sword()
    {

    }

    public void Hammer()
    {

    }

    public void Bow()
    {

    }
}
