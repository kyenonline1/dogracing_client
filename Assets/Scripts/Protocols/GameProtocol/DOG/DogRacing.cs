using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.DOG
{
    public class DogRacing
    {
        public byte DogId { get; set; }
        public byte Order { get; set; }
        public float Position { get; set; }
        /// <summary>
        /// Dont use for new version
        /// </summary>
        //public float[] WinFactors { get; set; }
        public byte CurrentSegment { get; set; }

        public Segment[] Segments { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.DogRacing; }
        }

        public static byte[] Serialize(object data)
        {
            DogRacing ene = (DogRacing)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.DogId);
                    writer.Write(ene.Order);
                    writer.Write(ene.Position);

                    //int len_factor = ene.WinFactors.Length;
                    //writer.Write(len_factor);
                    //foreach (var p in ene.WinFactors)
                    //{
                    //    writer.Write(p);
                    //}

                    writer.Write(ene.CurrentSegment);
                    int len = ene.Segments.Length;
                    writer.Write(len);
                    foreach (var p in ene.Segments)
                    {
                        var arr = Segment.Serialize(p);
                        writer.Write(arr.Length);
                        writer.Write(arr);
                    }
                }
                return m.ToArray();
            }
        }

        public static DogRacing Desserialize(byte[] data)
        {
            DogRacing result = new DogRacing();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.DogId = reader.ReadByte();
                    result.Order = reader.ReadByte();
                    result.Position = reader.ReadSingle();

                    //int len_factor = reader.ReadInt32();
                    //result.WinFactors = new float[len_factor];
                    //for (int i = 0; i < len_factor; i++)
                    //{
                    //    result.WinFactors[i] = reader.ReadSingle();
                    //}

                    result.CurrentSegment = reader.ReadByte();
                    int len_pos = reader.ReadInt32();
                    result.Segments = new Segment[len_pos];
                    for (int i = 0; i < len_pos; i++)
                    {
                        int _len = reader.ReadInt32();
                        result.Segments[i] = Segment.Desserialize(reader.ReadBytes(_len));
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("DogId: \"").Append(DogId).Append("\", ");
            log.Append("Order: \"").Append(Order).Append("\", ");
            log.Append("Position: \"").Append(Position).Append("\", ");
            log.Append("Segments:[ \"");
            for (int i = 0; i < Segments.Length; i++)
            {
                log.Append(Segments[i].ToString());
            }
            log.Append("\"], ");
            log.Append("CurrentSegment: \"").Append(CurrentSegment).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }

    public class Segment
    {
        public int Time { get; set; }
        public int Position { get; set; }
        public Segment(int time, int pos)
        {
            Time = time;
            Position = pos;
        }
        public Segment()
        {
        }
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.Segment; }
        }
        public static byte[] Serialize(object data)
        {
            Segment ene = (Segment)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.Time);
                    writer.Write(ene.Position);
                }
                return m.ToArray();
            }
        }

        public static Segment Desserialize(byte[] data)
        {
            Segment result = new Segment();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Time = reader.ReadInt32();
                    result.Position = reader.ReadInt32();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Time: \"").Append(Time).Append("\", ");
            log.Append("Position: \"").Append(Position).Append("\"}");

            return log.ToString();
        }
    }
}
