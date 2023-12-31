﻿using GameData.Configuration;
using GameData.network.messages;
using GameData.network.util.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameData.network.util.world.character
{
    /// <summary>
    /// This Class represents the character type Nobel
    /// </summary>
    public class Noble : Character
    {
        /// <summary>
        /// This is the Constructor of the Class Nobel
        /// </summary>
        /// <param name="healthMax">the maximum health of the Character</param>
        /// <param name="healthCurrent">the current healthpoints of the Character</param>
        /// <param name="healingHP">the healingHealthPoints of the Character</param>
        /// <param name="MPmax">the maximum movementpoints of the Character</param>
        /// <param name="MPcurrent">the current MovementPoints of the Character</param>
        /// <param name="APmax">the maximum Ability Points of the Character</param>
        /// <param name="APcurrent">the current Ability Points of the Character</param>
        /// <param name="attackDamage">the atack damage of the Character</param>
        /// <param name="inventorySize">the inventorySize of the Character</param>
        /// <param name="inventoryUsed">the usedup InventorySpace of the Character</param>
        /// <param name="killedBySandworm">true, if the Character was killed by the sandworm</param>
        /// <param name="isLoud">true, if the character is loud</param>
        public Noble(int healthMax, int healthCurrent, int healingHP, int MPmax, int MPcurrent, int APmax, int APcurrent, int attackDamage, int inventorySize, int inventoryUsed, bool killedBySandworm, bool isLoud) : base(CharacterType.NOBLE, healthMax, healthCurrent, healingHP,  MPmax, MPcurrent, APmax, APcurrent, attackDamage, inventorySize, inventoryUsed, killedBySandworm, isLoud)
        {

        }

        /// <summary>
        /// creates a new nobel 
        /// </summary>
        /// <param name="name">the name of the nobel</param>
        public Noble(string name) : base(CharacterType.NOBLE, CharacterConfiguration.Noble.maxHP, CharacterConfiguration.Noble.maxMP, CharacterConfiguration.Noble.maxAP, CharacterConfiguration.Noble.damage, CharacterConfiguration.Noble.inventorySize, CharacterConfiguration.Noble.healingHP, name) {
        }

        /// <summary>
        /// This method resets the data of the character
        /// </summary>
        public override void ResetData()
        {
            this.characterType = Enum.GetName(typeof(CharacterType), CharacterType.NOBLE);
            this.healthMax = CharacterConfiguration.Noble.maxHP;
            this.healthCurrent = CharacterConfiguration.Noble.maxHP;
            this.HealingHP = CharacterConfiguration.Noble.healingHP;
            this.MPmax = CharacterConfiguration.Noble.maxMP;
            this.MPcurrent = CharacterConfiguration.Noble.maxMP;
            this.APmax = CharacterConfiguration.Noble.maxAP;
            this.APcurrent = CharacterConfiguration.Noble.maxAP;
            this.attackDamage = CharacterConfiguration.Noble.damage;
            this.inventorySize = CharacterConfiguration.Noble.inventorySize;
            this.inventoryUsed = 0;
            this.killedBySandworm = false;
            this.isLoud = false;
        }

        /// <summary>
        /// This method represents the action Kanly for the Character type Nobel
        /// </summary>
        /// <param name="target">the Nobel that is targeted by the atack</param>
        /// <returns>true, if action was successful</returns>
        public override bool Kanly(Character target)
        {
            if ("NOBLE".Equals(target.characterType))
            {
                int dist = Math.Abs(target.CurrentMapfield.XCoordinate - CurrentMapfield.XCoordinate) + Math.Abs(target.CurrentMapfield.ZCoordinate - CurrentMapfield.ZCoordinate);
                if (dist <= 2 && target.greatHouse != this.greatHouse && this.APcurrent == this.APmax)
                {
                    Random random = new Random();
                    double randomValue = random.NextDouble();
                    if (PartyConfiguration.GetInstance().kanlySuccessProbability >= randomValue)
                    {
                        target.DecreaseHP(target.healthCurrent);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// information if the Great House Convention is already broken
        /// </summary>
        public static bool greatHouseConventionBroken { get; set; }

        /// <summary>
        /// This method represents the action FamilyAtomic
        /// </summary>
        /// <param name="target">The target Field for the Attack</param>
        /// <param name="map">the current map</param>
        /// <param name="greatHouseConventionBroken">information if the greatHouseConvention was already broken before this atomic was thrown</param>
        /// <param name="activePlayerGreatHouse">the great house of the player who did throw the atomic</param>
        /// <param name="passivePlayerGreatHouse">the great house of the player who didn't throw the atomic</param>
        /// <returns>characters hit by the atomic Bomb</returns>
        public override List<Character> AtomicBomb(MapField target, Map map, bool greatHouseConventionBroken, GreatHouse activePlayerGreatHouse, GreatHouse passivePlayerGreatHouse)
        {
            List<Character> charactersHit = new List<Character>();
            if(this.APcurrent == this.APmax && this.greatHouse.unusedAtomicBombs > 0)
            {
                bool breakGreatHouseConvention = false;                 //information if this is the atomic bomb which breaks the greatHouseConvention
                var mapFields = map.GetNeighborFields(target);
                mapFields.Add(target);

                foreach (var mapfield in mapFields)
                {
                    //change dune to flat sand and mountains to plateus
                    switch (Enum.Parse(typeof(TileType), mapfield.tileType))
                    {
                        case TileType.DUNE:
                            mapfield.tileType = Enum.GetName(typeof(TileType), TileType.FLAT_SAND);
                            break;
                        case TileType.MOUNTAINS:
                            mapfield.tileType = Enum.GetName(typeof(TileType), TileType.PLATEAU);
                            break;
                    }
                    //check if the atomic hits characters
                    if (mapfield.IsCharacterStayingOnThisField)
                    {
                        charactersHit.Add(mapfield.Character);
                        mapfield.Character.DecreaseHP(mapfield.Character.healthCurrent);
                        //if the greatHouseConvention is not already broken this atomic breaks it
                        if(!greatHouseConventionBroken)
                        {
                            Noble.greatHouseConventionBroken = true;
                            breakGreatHouseConvention = true;
                        }
                    }
                    //if atomic hits spice remove it
                    if (mapfield.hasSpice)
                    {
                        mapfield.hasSpice = false;
                    }
                }
                SpentAp(APmax);
                this.greatHouse.unusedAtomicBombs--;
                //get the great houses which are not used by any player
                if (breakGreatHouseConvention)
                {
                    this.Shunned = true;
                    List<GreatHouse> remainingGreatHouses = new List<GreatHouse>();
                    if (activePlayerGreatHouse.houseName != "CORRINO" && passivePlayerGreatHouse.houseName != "CORRINO")
                    {
                        remainingGreatHouses.Add(new Corrino());
                    }
                    if (activePlayerGreatHouse.houseName != "ATREIDES" && passivePlayerGreatHouse.houseName != "ATREIDES")
                    {
                        remainingGreatHouses.Add(new Atreides());
                    }
                    if (activePlayerGreatHouse.houseName != "HARKONNEN" && passivePlayerGreatHouse.houseName != "HARKONNEN")
                    {
                        remainingGreatHouses.Add(new Harkonnen());
                    }
                    if (activePlayerGreatHouse.houseName != "ORDOS" && passivePlayerGreatHouse.houseName != "ORDOS")
                    {
                        remainingGreatHouses.Add(new Ordos());
                    }
                    if (activePlayerGreatHouse.houseName != "RICHESE" && passivePlayerGreatHouse.houseName != "RICHESE")
                    {
                        remainingGreatHouses.Add(new Richese());
                    }
                    if (activePlayerGreatHouse.houseName != "VERNIUS" && passivePlayerGreatHouse.houseName != "VERNIUS")
                    {
                        remainingGreatHouses.Add(new Vernius());
                    }
                    //select a random character from each unused great house
                    Random rnd = new Random();
                    CharactersToAddAfterAtomics = new List<Character>();
                    foreach (var greatHouse in remainingGreatHouses)
                    {
                        int randomCharacterIndex = rnd.Next(greatHouse.Characters.Count);
                        
                        CharactersToAddAfterAtomics.Add(greatHouse.Characters[randomCharacterIndex]);
                    }
                }
            }
            return charactersHit;
        }
    }
}
