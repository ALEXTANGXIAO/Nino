/* this is generated by nino */
namespace Nino.Test
{
    public partial class CustomTypeTest
    {
        #region NINO_CODEGEN
        public void NinoWriteMembers(Nino.Serialization.Writer writer)
        {
            writer.WriteCommonVal(typeof(UnityEngine.Vector3), this.v3);
            writer.WriteCommonVal(typeof(System.DateTime), this.dt);
            writer.WriteCommonVal(typeof(System.Nullable<System.Int32>), this.ni);
            writer.Write(this.qs);
            writer.WriteCommonVal(typeof(UnityEngine.Matrix4x4), this.m);
            writer.Write(this.dict);
            if(this.dict2 != null)
            {
                writer.CompressAndWrite(this.dict2.Count);
                foreach (var entry in this.dict2)
                {
                     writer.WriteCommonVal(typeof(System.String), entry.Key);
                     entry.Value.NinoWriteMembers(writer);
                }
            }
            else
            {
                writer.CompressAndWrite(0);
            }
        }

        public void NinoSetMembers(object[] data)
        {
            this.v3 = (UnityEngine.Vector3)data[0];
            this.dt = (System.DateTime)data[1];
            this.ni = (System.Nullable<System.Int32>)data[2];
            this.qs = (System.Collections.Generic.List<UnityEngine.Quaternion>)data[3];
            this.m = (UnityEngine.Matrix4x4)data[4];
            this.dict = (System.Collections.Generic.Dictionary<System.String,System.Int32>)data[5];
            this.dict2 = (System.Collections.Generic.Dictionary<System.String,Nino.Test.Data>)data[6];
        }
        #endregion
    }
}