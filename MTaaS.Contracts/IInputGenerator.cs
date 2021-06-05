namespace MTaaS.Contracts
{
    public interface IInputGenerator<TGenerator, TInput>
    {
        TInput Generate(TGenerator model);
    }
}
