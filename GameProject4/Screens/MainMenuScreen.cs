using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameProject4.StateManagement;
using System.Windows.Forms;

namespace GameProject4.Screens
{


    // The main menu screen is the first thing displayed when the game starts up.
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen() : base("Gold Rush")
        {
            var playGameMenuEntry = new MenuEntry("Play Game");
            var controlsEntry = new MenuEntry("Controls");
            var exitMenuEntry = new MenuEntry("Exit");

            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            controlsEntry.Selected += ControlsEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(controlsEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            
           if (ScreenManager.gameState == GameState.LevelOne) LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new LevelOneScreen());
        }

        private void ControlsEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new Controls(), e.PlayerIndex);
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";
            var confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}
