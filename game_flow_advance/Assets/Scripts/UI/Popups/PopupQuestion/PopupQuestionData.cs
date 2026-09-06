using Rossoforge.Popups.UI;

namespace RossoGames.Popups.PopupQuestion
{
    public class PopupQuestionData : IPopupData
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ConfirmButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public QuestionResult Result { get; set; }
    }

    public enum QuestionResult
    {
        Cancel,
        Ok,
    }
}
