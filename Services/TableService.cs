using Depot;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TableService : ITableService
    {
        private readonly DataBase _dataBase;
        private readonly IUserService _userService;

        public TableService(IUserService userService)
        {
            _userService = userService;
            _dataBase = new DataBase();
        }
        public List<Table> ReturnAllTables()
        {
            return _dataBase.Tables.ToList();
        }

        public bool CreateTable(string name)
        {

            Table table = new Table(name);
            _dataBase.Tables.Add(table);
            _dataBase.SaveChanges();
            return true;

        }

        public bool IsTableNameUniq(string name)
        {
            var table = _dataBase.Tables.FirstOrDefault(x => x.Name == name);
            if (table == null)
            {
                return true;
            }
            return false;
        }

        public Table ReturnTable(string name)
        {
            return _dataBase.Tables.FirstOrDefault(x => x.Name == name);
        }
    }
}
