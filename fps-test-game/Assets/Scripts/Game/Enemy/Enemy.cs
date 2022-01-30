
using UnityEngine;

public class Enemy : EntityBehaviour {

    public int projectileId;
    public Transform gun;
    public float reloadTime;
    public float moveForce;
    public GameObject explodeEffect;

    private float reloadTimer;
    private ProjectileManager projectileManager;
    private Rigidbody rbody;

    public override void Spawn () {

        projectileManager = FindObjectOfType<ProjectileManager>();
        rbody = FindObjectOfType<Rigidbody>();

        reloadTime = Random.Range(0.5f, 2.0f);
    }

    private int actionId;
    private float actionTimer;

    public override void Tick () {

        if ((PlayerRef.player.position - transform.position).magnitude < 16.32f) {

            transform.LookAt(PlayerRef.player);

            if (reloadTimer < 0) { reloadTimer = reloadTime;

                projectileManager.FireProjectile(projectileId, gun.position, gun.rotation);

            } else reloadTimer -= Time.deltaTime;
        }

        if (actionTimer < 0) {

            actionTimer = Random.Range(0.0f, 6.0f);
            actionId = Random.Range(0, 4);

        } else actionTimer -= Time.deltaTime;

        switch (actionId) {

            case 0: rbody.AddForce(transform.right * moveForce * Time.deltaTime); break;
            case 1: rbody.AddForce(transform.right * -moveForce * Time.deltaTime); break;
            case 2: rbody.AddForce(transform.forward * moveForce * Time.deltaTime); break;
            case 3: rbody.AddForce(transform.forward * -moveForce * Time.deltaTime); break;
        }
    }

    public override void OnDespawnShow () {

        Instantiate(explodeEffect, transform.position, transform.rotation);
    }
}
