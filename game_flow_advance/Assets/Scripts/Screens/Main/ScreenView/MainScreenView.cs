using Rossoforge.UI.Controls.Buttons;
using Rossoforge.UI.Screens.ScreenBase;

namespace RossoGames.Main.ScreenView
{
    public class MainScreenView : ScreenView<MainScreenView, MainScreenPresenter>,
        IButtonClickListener<MainScreenButtonStart>,
        IButtonClickListener<MainScreenButtonSettings>,
        IButtonClickListener<MainScreenButtonExit>
    {
        protected override void Awake()
        {
            base.Awake();
            Presenter = new MainScreenPresenter(this);
        }

        public void OnClick(ButtonEventArg<MainScreenButtonStart> eventArg)
        {
            Presenter.PlayGame();
        }

        public void OnClick(ButtonEventArg<MainScreenButtonSettings> eventArg)
        {
            Presenter.OpenSettingsPopup();
        }

        public void OnClick(ButtonEventArg<MainScreenButtonExit> eventArg)
        {
            Presenter.OpenExitConfirmationPopup();
        }
    }
}