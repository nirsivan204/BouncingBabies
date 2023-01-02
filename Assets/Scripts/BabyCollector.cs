using UnityEngine.Pool;
using UnityEngine;
using System;

public abstract class BabyCollector : MonoBehaviour
{
    [SerializeField] protected SoundType _collectSound;

    protected Action _babyCollectedEvent;
    
    public Action BabyCollectedEvent { get => _babyCollectedEvent; set { _babyCollectedEvent = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BabyScript collectedBaby = collision.GetComponent<BabyScript>();
        if (collectedBaby != null)
        {
            collectedBaby.GetPool().Release(collectedBaby);
            BabyCollectedAffect();
            _babyCollectedEvent?.Invoke();
        }
    }

    protected virtual void BabyCollectedAffect()
    {
        AudioManager.Instance.PlaySound(_collectSound);
    }

}