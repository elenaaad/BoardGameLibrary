using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private BoardGameLibraryContext context;

        public UnitOfWork(BoardGameLibraryContext context)
        {
            this.context = context;

            BoardGames = new BoardGameRepository(context);
            Producers = new ProducerRepository(context);
            Collections = new CollectionRepository(context);
            BoardGamesInCollection = new BoardGameInCollectionRepository(context);
            Users = new UserRepository(context);
           
        }

        public IBoardGameRepository BoardGames { get; private set; }

        public IProducerRepository Producers { get; private set; }

        public ICollectionRepository Collections { get; private set; }

        public IBoardGameInCollectionRepository BoardGamesInCollection { get; private set; }

        public IUserRepository Users { get; private set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
