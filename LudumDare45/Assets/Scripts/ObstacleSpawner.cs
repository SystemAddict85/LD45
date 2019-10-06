using System.Collections;
using UnityEngine;

public class ObstacleSpawner : PoolableSpawner<Obstacle>
{
    public bool canSpawn = true;
    [SerializeField]
    [MinMaxRange(-20f, 20f)]
    private RangedFloat minMaxHorizontalSpawnLocation = new RangedFloat();

    [SerializeField]
    [MinMaxRange(0.5f, 30f)]
    private RangedFloat minMaxTimeBetweenSpawns = new RangedFloat();

    private float nextSpawnTime = 0f;
    private float currentTime = 0f;
    protected override void Awake()
    {
        base.Awake();
        _poolableParent.transform.parent = transform;
        _poolableParent.transform.localPosition = Vector3.zero;
        _poolableParent.transform.localEulerAngles = Vector3.zero;
    }
    private void Start()
    {
        nextSpawnTime = GetRandomSpawnTime();
    }
    private void Update()
    {
        if (canSpawn)
        {
            if (currentTime >= nextSpawnTime)
            {
                currentTime = 0f;
                nextSpawnTime = GetRandomSpawnTime();
                GetFromPool();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    private float GetRandomSpawnTime()
    {
        return Global.GetRandomNumberInRange(minMaxTimeBetweenSpawns);
    }

    public override Obstacle GetFromPool()
    {
        var obs = base.GetFromPool();
        obs.transform.localPosition = ChooseRandomHorizontalLocation();
        obs.EnablePoolableObject();
        return obs;
    }

    private Vector3 ChooseRandomHorizontalLocation()
    {
        Vector3 vec3 = new Vector3();
        vec3.x = Global.GetRandomNumberInRange(minMaxHorizontalSpawnLocation);
        return vec3;
    }
}
