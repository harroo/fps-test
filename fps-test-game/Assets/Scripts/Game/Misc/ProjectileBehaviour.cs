
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public float startForce;

    private void Start () {

        GetComponent<Rigidbody>().AddForce(transform.forward * startForce);

        player = GameObject.FindWithTag("Player").transform;
    }

    private Transform player;

    private void Update () {

        if ((player.position - transform.position).magnitude < 0.82f) {

            Network.PlaySound("megadiesoudn", player.position);
            FindObjectOfType<ProjectileManager>().FireProjectile(2, player.position, player.rotation);
            player.position = new Vector3(Random.Range(-8, 8), 4, Random.Range(-8, 8));
            FindObjectOfType<Score>().Reset();
        }
    }

    private void OnCollisionEnter (Collision collision) {

        var behaviour = collision.collider.GetComponent<EntityBehaviour>();
        if (behaviour != null) {

            Network.PlaySound("megadiesoudn", player.position);
            if (behaviour.isLocal) {

                DriftureManager.DeleteEntity(behaviour.entityId);
                FindObjectOfType<Score>().Up();
            }
        }
    }
}
