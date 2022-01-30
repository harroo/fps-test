
using UnityEngine;

using System.Collections.Generic;

using BlitzBit;
using Drifture;

public class GameManager : MonoBehaviour {

    private void Start () {

        Network.Setup();

        FindObjectOfType<PlayerManager>().Setup();
        FindObjectOfType<ProjectileManager>().Setup();

        DriftureInterface.ConfigureSubmanager();
        DriftureInterface.ConfigureDriftureManager();
        DriftureInterface.ConfigureEntityManager();

        Network.Start();
    }

    private void Update () {

        Network.tcpClient.RunCallBacks();
        Network.udpClient.RunCallBacks();

        DriftureInterface.Simulate();

        if (Input.GetKeyUp(KeyCode.Escape))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    private void OnDestroy () {

        BlitPacket packet = new BlitPacket();
        packet.Append(Network.clientId);
        packet.Append(DriftureManager.thisName);
        Network.tcpClient.Send(PacketId.PlayerMan_Leave, packet.ToArray());
    }
}
