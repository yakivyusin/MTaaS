namespace MTaaS.Contracts.Metamorphoses
{
    public interface IOutputMetamorphosis<T>
    {
        T Transform(T output);
    }
}
