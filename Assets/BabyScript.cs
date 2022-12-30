using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BabyScript:MonoBehaviour
{
    private IObjectPool<BabyScript> m_pool;

    public void SetPool(IObjectPool<BabyScript> pool)
    {
        m_pool = pool;
    }


    private void OnCollect()
    {
        m_pool?.Release(this);
    }

}
