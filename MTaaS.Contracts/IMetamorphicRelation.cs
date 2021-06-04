namespace MTaaS.Contracts
{
    public interface IMetamorphicRelation<TInput, TOutput>
    {
        bool Validate(TInput input);
    }
}
