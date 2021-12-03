using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


public class ProjectileScript : MonoBehaviour

{
    public float damage;

    public Vector3 targetPos;
    public float speed;
    public float arcHeight;

    public string faction;


    [SerializeField]
    bool isLaunched = false;

    Vector3 startPos;
    public void Launch(GameObject _targetUnit, float _damage, float _speed, float _arcHeight, Sprite _sprite, string _faction)
    {
        targetPos = _targetUnit.gameObject.transform.position;
        damage = _damage;
        speed = _speed;
        arcHeight = _arcHeight;
        faction = _faction;
        startPos = transform.position;

        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _sprite;

        isLaunched = true;    
    }
    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(2f);
        ObjectPoolScript.ReturnObject(this);
    }

    void Update()

    {
        if (isLaunched == true)
        {
            SimulateProjectile();
        }
        
    }

    void SimulateProjectile()
    {
        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        // Rotate to face the next position, and then move there
        transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;

        // Do something when we reach the target
        if (nextPos == targetPos)
        {
            StartCoroutine("LifeTimer");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (faction)
        {
            case ("P1"):
                if (collision.gameObject.CompareTag("P2"))
                {
                    StopCoroutine("LifeTimer");
                    isLaunched = false;
                    collision.gameObject.SendMessage("HitAttack", damage);
                    ObjectPoolScript.ReturnObject(this);
                }
                break;
            case ("P2"):
                if (collision.gameObject.CompareTag("P1"))
                {
                    StopCoroutine("LifeTimer");
                    isLaunched = false;
                    collision.gameObject.SendMessage("HitAttack", damage);
                    ObjectPoolScript.ReturnObject(this);
                }
                break;
        }           
       
    }

    void Arrived()

    {
        Destroy(gameObject);
    }

    ///

    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion

    /// that makes the local +X axis point in the given forward direction.

    ///

    /// forward direction

    /// Quaternion that rotates +X to align with forward

    static Quaternion LookAt2D(Vector2 forward)

    {

        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);

    }

}
