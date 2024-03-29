
using UnityEngine;

using BlitzBit;
using Harasoft;

public static class Network {

    public static BlitServer tcpServer;
    public static UBlitServer udpServer;

    public static void Setup () {

        tcpServer = new BlitServer();
        tcpServer.useCallBacks = true;
        // tcpServer.onLog = (string e) => { Logging.Log(e); };
        tcpServer.onError = (string e) => { Logging.Log(" [ERROR]: " + e); };

        udpServer = new UBlitServer();
        udpServer.useCallBacks = true;
        // udpServer.onLog = (string e) => { Logging.Log(e); };
        udpServer.onError = (string e) => { Logging.Log(" [ERROR]: " + e); };

        tcpServer.onUnknownPacket = (int sender, int packetId, byte[] e) => {

            tcpServer.RelayAll(packetId, e);
        };
    }

    public static void Start () {

        tcpServer.Start(4545);
        udpServer.Start(4544);
    }
}
