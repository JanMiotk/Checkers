using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITableService
    {
        bool CreateTable(string name);
        List<Table> ReturnAllTables();
        bool IsTableNameUniq(string name);
        public Table ReturnTable(string name);
    }
}