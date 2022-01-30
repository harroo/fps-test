
using UnityEngine;

public class PlayerAccess : MonoBehaviour {

    private void Start () {

        PlayerRef.player = transform;
    }
}

public static class PlayerRef {

    public static Transform player;
}
