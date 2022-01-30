
using UnityEngine;

using Drifture;
using BlitzBit;

public static class DriftureInterface {

    public static void ConfigureSubmanager () {

        //on control update
        Network.tcpClient.AddPacket(PacketId.Drifture_EntityControlUpdate, (byte[] e) => {

            Submanager.UpdateControl(e);
        });
    }

    public static void ConfigureDriftureManager () {

        //set name id
        DriftureManager.thisName = Random.Range(111111, 999999).ToString();

        //setup events
        DriftureManager.InteractEntity = (ulong entityId, object sender) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);
            packet.AppendT(sender);

            Network.tcpClient.Send(PacketId.Drifture_Interact, packet.ToArray());
        };
        DriftureManager.AttackEntity = (ulong entityId, int damage, object sender) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);
            packet.Append(damage);
            packet.AppendT(sender);

            Network.tcpClient.Send(PacketId.Drifture_Attack, packet.ToArray());
        };
        DriftureManager.CreateEntity = (int type, Vector3 position, byte[] metaData) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(type);
            packet.Append(position.x); packet.Append(position.y); packet.Append(position.z);
            packet.Append(metaData);

            Network.tcpClient.Send(PacketId.Drifture_Create, packet.ToArray());
        };
        DriftureManager.DeleteEntity = (ulong entityId) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);

            Network.tcpClient.Send(PacketId.Drifture_Delete, packet.ToArray());
        };
    }

    public static void ConfigureEntityManager () {

        //update metadata
        Network.tcpClient.AddPacket(PacketId.Drifture_UpdateMetaData, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ulong entityId = packet.GetUInt64();
            byte[] metaData = packet.GetByteArray();

            EntityManager.UpdateMetaData(entityId, metaData);
        });
        //spawn entity
        Network.tcpClient.AddPacket(PacketId.Drifture_SpawnEntity, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ulong entityId = packet.GetUInt64();
            int type = packet.GetInt32();
            Vector3 position = new Vector3(packet.GetSingle(), packet.GetSingle(), packet.GetSingle());
            Quaternion rotation = new Quaternion(packet.GetSingle(), packet.GetSingle(), packet.GetSingle(), packet.GetSingle());
            byte[] metaData = packet.GetByteArray();

            EntityManager.SpawnEntity(entityId, type, position, rotation, metaData);
        });
        //interact entity
        Network.tcpClient.AddPacket(PacketId.Drifture_Interact, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ulong entityId = packet.GetUInt64();
            object sender = packet.GetObject();

            EntityManager.InteractEntity(entityId, sender);
        });
        //attack entity
        Network.tcpClient.AddPacket(PacketId.Drifture_Attack, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ulong entityId = packet.GetUInt64();
            int damage = packet.GetInt32();
            object sender = packet.GetObject();

            EntityManager.AttackEntity(entityId, damage, sender);
        });
    }

    public static void Simulate () {

        if (Submanager.SendCount() != 0) {

            byte[] sendData = Submanager.PopSendQueue();

            Network.tcpClient.Send(PacketId.Drifture_PlayerPosUpdate, sendData);
        }
    }
}
