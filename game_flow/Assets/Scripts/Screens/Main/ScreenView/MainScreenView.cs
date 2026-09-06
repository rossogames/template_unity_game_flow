using Rossoforge.Controls.Buttons;
using Rossoforge.Screens.UI;

namespace Rossogames.Main.ScreenView
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