namespace ArkheroClone.Infrastructure
{
    public interface IState: IExitableState
    {
        void Enter();
    }
}