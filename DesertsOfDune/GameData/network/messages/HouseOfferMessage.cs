﻿using System;
using System.Collections.Generic;
using System.Text;
using GameData.network.util.world;
using Newtonsoft.Json;

namespace GameData.network.messages
{
    /// <summary>
    /// This class is used to comunicate the Houses that are offered by the server.
    /// </summary>
    public class HouseOfferMessage : ClientServerMessage
    {
        [JsonProperty]
        public GreatHouse[] houses { get; }

        /// <summary>
        /// Constructor of the class ClientServerMessage
        /// </summary>
        /// <param name="clientID">the id of the client</param>
        /// <param name="houses">the houses that the server offers the client.</param>
        public HouseOfferMessage(int clientID, GreatHouse[] houses) : base(clientID,MessageType.HOUSE_OFFER)
        {
            this.houses = houses;
        }
    }
}
