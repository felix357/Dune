﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Server.Configuration
{
    /// <summary>
    /// This class stores the scenario configuration and is used for loading the scenario configuration file.
    /// </summary>
    public class ScenarioConfiguration
    {
        [JsonProperty]
        public List<List<string>> scenario { get; set; }

        private static ScenarioConfiguration singleton;

        /// <summary>
        /// hide default constructor for implementing the singleton pattern
        /// </summary>
        /// TODO: set default values, if a new instance is created
        private ScenarioConfiguration()
        {
        }

        /// <summary>
        /// get the reference to the scenario configuration class (implementation of the singleton pattern)
        /// </summary>
        /// <returns>reference to this class</returns>
        public static ScenarioConfiguration GetInstance()
        {
            if (singleton == null)
            {
                singleton = new ScenarioConfiguration();
            }
            return singleton;
        }

        /// <summary>
        /// creates a new (singleton) instance based on an existing ScenarioConfiguration-Object
        /// </summary>
        /// <param name="partyConfigObject">the object to "copy"</param>
        public static void CreateInstance(ScenarioConfiguration scenarioConfigObject)
        {
            singleton = (ScenarioConfiguration)scenarioConfigObject.MemberwiseClone();
        }
    }
}