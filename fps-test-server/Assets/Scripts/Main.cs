
using UnityEngine;

using System.Threading;
using System.Collections.Generic;

using BlitzBit;
using Drifture;
using Harasoft;

public class Main : MonoBehaviour {

    private void Start () {

        Logging.StartNewLog();
        Logging.Log("Starting FPS-Test Server..");

        Logging.Log("Configure Shutdown-Event..");
        Application.quitting += OnShutdown;

        Logging.Log("Setup Network..");
        Network.Setup();

        Logging.Log("Configure Entity-System..");
        DriftureInterface.ConfigureSubmanager();
        DriftureInterface.ConfigureEntityManager();

        Logging.Log("Setup Player-Management..");
        PlayerManager.Setup();

        Logging.Log("Start Network..");
        Network.Start();
    }

    private float timer = 1.0f;

    private void Update () {

        Network.tcpServer.RunCallBacks();
        Network.udpServer.RunCallBacks();

        if (timer < 0.0f) { timer = 1.0f;

            DriftureInterface.Simulate();

        } else timer -= Time.deltaTime;
    }

    private void OnDestroy () { OnShutdown(); }

    private void OnShutdown () {

        Logging.Log("Shutting Down..");

        Network.tcpServer.Stop();
        Network.udpServer.Stop();

        Logging.Log("Shutdown.");
        Logging.Log("Exit.");
    }
}
