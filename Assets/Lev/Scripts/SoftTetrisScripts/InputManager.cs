namespace SoftTetris
{
    public class InputManager
    {
        public PlayerActions PlayerActions { get; }

        public InputManager() => PlayerActions = new PlayerActions();

        public void EnablePlayersControls() => PlayerActions.Enable();

        public void DisablePlayerControls() => PlayerActions.Disable();

    }
}