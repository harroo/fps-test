
using UnityEngine;

using Drifture;
using BlitzBit;

public static class DriftureInterface {

    public static void ConfigureSubmanager () {

        //on player pos update from drifture client
        Network.tcpServer.AddPacket(PacketId.Drifture_PlayerPosUpdate, (int sender, byte[] rb) => {

            Submanager.UpdatePlayerPos(rb);
        });
    }

    public static void ConfigureEntityManager () {

        //on entity interact
        Network.tcpServer.AddPacket(PacketId.Drifture_Interact, (int sender, byte[] rb) => {

            Network.tcpServer.RelayAll(PacketId.Drifture_Interact, rb);
        });
        //on entity attack
        Network.tcpServer.AddPacket(PacketId.Drifture_Attack, (int sender, byte[] rb) => {

            Network.tcpServer.RelayAll(PacketId.Drifture_Attack, rb);
        });
        //on create entity
        Network.tcpServer.AddPacket(PacketId.Drifture_Create, (int sender, byte[] rb) => {
                BlitPacket packet = new BlitPacket(rb);

            int type = packet.GetInt32();
            Vector3 position = new Vector3(packet.GetSingle(), packet.GetSingle(), packet.GetSingle());
            byte[] metaData = packet.GetByteArray();

            EntityManager.CreateEntity(type, position, metaData);
        });
        //on delete entity
        Network.tcpServer.AddPacket(PacketId.Drifture_Delete, (int sender, byte[] rb) => {
                BlitPacket packet = new BlitPacket(rb);

            ulong entityId = packet.GetUInt64();
            EntityManager.DeleteEntity(entityId);
        });
        //on ensure entity pos
        Network.tcpServer.AddPacket(PacketId.Drifture_EnsureEntityPosition, (int sender, byte[] rb) => {
                BlitPacket packet = new BlitPacket(rb);

            ulong entityId = packet.GetUInt64();

            float px = packet.GetSingle(); float py = packet.GetSingle(); float pz = packet.GetSingle();
            float rx = packet.GetSingle(); float ry = packet.GetSingle();
            float rz = packet.GetSingle(); float rw = packet.GetSingle();

            EntityManager.UpdateTransform(entityId, new Vector3(px, py, pz), new Quaternion(rx, ry, rz, rw));
        });
        //on update meta data
        Network.tcpServer.AddPacket(PacketId.Drifture_UpdateMetaData, (int sender, byte[] rb) => {
                BlitPacket packet = new BlitPacket(rb);

            ulong entityId = packet.GetUInt64();
            byte[] metaData = packet.GetByteArray();

            EntityManager.UpdateMetaData(entityId, metaData);

            Network.tcpServer.RelayAll(PacketId.Drifture_UpdateMetaData, rb);
        });
        EntityManager.SpawnEntity = (ulong entityId, int type, Vector3 pos, Quaternion rot, byte[] metaData) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);
            packet.Append(type);
            packet.Append(pos.x); packet.Append(pos.y); packet.Append(pos.z);
            packet.Append(rot.x); packet.Append(rot.y); packet.Append(rot.z); packet.Append(rot.w);
            packet.Append(metaData);

            Network.tcpServer.RelayAll(PacketId.Drifture_SpawnEntity, packet.ToArray());
        };
        EntityManager.SpawnEntityTo = (ulong entityId, int type, Vector3 pos, Quaternion rot, byte[] metaData, string targetNameId) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);
            packet.Append(type);
            packet.Append(pos.x); packet.Append(pos.y); packet.Append(pos.z);
            packet.Append(rot.x); packet.Append(rot.y); packet.Append(rot.z); packet.Append(rot.w);
            packet.Append(metaData);

            Network.tcpServer.RelayTo(PacketId.Drifture_SpawnEntity, PlayerManager.GetIdFromNameId(targetNameId), packet.ToArray());
        };
        EntityManager.DespawnEntity = (ulong entityId) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);

            Network.tcpServer.RelayAll(PacketId.Drifture_DespawnEntity, packet.ToArray());
        };
        EntityManager.DespawnEntityTo = (ulong entityId, string targetNameId) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);

            Network.tcpServer.RelayTo(PacketId.Drifture_DespawnEntity, PlayerManager.GetIdFromNameId(targetNameId), packet.ToArray());
        };
    }

    public static void Simulate () {

        if (Submanager.SendCount() != 0) {

            byte[] sendData = Submanager.PopSendQueue();

            Network.tcpServer.RelayAll(PacketId.Drifture_EntityControlUpdate, sendData);
        }

        Submanager.RunChecks();
    }
}
