using System;
using System.IO;

namespace ACTAP.Extensions
{
    public static class StreamExt
    {
        public static byte[] GetByteArray(this Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            try
            {
                using (stream)
                {
                    if (stream is MemoryStream memoryStream)
                    {
                        return memoryStream.ToArray();
                    }

                    using (memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
