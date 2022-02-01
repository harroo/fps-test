
using UnityEngine;

using System;
using System.Collections.Generic;

using BlitzBit;
using Harasoft;
using Drifture;

public static class PlayerManager {

    public static List<ClientInfo> clients = new List<ClientInfo>();

    public static int GetIdFromNameId (string nameId) {

        ClientInfo client = Array.Find(clients.ToArray(), e => e.nameId == nameId);
        return client == null ? -1 : client.blitzId;
    }

    public static void Setup () {

        //on player join
        Network.tcpServer.AddPacket(PacketId.PlayerMan_Join, (int sender, byte[] rb) => {
                BlitPacket packet = new BlitPacket(rb);

            // Create and cache new client.
            ClientInfo newClient = new ClientInfo {

                blitzId = sender,
                nameId = packet.GetString(),
                username = packet.GetString(),
            };
            clients.Add(newClient);

            // Send Id to new client so that they're aware of who they are.
            BlitPacket infoPacket = new BlitPacket();
            infoPacket.Append(sender);
            Network.tcpServer.RelayTo(PacketId.PlayerMan_YourNameIs, sender, infoPacket.ToArray());

            // Send currently online players to new player.
            foreach (var client in clients) {

                if (client.blitzId == sender) continue;

                BlitPacket joinPacket = new BlitPacket();
                joinPacket.Append(client.blitzId);
                joinPacket.Append(client.username);
                Network.tcpServer.RelayTo(PacketId.PlayerMan_Join, sender, joinPacket.ToArray());
            }

            // Send new player to all current players.
            BlitPacket relayPacket = new BlitPacket();
            relayPacket.Append(newClient.blitzId);
            relayPacket.Append(newClient.username);
            Network.tcpServer.RelayExclude(PacketId.PlayerMan_Join, relayPacket.ToArray(), sender);

            Logging.Log("`" + newClient.username + "' joined the Server!");
        });

        //on player pos ensure update
        Network.tcpServer.AddPacket(PacketId.PlayerMan_Move, (int sender, byte[] b) => {

            Network.tcpServer.RelayExclude(PacketId.PlayerMan_Move, b, sender);
        });

        //on player leave
        Network.tcpServer.AddPacket(PacketId.PlayerMan_Leave, (int a, byte[] b)=>{HandleDisconnect(a);});
        //backup player leave
        Network.tcpServer.onClientDisconnect = HandleDisconnect;
    }

    private static void HandleDisconnect (int clientId) {

        ClientInfo client = Array.Find(clients.ToArray(), e => e.blitzId == clientId);
        if (client == null) return;

        clients.Remove(client);
        Submanager.ClearPlayerCache(client.nameId);

        BlitPacket relayPacket = new BlitPacket();
        relayPacket.Append(client.blitzId);
        Network.tcpServer.RelayExclude(PacketId.PlayerMan_Leave, relayPacket.ToArray(), clientId);

        Logging.Log("`" + client.username + "' left the Server..");
    }

    public class ClientInfo {

        public int blitzId;
        public string nameId;
        public string username;
    }
}
