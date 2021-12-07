using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer1Script : MonoBehaviour
{
    UnitScript unitscript;

    public float hp;

    public float moveSpeed;

    public float attackDamage;

    public float attackSpeed;

    public float attackRange;

    public Sprite unitIcon;

    public int unitPrice;

    public string unitName;

    public int unitTrainingTime;

    public float projectileSpeed;

    public float arcHeight;

    public Sprite projectileSprite;


    void Awake()
    {
        unitscript = GetComponent<UnitScript>();
        Init();
    }
    void Init()
    {
        unitscript._hp = hp;
        unitscript._moveSpeed = moveSpeed;
        unitscript._attackDamage = attackDamage;
        unitscript._attackSpeed = attackSpeed;
        unitscript._attackRange = attackRange;
        unitscript._unitIcon = unitIcon;
        unitscript._unitPrice = unitPrice;
        unitscript._unitName = unitName;
        unitscript._unitTraingTime = unitTrainingTime;
        unitscript._projectileSpeed = projectileSpeed;
        unitscript._arcHeight = arcHeight;
        unitscript._projectileSprite = projectileSprite;
    }  
}
