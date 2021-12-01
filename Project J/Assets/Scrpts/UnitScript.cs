using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitScript : MonoBehaviour
{
    public float _hp;

    public float _moveSpeed;

    public float _attackDamage;

    public float _attackSpeed;

    public float _attackRange;

    public string faction;

    public float _attackTimer;

    public Sprite _unitIcon;

    public int _unitPrice;

    public string _unitName;

    public float _projectileSpeed;

    public float _arcHeight;

    public Sprite _projectileSprite;

    public List<AudioClip> audioList = new List<AudioClip>();

    


    [SerializeField]
    Animator anim;

    bool isAttacking = false;

    Vector2 pos;
    Ray2D ray;

    AudioSource audioSource;
    

    public enum UnitState
    {
        idle,

        run,

        stun,

        attack,

        die
    }
    public enum AttackType
    {
        singleMelee,

        splashMelee,

        range
    }

    public UnitState _unitState;
    public AttackType _attackType;

    void Start()
    {
        
    }


    void Awake()
    {
        anim = GetComponent<Animator>();

        SetState(UnitState.run);

        if (this.tag == "P1" )
        {
            faction = "P1";
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            faction = "P2";
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Update()
    {
        pos = transform.position;     
        CheckState();
        SetZpos();
    }

    void SetZpos()
    {
        Vector3 tPos = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.1f);
        transform.localPosition = tPos;
    }

    void FindEmeny()
    {    
        if (faction == "P1")
        {
            ray = new Ray2D(pos, Vector2.right);
            Debug.DrawRay(ray.origin, ray.direction * _attackRange, Color.blue, 0.3f);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, _attackRange, LayerMask.GetMask("P2"));
            if (rayHit.collider == null)
            {
                SetState(UnitState.run);
            }
            else
            {
                SetState(UnitState.attack);
            }
        }
        else
        { 
            ray = new Ray2D(pos, Vector2.left);
            Debug.DrawRay(ray.origin, ray.direction * _attackRange, Color.blue, 0.3f);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, _attackRange, LayerMask.GetMask("P1"));
            if (rayHit.collider == null)
            {
                SetState(UnitState.run);
            }
            else
            {
                SetState(UnitState.attack);
            }
        }
                        
    }

    void CheckState()
    {
        switch (_unitState)
        {
            case UnitState.idle:
                FindEmeny();
                break;
            case UnitState.run:
                FindEmeny();
                Move();
                break;
            case UnitState.stun:
                break;
            case UnitState.attack:
                FindEmeny();
                CheckAttack();
                break;
        }
           
    }


    void SetState(UnitState state)
    {
        _unitState = state;
        switch (state)
        {
            case UnitState.idle:
                anim.SetBool("isRunning", false);
                break;
            case UnitState.run:
                anim.SetBool("isRunning", true);
                break;
            case UnitState.stun:
                anim.SetBool("isRunning", false);
                anim.SetBool("isStunned", false);
                break;
            case UnitState.attack:
                anim.SetBool("isRunning", false);
                break;
            case UnitState.die:
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                anim.SetTrigger("Die");
                break;

        }
    }


    void Move()
    {
        if (!isAttacking)
        {
            switch (faction)
            {
                case "P1":
                    transform.Translate(Vector2.right * Time.deltaTime * _moveSpeed);
                    break;
                case "P2":
                    transform.Translate(Vector2.left * Time.deltaTime * _moveSpeed);
                    break;
            }
        }
    }

    void CheckAttack()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer > 1 / _attackSpeed)
        {
            anim.SetTrigger("Attack");
            _attackTimer = 0;
        }
    }

    void DoAttack()
    {
        GameObject target;
        RaycastHit2D rayHit;        
        
        switch (_attackType)
        {
            case AttackType.singleMelee:
                if (faction == "P1")
                {
                    Debug.DrawRay(ray.origin, ray.direction * _attackRange, Color.blue, 0.3f);
                    rayHit = Physics2D.Raycast(ray.origin, ray.direction, _attackRange, LayerMask.GetMask("P2"));
                    if (rayHit.collider != null)
                    {
                        target = rayHit.collider.gameObject;
                        target.gameObject.SendMessage("HitAttack", _attackDamage);
                    }
                    else
                    {
                        SetState(UnitState.run);
                    }
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * _attackRange, Color.blue, 0.3f);
                    rayHit = Physics2D.Raycast(ray.origin, ray.direction, _attackRange, LayerMask.GetMask("P1"));
                    if (rayHit.collider != null)
                    {
                        target = rayHit.collider.gameObject;
                        target.gameObject.SendMessage("HitAttack", _attackDamage);
                    }
                    else
                    {
                        SetState(UnitState.run);
                    }
                }
                break;
            case AttackType.splashMelee:
                break;
            case AttackType.range:
                if (faction == "P1")
                {
                    Debug.DrawRay(ray.origin, ray.direction * _attackRange, Color.blue, 0.3f);
                    rayHit = Physics2D.Raycast(ray.origin, ray.direction, _attackRange, LayerMask.GetMask("P2"));
                    if (rayHit.collider != null)
                    {
                        var projectile = ObjectPoolScript.GetObject();
                        projectile.gameObject.transform.position = transform.position; 
                        projectile.Launch(rayHit.collider.gameObject, _attackDamage, _projectileSpeed, _arcHeight, _projectileSprite, "P1");
                    }
                    else
                    {
                        SetState(UnitState.run);
                    }
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * _attackRange, Color.blue, 0.3f);
                    rayHit = Physics2D.Raycast(ray.origin, ray.direction, _attackRange, LayerMask.GetMask("P1"));
                    if (rayHit.collider != null)
                    {
                        var projectile = ObjectPoolScript.GetObject();
                        projectile.gameObject.transform.position = transform.position;
                        projectile.Launch(rayHit.collider.gameObject, _attackDamage, _projectileSpeed, _arcHeight, _projectileSprite, "P2");
                    }
                    else
                    {
                        SetState(UnitState.run);
                    }
                }
                break;
        }
     
    }

    void HitAttack(float f)
    {
        _hp -= f;
        Debug.Log(_hp);
        if (_hp <= 0)
        {
            SetState(UnitState.die);
        }
    }


    void Die()
    {

    }

    void PlaySound(string s)
    {
        switch (s)
        {
            case "Hit":
                audioSource.clip = audioList[0];
                break;
            case "Die":
                audioSource.clip = audioList[1];
                break;
            case "Shoot":
                audioSource.clip = audioList[2];
                break;
        }

    }
}
