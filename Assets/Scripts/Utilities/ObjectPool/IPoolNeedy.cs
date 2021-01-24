namespace Utilities.ObjectPool
{
    public interface IPoolNeedy
    {
        public ObjectPool pool { get; set; }
    }
}