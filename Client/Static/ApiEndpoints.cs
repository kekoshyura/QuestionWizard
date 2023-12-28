namespace Client.Static {
    internal static class ApiEndpoints {
#if DEBUG 
        internal const string ServerBaseUrl = "https://localhost:7171";
#else
        internal const string ServerBaseUrl = "https://appname.azurewebsite.net";
#endif

        internal readonly static string s_sections = $"{ServerBaseUrl}/api/sections";
        internal readonly static string s_surveys = $"{ServerBaseUrl}/api/surveys";
        internal readonly static string s_imageUpload = $"{ServerBaseUrl}/api/imageupload";
    }
}
