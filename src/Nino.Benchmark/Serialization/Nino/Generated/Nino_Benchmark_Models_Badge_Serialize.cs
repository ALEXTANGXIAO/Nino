/* this is generated by nino */
namespace Nino.Benchmark.Models
{
    public partial class Badge
    {
        public static Badge.SerializationHelper NinoSerializationHelper = new Badge.SerializationHelper();
        public class SerializationHelper: Nino.Serialization.NinoWrapperBase<Badge>
        {
            #region NINO_CODEGEN
            public override void Serialize(Badge value, Nino.Serialization.Writer writer)
            {
                writer.CompressAndWrite(value.BadgeId);
                writer.Write(value.Name);
                writer.Write(value.Description);
                writer.CompressAndWrite(value.AwardCount);
                writer.Write(value.Link);
            }

            public override Badge Deserialize(Nino.Serialization.Reader reader)
            {
                Badge value = new Badge();
                value.BadgeId =  (System.Int32)reader.DecompressAndReadNumber();
                value.Name = reader.ReadString();
                value.Description = reader.ReadString();
                value.AwardCount =  (System.Int32)reader.DecompressAndReadNumber();
                value.Link = reader.ReadString();
                return value;
            }
            #endregion
        }
    }
}