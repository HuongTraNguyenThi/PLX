using System;


namespace PLX.API.Constants
{
    public static class ResultCodeConstants
    {
        #region Common result codes
        public const string EInternalServerError = "10001";
        public const string ENullOrEmptyValue = "10002";
        public const string EInvalidDateFormat = "10003";
        public const string EInvalidPhoneFormat = "10004";
        public const string Success = "10005";
        public const string EUnauthorized = "10006";
        public const string ValidationExist = "10007";


        #endregion

        #region Authentication Result Codes
        public const string AuthEInvalidOTP = "11001";
        public const string AuthEWrongUserOrPassword = "11002";
        public const string AuthValidOTP = "11003";
        public const string AuthSuccessLogin = "11004";
        public const string AuthUnsupportedOTPType = "11005";

        #endregion

        #region Register Result Codes
        public const string RegisterSuccess = "12001";
        public const string RegisterError = "12002";
        #endregion
    }
}