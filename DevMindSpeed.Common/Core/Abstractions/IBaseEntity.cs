namespace DevMindSpeed.Common.Core.Abstractions
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}