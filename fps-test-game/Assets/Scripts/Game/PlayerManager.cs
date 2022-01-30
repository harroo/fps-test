
using UnityEngine;

using System.Collections.Generic;

using BlitzBit;

public class PlayerManager : MonoBehaviour {

    public GameObject playerPrefab;

    private Dictionary<int, GameObject> playerCache
        = new Dictionary<int, GameObject>();

    public void Setup () {

        //on player join
        Network.tcpClient.AddPacket(PacketId.PlayerMan_Join, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            int playerId = packet.GetInt32();
            string playerName = packet.GetString();

            if (playerCache.ContainsKey(playerId) || playerId == Network.clientId) return;

            playerCache.Add(playerId,
                Instantiate(playerPrefab, new Vector3(0,-100,0), Quaternion.identity)
            );
        });

        //on player leave
        Network.tcpClient.AddPacket(PacketId.PlayerMan_Leave, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            int playerId = packet.GetInt32();

            if (!playerCache.ContainsKey(playerId) || playerId == Network.clientId) return;

            Destroy(playerCache[playerId]);
            playerCache.Remove(playerId);
        });

        //on player pos update
        Network.udpClient.AddPacket(PacketId.PlayerMan_Move, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            int id = packet.GetInt32();
            if (Network.clientId == id) return;

            float px = packet.GetSingle(); float py = packet.GetSingle();
            float pz = packet.GetSingle();

            float rx = packet.GetSingle(); float ry = packet.GetSingle();
            float rz = packet.GetSingle(); float rw = packet.GetSingle();

            if (!playerCache.ContainsKey(id)) return;

            playerCache[id].transform.position = new Vector3(px, py, pz);
            playerCache[id].transform.rotation = new Quaternion(rx, ry, rz, rw);
        });
    }
}
