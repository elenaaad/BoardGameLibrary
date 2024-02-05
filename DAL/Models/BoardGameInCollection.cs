using DAL.Models;
using System.Reflection;

namespace DAL.Models
{
    public class BoardGameInCollection
    {
        public Guid BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
        public Guid? CollectionId { get; set; }
        public Collection? Collection{ get; set; }
    }
}