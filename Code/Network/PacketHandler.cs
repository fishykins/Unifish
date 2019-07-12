using System.Collections.Generic;
using Lidgren.Network;

namespace Unifish.Network
{
    /// <summary>
    /// A PacketHandler deals with invoking received network messages on both client and servers
    /// </summary>
    public class PacketHandler
    {
        #region Variables
        public delegate void Packet(NetIncomingMessage message);
        private Dictionary<int, Packet> packetDictionary = new Dictionary<int, Packet>();
        #endregion

        #region Constructors

        #endregion

        #region Public Methods
        public virtual void HandleMessage(NetIncomingMessage message)
        {
            int messageType = message.ReadInt32();
            Packet packet;
            if (packetDictionary.TryGetValue(messageType, out packet))
            {
                packet.Invoke(message);
            }
            else
            {
                Console.LogError("Message type '" + messageType + "' not found- make sure you add it to the packetHandler!");
            }
        }

        public bool TryAddPacket(int id, Packet packet)
        {
            if (packetDictionary.ContainsKey(id)) return false;
            packetDictionary.Add(id, packet);
            return true;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
