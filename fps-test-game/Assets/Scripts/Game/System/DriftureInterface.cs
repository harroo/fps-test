
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

        EntityManager.UpdateEntityPosition = (ulong entityId, Vector3 pos, Quaternion rot) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);
            packet.Append(pos.x); packet.Append(pos.y); packet.Append(pos.z);
            packet.Append(rot.x); packet.Append(rot.y); packet.Append(rot.z); packet.Append(rot.w);

            Network.udpClient.Send(PacketId.Drifture_UpdatePos, packet.ToArray());
        };
        EntityManager.EnsureEntityPosition = (ulong entityId, Vector3 pos, Quaternion rot) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);
            packet.Append(pos.x); packet.Append(pos.y); packet.Append(pos.z);
            packet.Append(rot.x); packet.Append(rot.y); packet.Append(rot.z); packet.Append(rot.w);

            Network.tcpClient.Send(PacketId.Drifture_EnsureEntityPosition, packet.ToArray());
        };
        EntityManager.SyncEntityMetaData = (ulong entityId, byte[] metaData) => {

            BlitPacket packet = new BlitPacket();
            packet.Append(entityId);
            packet.Append(metaData);

            Network.tcpClient.Send(PacketId.Drifture_UpdateMetaData, packet.ToArray());
        };
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
        //despawn entity
        Network.tcpClient.AddPacket(PacketId.Drifture_DespawnEntity, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ulong entityId = packet.GetUInt64();

            EntityManager.DespawnEntity(entityId);
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

        //entity pos update
        Network.udpClient.AddPacket(PacketId.Drifture_UpdatePos, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ulong entityId = packet.GetUInt64();

            float px = packet.GetSingle(); float py = packet.GetSingle(); float pz = packet.GetSingle();

            float rx = packet.GetSingle(); float ry = packet.GetSingle();
            float rz = packet.GetSingle(); float rw = packet.GetSingle();

            EntityManager.UpdateTransform(entityId, new Vector3(px, py, pz), new Quaternion(rx, ry, rz, rw));
        });

        //entity pos ensure update
        Network.tcpClient.AddPacket(PacketId.Drifture_EnsureEntityPosition, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            ulong entityId = packet.GetUInt64();

            float px = packet.GetSingle(); float py = packet.GetSingle(); float pz = packet.GetSingle();

            float rx = packet.GetSingle(); float ry = packet.GetSingle();
            float rz = packet.GetSingle(); float rw = packet.GetSingle();

            EntityManager.UpdateTransform(entityId, new Vector3(px, py, pz), new Quaternion(rx, ry, rz, rw));
        });
    }

    public static void Simulate () {

        while (Submanager.SendCount() != 0) {

            byte[] sendData = Submanager.PopSendQueue();

            Network.tcpClient.Send(PacketId.Drifture_PlayerPosUpdate, sendData);
        }
    }
}
