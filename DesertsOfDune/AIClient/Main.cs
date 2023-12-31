﻿using System;
using System.Threading;
using AIClient.Configuration;
using AIClient.parser.commandLineParser;
using GameData.network.controller;
using GameData.network.util;
using Serilog;

namespace AIClient
{
    /// <summary>
    /// The main class for configuring and starting the ai client.
    /// </summary>
    /// <remarks>
    /// The ai client needs command line arguments for configuration. Especially it needs the
    /// address of the server and ideally the port (otherwise the standard port is used). It is assumed, that the
    /// server address is correct and represents a running server, because otherwise the ai client cannot start is shut down. \n
    /// For further information see <see cref="Main(string[])"/>
    /// </remarks>
    static class Programm
    {
        /// <summary>
        /// the message controller used by this ai client
        /// </summary>
        private static AIPlayerMessageController messageController;

        /// <summary>
        /// the configuration of the ai client, given by the command line arguements
        /// </summary>
        public static AIClientConfiguration Configuration { get; private set; }

        /// <summary>
        /// main method, which is executed when the ai client was started / executed
        /// It will initialize all classes and trigger all starting events including
        /// <list type="bullet">
        /// <item>create and configure logger</item>
        /// <item>create and configure network module</item>
        /// <item>connect to server</item>
        /// </list>
        /// </summary>
        /// <param name="args">arguments for starting the server (see Commandline args in "Standardisierungskomitee")</param>
        static void Main(string[] args)
        {
            // wait 4 seconds for configuring everything
            Thread.Sleep(4000);

            InitiliazeLogger();
            Log.Information("Starting ai client... Everything will be created and configured.");

            ParseCommandLineArguments(args);
            Log.Debug("Parsed the command line arguments and configuring the ai client");

            CreateNetworkModule();
            Log.Debug("Created network module in ai client and connect to the server");

            // create a new party and initialize the game
            Party.CreateParty(messageController);
            Party.GetInstance().JoinParty(Configuration.Name);
            Log.Debug("Created a new party and initialized the join");
        }

        /// <summary>
        /// creates and configures a new logger for the ai client
        /// </summary>
        private static void InitiliazeLogger()
        {
            Logger logger = new Logger();
            logger.CreateDebugLogger();
        }

        /// <summary>
        /// creates and configure the network module for the ai client
        /// </summary>
        private static void CreateNetworkModule()
        {
            messageController = new AIPlayerMessageController();

            ClientConnectionHandler aiClientConnectionHandler = new ClientConnectionHandler(Configuration.Address, Configuration.Port);
            _ = new ClientNetworkController(aiClientConnectionHandler, messageController);
        }

        /// <summary>
        /// parsing the command line arguments giving when started the ai client and configure the ai client
        /// </summary>
        /// <param name="args"></param>
        private static void ParseCommandLineArguments(String[] args)
        {
            CommandLineParser parser = new CommandLineParser();
            bool wasSuccessfullyParsed = parser.ParseCommandLineArguments(args);
            if (wasSuccessfullyParsed)
            {
                Log.Debug("The command line arguments of the ai client were parsed sucessfully");

                // get the configuration data
                Configuration = parser.Configuration;
            }
            else
            {
                Log.Fatal("The given command line arguments contains errors and cannot be processed. So restart the ai client with correct arguments.");
                Environment.Exit(0);
            }
        }
    }
}
