using Rossoforge.UI.Popups.PopupBase;

namespace RossoGames.Popups.PopupPause
{
    public class PopupPausePresenter : PopupPresenter<PopupPauseView, PopupPausePresenter, PopupPauseData>
    {
        public PopupPausePresenter(PopupPauseView view) : base(view)
        {
        }

        public void GoToMainMenu()
        {
            Data.ReturnToMain = true;
            View.Close();
        }
    }
}
