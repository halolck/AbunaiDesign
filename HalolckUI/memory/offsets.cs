using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalolckUI.memory
{
    internal class offsets
    { 
        public const int baseGame = 0x0050F4E8;
        public const int viewMatrix = 0x00501AE8;

        //pointer to players
        //ptrPlayerEnitity -> variableOffset
        public const int ptrPlayerEntity = 0x0C;
        public const int ptrPlayerArray = 0x10;
        public const int numPlayers = 0x18; //size of ptrPlayerArray
        //player variables
        public const int name = 0x0224;
        public const int team = 0x032C;
        public const int headPos = 0x04;
        public const int footPos = 0x34;
        public const int velocity = 0x10;
        public const int yaw = 0x40;
        public const int pitch = 0x44;
        public const int health = 0xF8;


        //weapon pointers
        //ptrPlayerEntity -> ptrCurrentWeapon -> ptrWeapon -> variableOffset
        public const int ptrCurrentWeapon = 0x0378;
        public const int ptrWeapon = 0x10;
        //weapon variables
        public const int ammo = 0x28;
        public const int ammoClip = 0x00;
        public const int delayTime = 0x50;
    }
}
