
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

        //spawn test entity
            DriftureManager.CreateEntity(0, new Vector3(Random.Range(-8, 8), Random.Range(3, 8), Random.Range(-8, 8)), new byte[0]{});
    }

    private void Update () {

        Network.tcpClient.RunCallBacks();
        Network.udpClient.RunCallBacks();

        DriftureInterface.Simulate();
    }

    private void OnDestroy () {

        BlitPacket packet = new BlitPacket();
        packet.Append(Network.clientId);
        packet.Append(DriftureManager.thisName);
        Network.tcpClient.Send(PacketId.PlayerMan_Leave, packet.ToArray());
    }
}
