
using UnityEngine;

using BlitzBit;

public class LocalPlayerPosSync : MonoBehaviour {

    public Transform player, cam;

    private float timer = 1.0f;

    private void Update () {

        timer -= Time.deltaTime;
        if (timer < 0.0f) timer = 0.025f; else return;

        BlitPacket packet = new BlitPacket();
        packet.Append(Network.clientId);
        packet.Append(player.position.x); packet.Append(player.position.y);
        packet.Append(player.position.z);

        packet.Append(cam.rotation.x); packet.Append(cam.rotation.y);
        packet.Append(cam.rotation.z); packet.Append(cam.rotation.w);

        Network.udpClient.Send(0, packet.ToArray());
    }
}
