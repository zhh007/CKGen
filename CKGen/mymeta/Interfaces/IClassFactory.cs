using System;
using System.EnterpriseServices;
using System.Data;

namespace MyMeta
{
#if ENTERPRISE
	using System.Runtime.InteropServices;
    using System.Data;
	[ComVisible(false)]
#endif
	public interface IClassFactory 
	{
        IDbConnection CreateConnection();

		IDatabase		CreateDatabase();
		IDatabases		CreateDatabases();
		ITables			CreateTables();
		ITable			CreateTable();
		IColumn			CreateColumn();
		IColumns		CreateColumns();
		IProcedure		CreateProcedure();
		IProcedures		CreateProcedures();
		IView			CreateView();
		IViews			CreateViews();
		IParameter   	CreateParameter();
		IParameters  	CreateParameters();
		IForeignKey  	CreateForeignKey();
		IForeignKeys 	CreateForeignKeys();
		IIndex       	CreateIndex();
		IIndexes     	CreateIndexes();
		IResultColumn	CreateResultColumn();
		IResultColumns	CreateResultColumns();
		IDomain			CreateDomain();
		IDomains		CreateDomains();
		IProviderTypes	CreateProviderTypes();
		IProviderType	CreateProviderType();
	}
}
