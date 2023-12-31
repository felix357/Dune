﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GameData.network.messages
{
    /// <summary>
    /// This class is used to communicate the paus of the game.
    /// </summary>
    public class GamePauseDemandMessage : Message
    {
        [JsonProperty]
        public int requestedByClientID { get; }
        [JsonProperty]
        public bool pause { get; }

        /// <summary>
        /// Constructor of the class PauseGameMessage
        /// </summary>
        /// <param name="requestedByClientID">the client id</param>
        /// <param name="pause">true if the game is paused.</param>
        public GamePauseDemandMessage(int requestedByClientID, bool pause) : base("1.1", MessageType.GAME_PAUSE_DEMAND)
        {
            this.pause = pause;
            this.requestedByClientID = requestedByClientID;
        }
    }
}
