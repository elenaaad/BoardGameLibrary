using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBoardGameRepository BoardGames { get; }
        IProducerRepository Producers { get; }
        ICollectionRepository Collections { get; }
        IUserRepository Users { get; }

        IBoardGameInCollectionRepository BoardGamesInCollection { get; }    
        int Save();
    }
}



