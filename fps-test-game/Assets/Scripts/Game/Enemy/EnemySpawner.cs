
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float MINspawnDelay, MAXspawnDelay;

    public int[] types;

    private float timer = 4.0f;

    private void Update () {

        timer -= Time.deltaTime;
        if (timer < 0) timer = Random.Range(MINspawnDelay, MAXspawnDelay); else return;

        DriftureManager.CreateEntity(
            types[Random.Range(0, types.Length)],
            new Vector3(Random.Range(-96, 96), 1, Random.Range(-96, 96)),
            new byte[0]{}
        );
    }
}
