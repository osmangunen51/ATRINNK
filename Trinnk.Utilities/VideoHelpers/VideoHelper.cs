namespace Trinnk.Utilities.VideoHelpers
{
    public static class VideoHelper
    {
        public static string GetVideoPath(string videoPath)
        {
            return string.Format("//s.trinnk.com/NewVideos/{0}.mp4", videoPath);
            //return string.Format("/UserFiles/NewVideos/{0}.mp4", videoPath); 
        }
    }
}
