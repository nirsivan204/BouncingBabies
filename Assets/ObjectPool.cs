using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    //[SerializeField] private BabyScript _baby;
    [SerializeField] GameObject _baby;

    //private IObjectPool<BabyScript> _babyPool;
    public IObjectPool<GameObject> _babyPool;


    private void Awake()
    {
        //_babyPool = new ObjectPool<BabyScript>(CreateBaby, OnGetBaby, OnReleaseBaby, OnDestroyBaby, true,10,5);
        _babyPool = new ObjectPool<GameObject>(CreateBaby, OnGetBaby, OnReleaseBaby, OnDestroyBaby, true,10,5);
    }

    private void OnDestroyBaby(GameObject baby)
    {
        Destroy(baby);
    }
    private GameObject CreateBaby()
    {
        GameObject baby = Instantiate(_baby, transform.position, Quaternion.identity);
        //baby.SetPool(_babyPool);
        return baby;
    }

    private void OnGetBaby(GameObject baby)
    {
        baby.SetActive(true);
    }

    private void OnReleaseBaby(GameObject baby)
    {
        baby.gameObject.SetActive(false);
    }


/*    private BabyScript CreateBaby()
    {
        BabyScript baby = Instantiate(_baby, transform.position, Quaternion.identity);
        baby.SetPool(_babyPool);
        return baby;
    }

    private void OnGetBaby(BabyScript baby)
    {
        baby.gameObject.SetActive(true);
        baby.transform.position = transform.position;
    }

    private void OnReleaseBaby(BabyScript baby)
    {
        baby.gameObject.SetActive(false);
    }

    private void OnDestroyBaby(BabyScript baby)
    {
        Destroy(baby.gameObject);
    }*/


}
