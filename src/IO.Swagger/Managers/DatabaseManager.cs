using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IO.Swagger.Models;

namespace IO.Swagger.Managers
{
    public class DatabaseManager: IDatabaseManager
    {
        private Database chosenDatabase;
        private DriveManager driveManager;
        private readonly string path = "D:/Databases/";
        public DatabaseManager()
        {
            driveManager = new DriveManager();
            driveManager.CreateDirectory(path);
        }

        public Database? GetCurrentDatabase()
        {
            return chosenDatabase;
        }

        public bool CreateDatabase(string DBName)
        {
            if (DBName.Trim().Equals(""))
            {
                return false;
            }
            chosenDatabase = new Database { Name = DBName, Tables = new List<Table>() };
            return true;
        }

        public bool AddTable(string TableName)
        {
            if (chosenDatabase == null)
            {
                return false;
            }
            return chosenDatabase.AddTable(TableName);
        }

        public bool AddTable(Table newTable)
        {
            if (chosenDatabase == null)
            {
                return false;
            }
            return chosenDatabase.AddTable(newTable);
        }


        public Table? GetTable(int index)
        {
           return chosenDatabase.GetTable(index);
        }

        public bool AddFieldToTheTable(int tableInd, string Name, string TypeName)
        {
            if (chosenDatabase == null)
            {
                return false;
            }
            return chosenDatabase.AddField(tableInd, Name, TypeName);
        }

        public bool AddRecordToTheTable(int tableInd)
        {
            if (chosenDatabase == null)
            {
                return false;
            }
            return chosenDatabase.AddRecordToTheTable(tableInd);
        }

        public bool EditValue(string newValue, int tableInd, int fieldInd, int recordInd)
        {
            return chosenDatabase.EditValue(newValue, tableInd, fieldInd, recordInd);
        }

        public bool DeleteRecordInTheTable(int tableInd, int recordInd)
        {
            return chosenDatabase.DeleteRecord(tableInd, recordInd);
        }

        public bool DeleteFieldInTheTable(int tableInd, int fieldInd)
        {
            return chosenDatabase.DeleteField(tableInd, fieldInd);
        }

        public bool DeleteTable(int tableInd)
        {
           return chosenDatabase.DeleteTable(tableInd);
        }

        public bool SaveCurrentDatabase()
        {
            return driveManager.SaveDatabaseOnDrive(path, chosenDatabase);
        }

        public bool LoadDatabase(string databaseName)
        {
            chosenDatabase = driveManager.LoadDatabaseFromDrive(path, databaseName);
            return chosenDatabase != null;
        }
        
        public bool RemoveDatabase(string databaseName)
        {
            chosenDatabase = null;
             return driveManager.RemoveDatabaseFromDrive(path, databaseName);
             
        }

        public List<string> GetTablesNameList()
        {
            return chosenDatabase.GetTablesNamesList();
        }



        public Table TablesSubtract(Table firstTable, Table secondTable)
        {
            bool equal = true;
            var firstTableFields = firstTable.GetFields();
            var secondTableFields = secondTable.GetFields();

            Table resultTable = new Table();
            List<Record> firstTableRecords_res = new List<Record>();
            for (int i = 0; i < firstTableFields.Count; ++i)
            {
                if (!secondTableFields.Contains(firstTableFields[i]))
                {
                    equal = false;
                    break;
                }
            }
            if (equal)
            {
                var firstTableRecords = firstTable.GetRecords();

                firstTableRecords_res.AddRange(firstTableRecords);
                var secondTableRecords = secondTable.GetRecords();

                foreach (var record1 in firstTableRecords)
                {
                    foreach (var record2 in secondTableRecords)
                    {
                        bool flag = false;
                        for (int i = 0; i < record1.ValuesList.Count; i++)
                        {
                            if (record1.ValuesList[i] == record2.ValuesList[i])
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            firstTableRecords_res.Remove(record1);
                        }


                    }

                }
                resultTable = new Table($"{firstTable.GetName()}&{secondTable.GetName()}", firstTableFields, firstTableRecords_res);
            }

            return resultTable;
        }
    }
}
