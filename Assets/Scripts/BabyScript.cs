using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BabyScript : MonoBehaviour
{
    private IObjectPool<BabyScript> _pool;

    public void SetPool(IObjectPool<BabyScript> pool)
    {
        _pool = pool;
    }

    public IObjectPool<BabyScript> GetPool()
    {
        return _pool;
    }

    

}
