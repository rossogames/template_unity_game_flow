using Rossoforge.UI.Controls.Buttons;
using Rossoforge.UI.Popups.PopupBase;

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
