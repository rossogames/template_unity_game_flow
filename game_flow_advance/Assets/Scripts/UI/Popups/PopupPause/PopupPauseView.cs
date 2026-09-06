using Rossoforge.Controls.Buttons;
using Rossoforge.Popups.UI;

namespace RossoGames.Popups.PopupPause
{
    public class PopupPauseView : PopupView<PopupPauseView, PopupPausePresenter, PopupPauseData>,
        IButtonClickListener<PopupPauseButtonContinue>,
        IButtonClickListener<PopupPauseButtonGoMain>
    {
        protected override void Awake()
        {
            base.Awake();
            base.Presenter = new PopupPausePresenter(this);
        }

        public void OnClick(ButtonEventArg<PopupPauseButtonContinue> eventArg)
        {
            Close();
        }

        public void OnClick(ButtonEventArg<PopupPauseButtonGoMain> eventArg)
        {
            Presenter.GoToMainMenu();
        }
    }
}
