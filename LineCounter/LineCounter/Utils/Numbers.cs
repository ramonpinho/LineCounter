namespace LineCounter.Utils
{
    /// <summary>
    /// One of the Utils class. Utils.Number contains a few formatting number functions.
    /// </summary>
    public class Numbers
    {
        public static string FormatByteLength(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes.ToString("#,###")} Bytes";
            else if (bytes < 1048576)
            {
                double value = bytes / 1024.0;
                return $"{value.ToString("#,###.##")} KB";
            }
            else
            {
                double value = bytes / 1048576.0;
                return $"{value.ToString("#,###.##")} MB";
            }
        }
    }
}
