using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class BinarySerializer<T>
    {
        public byte[] Serialize(T data)
        {
            byte[] ret = null;

            if (data != null)
            {
                MemoryStream streamMemory = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(streamMemory, data); ret = streamMemory.GetBuffer();
            }
            return ret;
        }

        public T Deserialize(byte[] binData)
        {
            T retorno = default(T);
            if (binData != null || binData.Length != 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(binData);
                retorno = (T)formatter.Deserialize(ms);
            }
            return retorno;
        }
    }

}
