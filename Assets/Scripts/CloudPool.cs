using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPool : AbstractPool<Cloud>
{
    [SerializeField] Sprite[] _cloudSprites;
    [SerializeField] Transform[] _cloudSpawnPositions;
    [SerializeField] float _cloudAvgSpeed;

    protected override void OnGetObj(Cloud obj)
    {
        InitCloud(obj);
    }

    protected override void OnReleaseObj(Cloud obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected override Cloud OnCreateObj()
    {
        Cloud cloud = base.OnCreateObj();
        InitCloud(cloud);
        cloud.SetPool(_pool);
        return cloud;
    }

    private void InitCloud(Cloud cloud)
    {
        //set random place to start
        int randomPlaceID = Random.Range(0, _cloudSpawnPositions.Length);
        cloud.transform.position = _cloudSpawnPositions[randomPlaceID].position;
        //set random Sprite and Speed
        int randomImageID = Random.Range(0, _cloudSprites.Length);
        float randomSpeed = Random.Range(0.01f, 1) * _cloudAvgSpeed; // must not be zero
        cloud.Init(_cloudSprites[randomImageID], randomSpeed);
        cloud.gameObject.SetActive(true);
    }
}
