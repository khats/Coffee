namespace Coffee.Shared
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class Extenders
    {
        public static byte[] ToByteArray(this object a, bool failIfNull = true)
        {
            if (a == null)
            {
                if (failIfNull)
                {
                    throw new ArgumentNullException("a");
                }

                return null;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, a);
                return ms.ToArray();
            }
        }

        public static object AsObject(this byte[] a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(a, 0, a.Length);
                ms.Position = 0;
                return new BinaryFormatter().Deserialize(ms);
            }
        }
 
    }
}