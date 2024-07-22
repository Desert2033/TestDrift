public interface IStateWithParameter<TParameter> : IExitableState
{
    void Enter(TParameter parameter);
}