using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BabyPoolManager : AbstractPoolManager<BabyScript>
{

    protected override BabyScript OnCreateObj()
    {
        BabyScript baby = base.OnCreateObj();
        baby.SetPool(Pool);
        return baby;
    }

    protected override void OnGetObj(BabyScript baby)
    {
        baby.gameObject.SetActive(true);
    }

    protected override void OnReleaseObj(BabyScript baby)
    {
        baby.gameObject.SetActive(false);
    }


}
