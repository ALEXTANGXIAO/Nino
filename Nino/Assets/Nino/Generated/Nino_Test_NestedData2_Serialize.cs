/* this is generated by nino */
namespace Nino.Test
{
    public partial class NestedData2
    {
        #region NINO_CODEGEN
        private object[] NinoGetMembers()
        {
            return new object[] { name,ps,vs };
        }


        private void NinoSetMembers(object[] data)
        {
            this.name = (System.String)data[0];
            this.ps = (Nino.Test.Data[])data[1];
            this.vs = (System.Collections.Generic.List<System.Int32>)data[2];
        }
        #endregion
    }
}