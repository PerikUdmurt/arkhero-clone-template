namespace ArkheroClone.Infrastructure
{
    public interface IPayloadedState<TPayload>: IExitableState
    {
        void Enter(TPayload payload);
    }
}