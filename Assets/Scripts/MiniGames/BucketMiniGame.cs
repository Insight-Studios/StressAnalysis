using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketMiniGame : MiniGameBase
{
    
    [SerializeField] private float spawnDelayProp = 0.05f;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject nailPrefab;
    [SerializeField] private Collider2D bucketCollider;
    [SerializeField] private Collider2D conveyorCollider;

    private GameObject bucket;
    private int bucketIndex;
    private float timeSinceSpawn;
    private List<Collider2D> nailColliders;

    private int BucketIndex
    {
        get {
            return bucketIndex;
        }
        set {
            if (value < 0) value = 0;
            else if (value > spawners.Length - 1) value = spawners.Length - 1;

            bucket.transform.Translate( Vector3.right * (spawners[value].transform.position.x - bucket.transform.position.x) );

            bucketIndex = value;
        }
    }

    protected override void MiniGameStart()
    {
        nailColliders = new List<Collider2D>();

        bucketCollider.isTrigger = true;

        bucket = bucketCollider.gameObject;
        BucketIndex = 0;
        timeSinceSpawn = 0;
    }

    protected override void MiniGameUpdate()
    {
        List<Collider2D> toBeRemoved = new List<Collider2D>();
        foreach (Collider2D nailCollider in nailColliders)
        {
            if (bucketCollider.IsTouching(nailCollider))
            {
                Score++;
                toBeRemoved.Add(nailCollider);
                Destroy(nailCollider.gameObject);
            }
            else if (conveyorCollider.IsTouching(nailCollider))
            {
                Score = 0;
                toBeRemoved.Add(nailCollider);
                Destroy(nailCollider.gameObject);
            }
        }

        nailColliders.RemoveAll( (Collider2D collider) => { return toBeRemoved.Contains(collider); } );

        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= Mathf.Max(spawnDelayProp * LifeTime, minSpawnDelay))
        {
            GameObject newNail = Instantiate(nailPrefab, spawners[Random.Range(0, spawners.Length)].transform);
            nailColliders.Add(newNail.GetComponent<Collider2D>());
            timeSinceSpawn = 0;
        }
    }

    public override void SendInput(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.LeftArrow:
                BucketIndex--;
                break;
            case KeyCode.RightArrow:
                BucketIndex++;
                break;
        }
    }

    protected override void MiniGameEnd()
    {
        foreach (Collider2D nailCollider in nailColliders)
        {
            Destroy(nailCollider.gameObject);
        }
    }
}
