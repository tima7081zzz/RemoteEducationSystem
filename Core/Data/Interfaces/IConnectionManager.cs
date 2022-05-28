namespace Data.Interfaces;

public interface IConnectionManager
{
    IConnectionScope GetConnection();
    IConnectionScope GetReadConnection();
}