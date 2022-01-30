
using UnityEngine;

public class PlayerGun : MonoBehaviour {

    public int projectileId;
    public Transform gun;

    private ProjectileManager projectileManager;
    private void Start () {

        projectileManager = FindObjectOfType<ProjectileManager>();
    }

    private void Update () {

        if (!Input.GetMouseButtonDown(0)) return;

        projectileManager.FireProjectile(projectileId, gun.position, gun.rotation);
    }
}
