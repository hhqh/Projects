﻿using System;
using System.Collections.ObjectModel;

using Game.Models;
using Game.Engine.EngineInterfaces;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class BattleEngineViewModel
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile BattleEngineViewModel instance;
        private static readonly object syncRoot = new Object();

        public static BattleEngineViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new BattleEngineViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        // The Battle Engine
        public IBattleEngineInterface Engine = new Engine.EngineGame.BattleEngine();

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngine = new Engine.EngineGame.AutoBattleEngine();

        // Hold the Proposed List of Characters for the Battle to Use
        public ObservableCollection<CharacterModel> PartyCharacterList { get; set; } = new ObservableCollection<CharacterModel>();

        // Hold the View Model to the CharacterIndexViewModel
        public CharacterIndexViewModel DatabaseCharacterViewModel = CharacterIndexViewModel.Instance;

        // Have the Database Character List point to the Character View Model List
        public ObservableCollection<CharacterModel> DatabaseCharacterList { get; set; } = CharacterIndexViewModel.Instance.Dataset;

        // Background image
        public string BackgroundImage { get; set; } = "battle_background.png";

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel()
        {
        }

        #endregion Constructor
    }
}