namespace DailyCat.Common.Model
{
    using Xamarin.Forms;

    public class Cat
    {
        public string Id { get; set; }

        public ImageSource ImageSource { get; set; }

        public string Url { get; set; }

        public string SourceUrl { get; set; }

        public byte[] ImageBytes { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }
    }
}
