using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum _AttackType
{
    singleMelee,

    splashMelee,

    range,

    special
}


[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/Unit Data", order = int.MaxValue)]

public class UnitData : ScriptableObject
{
    // Inspector 초기 값
    #region Info
    [SerializeField]
    private string unitName;
    [SerializeField]
    private Sprite unitIcon;
    [SerializeField]
    private int unitPrice;
    [SerializeField]
    private int unitTrainingTime;    
    #endregion Info

    #region Stat
    [SerializeField]
    private float hp;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private _AttackType attackType;
    #endregion Stat

    #region Range Stat
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float arcHeight;
    [SerializeField]
    private Sprite projectileSprite;
    #endregion Range Stat

    #region ETC
    [SerializeField]
    private AnimatorOverrideController overrideController;
    [SerializeField]
    private Vector2 colliderSize;
    #endregion ETC

    // Get 접근자 함수
    #region Info
    public string UnitName { get { return unitName; } }
    public Sprite UnitIcon { get { return unitIcon; } }
    public int UnitPrice { get { return unitPrice; } }
    public int UnitTrainingTime { get { return unitTrainingTime; } }
    #endregion Info

    #region Stat
    public float Hp { get { return hp; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float AttackDamage { get { return attackDamage; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public float AttackRange { get { return attackRange; } }
    public int AttackType { get { return (int)attackType; } }
    #endregion Stat

    #region Range Stat
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public float ArcHeight { get { return arcHeight; } }
    public Sprite ProjectileSprite { get { return projectileSprite; } }
    #endregion Range Stat

    #region ETC
    public AnimatorOverrideController OverrideController { get { return overrideController; } }
    public Vector2 ColliderSize { get { return colliderSize; } }
    #endregion ETC

}
