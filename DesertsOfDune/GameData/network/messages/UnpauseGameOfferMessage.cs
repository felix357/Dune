﻿using System;
using Newtonsoft.Json;

namespace GameData.network.messages
{
    public class UnpauseGameOfferMessage : Message
    {
        [JsonProperty]
        public int requestedByClientID { get; }

        /// <summary>
        /// Message to finish the pause and continue the game.
        /// </summary>
        /// <param name="requestedByClientID">ID from client, who requested unpause</param>
        public UnpauseGameOfferMessage(int requestedByClientID) : base("1.1", MessageType.UNPAUSE_GAME_OFFER)
        {
            this.requestedByClientID = requestedByClientID;
        }
    }
}
