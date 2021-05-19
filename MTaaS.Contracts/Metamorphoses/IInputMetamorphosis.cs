namespace MTaaS.Contracts.Metamorphoses
{
    public interface IInputMetamorphosis<T>
    {
        T Transform(T input);
    }
}
