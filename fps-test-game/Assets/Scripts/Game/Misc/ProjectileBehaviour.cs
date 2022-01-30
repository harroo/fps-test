
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

            Network.PlaySound("diesound", player.position);
            player.position = new Vector3(Random.Range(-8, 8), 4, Random.Range(-8, 8));
        }
    }

    private void OnCollisionEnter (Collision collision) {

        if (collision.collider.tag == "Enemy") {
        }
    }
}
