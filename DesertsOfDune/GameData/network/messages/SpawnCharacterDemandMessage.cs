﻿using System;
using System.Collections.Generic;
using System.Text;
using GameData.network.util.world;
using Newtonsoft.Json;

namespace GameData.network.messages
{
    /// <summary>
    /// This class is used to comunicate the spawn of a character
    /// </summary>
    public class SpawnCharacterDemandMessage : TurnMessage
    {
        [JsonProperty]
        private string characterName;
        [JsonProperty]
        private Position position;
        [JsonProperty]
        private Character attributes;
        private CharacterType characterType;

        /// <summary>
        /// Constructor of the class SpawnCharacterMessage
        /// </summary>
        /// <param name="clientID">the id of the client</param>
        /// <param name="characterID">the id of the character</param>
        /// <param name="characterName">the name of the character</param>
        /// <param name="position">the position of the character</param>
        /// <param name="attributes">the character that should be created</param>
        /// <param name="characterType">the type of the character</param>
        public SpawnCharacterDemandMessage(int clientID, int characterID, string characterName, Position position, Character attributes, CharacterType characterType) : base(characterID,clientID,MessageType.SPAWN_CHARACTER_DEMAND)
        {
            this.characterName = characterName;
            this.position = position;
            this.attributes = attributes;
            this.characterType = characterType;
        }

        public CharacterType GetCharacterTypeFromCharacter()
        {
            return this.characterType;
        }

        public string[] GetAttributesFromCharacter()
        {
            return null;
        }
    }
}