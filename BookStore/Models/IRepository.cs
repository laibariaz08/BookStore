namespace BookStore.Models
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entiy);
        void Update(TEntity entity);
        List<TEntity> GetAll();
        void Delete(object name);
        TEntity GetById(object id);
        List<TEntity> Search(object searchString);
    }
}
