namespace MP.CSharpUtilities.ObjectPool
{
    public interface IPoolable
    {
        void OnGet();

        void OnRecycle();
    }

    public interface TPoolAllocator<T>
    {
        T CreateNew();
    }
}
