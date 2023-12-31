﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GameData.network.messages
{
    /// <summary>
    /// This Class is used as a message to request a pause of or to request continuation of the game
    /// </summary>
    public class PauseGameRequestMessage : Message
    {
        [JsonProperty]
        public bool pause { get; set; }

        /// <summary>
        /// Constructor of the class PauseGameRequestMessage
        /// </summary>
        /// <param name="pause">true, if client wishes to pause the game if he wishes to continue false</param>
        public PauseGameRequestMessage(bool pause) : base("1.1", MessageType.PAUSE_REQUEST)
        {
            this.pause = pause;
        }
    }
}
