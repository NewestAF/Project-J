using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleScript : MonoBehaviour
{

    public float _hp;


    public Vector3 summonPos;

    public GameObject spawnPoint;

    public float randomY;

    public GameObject UnitPrefab;

    void Awake()
    {
        
        spawnPoint = transform.GetChild(0).gameObject;
        
        if (this.tag == "P1")
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }
    }

    public void SummonUnit(UnitData _unit)
    {
        float randomY = Random.Range(-2.4f, -2.6f);
        summonPos = new Vector3(spawnPoint.transform.position.x, randomY, spawnPoint.transform.position.z);
        var _unitScript = Instantiate(UnitPrefab, summonPos, Quaternion.identity).GetComponent<UnitScript>();
        _unitScript.unitData = _unit;
        _unitScript.InitData();
    }

    void HitAttack(float f)
    {
        _hp -= f;
        Debug.Log(_hp);
        if (_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
