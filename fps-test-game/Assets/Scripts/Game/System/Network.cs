
using UnityEngine;

using BlitzBit;
using Harasoft;
using Drifture;

public static class Network {

    public static BlitClient tcpClient;
    public static UBlitClient udpClient;

    public static int clientId;

    public static void Setup () {

        tcpClient = new BlitClient();
        tcpClient.useCallBacks = true;
        // tcpClient.onLog = (string e) => { Debug.Log(e); };
        tcpClient.onError = (string e) => { Debug.LogError(e); };

        udpClient = new UBlitClient();
        udpClient.useCallBacks = true;
        // udpClient.onLog = (string e) => { Debug.Log(e); };
        udpClient.onError = (string e) => { Debug.LogError(e); };

        //on recv client id
        tcpClient.AddPacket(PacketId.PlayerMan_YourNameIs, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            clientId = packet.GetInt32();
        });

        //on play sound
        tcpClient.AddPacket(PacketId.PlaySound, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ReverbAudioManager.Play3D(packet.GetString(),
                new Vector3(packet.GetSingle(), packet.GetSingle(), packet.GetSingle())
            );
        });
    }

    public static void Start () {

        tcpClient.Connect(PlayerPrefs.GetString("ADDR"), 4545);
        udpClient.Setup(PlayerPrefs.GetString("ADDR"), 4544);

        BlitPacket joinPacket = new BlitPacket();
        joinPacket.Append(DriftureManager.thisName);
        joinPacket.Append(PlayerPrefs.GetString("USER"));
        tcpClient.Send(PacketId.PlayerMan_Join, joinPacket.ToArray());
    }

    public static void PlaySound (string name, Vector3 pos) {

        BlitPacket packet = new BlitPacket();
        packet.Append(name);
        packet.Append(pos.x); packet.Append(pos.y); packet.Append(pos.z);

        tcpClient.Send(PacketId.PlaySound, packet.ToArray());
    }
}
