namespace domain.translate.Configurations
{
    using System.IO;
    using System.Threading;
    public class TranslateConfigurations
    {
        public string Extension { get; set; } = ".xml";
        public string Culture { get { return Thread.CurrentThread.CurrentUICulture.Name; } }
        public string Path { get; set; } = @$"{Directory.GetCurrentDirectory().Split("bin")[0]}\Globalization\";
    }
}
