using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalolckUI.memory
{
    class Player
    {
        private int pointerPlayer;

        public string Name { get { return Memory.ReadString2(pointerPlayer + offsets.name, 17).Remove(0, 1); } }

        public int Team { get { return Memory.Read<int>(pointerPlayer + offsets.team); } }

        public int Health
        {
            get { return Memory.Read<int>(pointerPlayer + offsets.health); }
            set { Memory.Write<int>(pointerPlayer + offsets.health, value); }
        }

        public Vector3 PositionHead
        {
            get { return Memory.ReadVector3(pointerPlayer + offsets.headPos); }
            set { Memory.WriteVector3(pointerPlayer + offsets.headPos, value); }
        }

        public Vector3 PositionFoot
        {
            get { return Memory.ReadVector3(pointerPlayer + offsets.footPos); }
            set { Memory.WriteVector3(pointerPlayer + offsets.footPos, value); }
        }

        public Vector3 Velocity
        {
            get { return Memory.ReadVector3(pointerPlayer + offsets.velocity); }
            set { Memory.WriteVector3(pointerPlayer + offsets.velocity, value); }
        }

        public float Yaw
        {
            get { return Memory.Read<float>(pointerPlayer + offsets.yaw); }
            set { Memory.Write(pointerPlayer + offsets.yaw, value); }
        }

        public float Pitch
        {
            get { return Memory.Read<float>(pointerPlayer + offsets.pitch); }
            set { Memory.Write(pointerPlayer + offsets.pitch, value); }
        }

        public Weapon weapon;

        public Player(int pointerPlayer)
        {
            this.pointerPlayer = pointerPlayer;
            weapon = new Weapon(pointerPlayer);
        }
    }

    /// <summary>
    /// Weapon object that player is holding.
    /// </summary>
    class Weapon
    {

        private int pointerWeapon;

        public int Ammo
        {
            get { return Memory.Read<int>(pointerWeapon + offsets.ammo); }
            set { Memory.Write<int>(pointerWeapon + offsets.ammo, value); }
        }

        public int AmmoClip
        {
            get { return Memory.Read<int>(pointerWeapon + offsets.ammoClip); }
            set { Memory.Write<int>(pointerWeapon + offsets.ammoClip, value); }
        }

        public int DelayTime
        {
            get { return Memory.Read<int>(pointerWeapon + offsets.delayTime); }
            set { Memory.Write<int>(pointerWeapon + offsets.delayTime, value); }
        }

        public Weapon(int pointerPlayer)
        {
            int pointerCurrentWeapon = Memory.Read<Int32>(pointerPlayer + offsets.ptrCurrentWeapon);
            pointerWeapon = Memory.Read<Int32>(pointerCurrentWeapon + offsets.ptrWeapon);
        }
    }
}
