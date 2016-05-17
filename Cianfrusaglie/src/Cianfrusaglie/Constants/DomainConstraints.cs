using Microsoft.AspNet.Identity;

namespace Cianfrusaglie.Constants {
    /// <summary>
    ///     Classe contenente i vincoli per i Model e i ViewModel. Se ritenete necessario aggiungerne fate pure, basta che le
    ///     aggiungete anche nei Model/ViewModel relativi :)
    /// </summary>
    public static class DomainConstraints {
        //Constraints per Announce/CreateAnnounceViewModel
        public const int AnnounceTitleMinLenght = 3;
        public const int AnnounceTitleMaxLenght = 80;
        public const int AnnounceDescriptionMaxLenght = 255;
        public const int AnnounceMeterRangeMinLenght = 0;
        public const int AnnounceMeterRangeMaxLenght = int.MaxValue;
        public const int AnnouncePriceMinLenght = 0;
        public const int AnnouncePriceMaxLenght = int.MaxValue;
        public const long AnnouncePhotosMaxLenght = 10000000;

        //Constrains per AnnounceFormFieldsValues
        public const int AnnounceFormFieldsValuesValueMaxLength = 99;

        //Constrains per FeedBack
        public const int FeedBackVoteMinRange = 0;
        public const int FeedBackVoteMaxRange = 5;
        public const int FeedBackTextMaxLenght = 99;

        //Constrains per Gat
        public const int GatTextMinLenght = 3;
        public const int GatTextMaxLenght = 30;

        //Constrains per ImageUrl
        public const int ImageUrlUrlMaxLenght = 2083;

        //Constrains per RegisterViewModel/ResetPasswordViewModel/ChangePasswordViewModel/SetPasswordViewModel
        public const int UserUserNameMinLenght = 3;
        public const int UserUserNameMaxLenght = 32;
        public const int UserPasswordMinLengh = 6;
        public const int UserPasswordMaxLengh = 100;
    }
}
