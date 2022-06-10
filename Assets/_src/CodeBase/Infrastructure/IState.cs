namespace _src.CodeBase.Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}