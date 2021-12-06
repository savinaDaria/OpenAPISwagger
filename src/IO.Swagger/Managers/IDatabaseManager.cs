using IO.Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IO.Swagger.Managers
{
    public interface IDatabaseManager
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Database? GetCurrentDatabase();
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public bool CreateDatabase(string DBName);
        public bool AddTable(string TableName);
        public bool AddTable(Table newTable);
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Table? GetTable(int index);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public bool AddFieldToTheTable(int tableInd, string Name, string TypeName);
        public bool AddRecordToTheTable(int tableInd);
        public bool EditValue(string newValue, int tableInd, int fieldInd, int recordInd);
        public bool DeleteRecordInTheTable(int tableInd, int recordInd);
        public bool DeleteFieldInTheTable(int tableInd, int fieldInd);
        public bool DeleteTable(int tableInd);
        public bool SaveCurrentDatabase();
        public bool LoadDatabase(string databaseName);
        public bool RemoveDatabase(string databaseName);
        public List<string> GetTablesNameList();
        public Table TablesSubtract(Table firstTable, Table secondTable);
    }

}
