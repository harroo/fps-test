
using UnityEngine;

using System.Collections.Generic;

using BlitzBit;
using Drifture;

public class GameManager : MonoBehaviour {

    private void Start () {

        Network.Setup();

        FindObjectOfType<PlayerManager>().Setup();

        DriftureInterface.ConfigureSubmanager();
        DriftureInterface.ConfigureDriftureManager();
        DriftureInterface.ConfigureEntityManager();

        Network.Start();
    }

    private float timer = 1.0f;

    private void Update () {

        Network.tcpClient.RunCallBacks();
        Network.udpClient.RunCallBacks();

        if (timer < 0.0f) { timer = 1.0f;

            DriftureInterface.Simulate();

        } else timer -= Time.deltaTime;
    }

    private void OnDestroy () {

        BlitPacket packet = new BlitPacket();
        packet.Append(Network.clientId);
        packet.Append(DriftureManager.thisName);
        Network.tcpClient.Send(PacketId.PlayerMan_Leave, packet.ToArray());
    }
}
