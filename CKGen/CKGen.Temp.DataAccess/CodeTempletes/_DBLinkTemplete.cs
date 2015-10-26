
namespace CKGen.Temp.DataAccess.CodeTempletes
{
    public partial class DBLinkTemplete
    {
        public string NameSpace;
        public string DBLinkName;

        public DBLinkTemplete(string n, string _dbLinkName)
        {
            NameSpace = n;
            DBLinkName = _dbLinkName;
        }
    }
}
