namespace MakinaTurkiye.Utilities.VideoHelpers
{
    public static class VideoHelper
    {
        public static string GetVideoPath(string videoPath)
        {
            return string.Format("//s.makinaturkiye.com/NewVideos/{0}.mp4", videoPath);
            //return string.Format("/UserFiles/NewVideos/{0}.mp4", videoPath); 
        }
    }
}
