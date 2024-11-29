using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGaleriaFotos
{
    public class FotosDatabase
    {
        SQLiteAsyncConnection Database;

        public FotosDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Postagem>();
        }

        public async Task<List<Postagem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Postagem>().OrderByDescending(i => i.Id).ToListAsync();
        }

        public async Task<Postagem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Postagem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Postagem item)
        {
            await Init();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Postagem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}

