
using UnityEngine;

using System;

using BlitzBit;

public class ProjectileManager : MonoBehaviour {

    public Projectile[] projectiles;

    public void Setup () {

        //on projectile fired
        Network.tcpClient.AddPacket(PacketId.Projectile_Shoot, (byte[] e) => {
                BlitPacket packet = new BlitPacket(e);

            int id = packet.GetInt32();
            Vector3 pos = new Vector3(packet.GetSingle(), packet.GetSingle(), packet.GetSingle());
            Quaternion rot = new Quaternion(packet.GetSingle(), packet.GetSingle(), packet.GetSingle(), packet.GetSingle());

            Instantiate(Array.Find(projectiles, c => c.id == id).prefab, pos, rot);
        });
    }

    public void FireProjectile (int id, Vector3 position, Quaternion rotation) {

        BlitPacket packet = new BlitPacket();
        packet.Append(id);
        packet.Append(position.x); packet.Append(position.y); packet.Append(position.z);

        packet.Append(rotation.x); packet.Append(rotation.y);
        packet.Append(rotation.z); packet.Append(rotation.w);

        Network.tcpClient.Send(PacketId.Projectile_Shoot, packet.ToArray());
    }
}

[Serializable]
public class Projectile {

    public int id;
    public GameObject prefab;
}
