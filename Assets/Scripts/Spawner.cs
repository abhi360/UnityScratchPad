using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;

    public Spawnable[] spawnablePrefab;

    public Material[] materials;

    public float velocity;

    private float timeSinceLastSpawn;
    private float currentSpawnDelay;

    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= currentSpawnDelay)
        {
            timeSinceLastSpawn -= currentSpawnDelay;
            currentSpawnDelay = timeBetweenSpawns.RandomInRange;
            SpawnStuff();
        }
    }

    void SpawnStuff()
    {
        Spawnable prefab = spawnablePrefab[Random.Range(0, spawnablePrefab.Length)];
        Spawnable spawn = prefab.GetPooledInstance<Spawnable>();

        spawn.transform.localPosition = transform.position;
        spawn.transform.localScale = Vector3.one * scale.RandomInRange;
        spawn.transform.localRotation = Random.rotation;

        spawn.Body.velocity = transform.up * velocity +
            Random.onUnitSphere * randomVelocity.RandomInRange;

        spawn.Body.angularVelocity =
            Random.onUnitSphere * angularVelocity.RandomInRange;

        spawn.SetMaterial(materials[Random.Range(0, materials.Length)]);
    }
}


[System.Serializable]
public struct FloatRange
{

    public float min, max;

    public float RandomInRange
    {
        get
        {
            return Random.Range(min, max);
        }
    }
}
