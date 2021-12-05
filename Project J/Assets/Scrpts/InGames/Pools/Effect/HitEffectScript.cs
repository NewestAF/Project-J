using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectScript: MonoBehaviour
{
    public void Return()
    {
        HitEffectPool.ReturnObject(this);
    }

}
