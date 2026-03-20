namespace labs_RPM.Interfaces;

public interface IStorageAdapter
{
    void Save(string path, string content);
    void Delete(string path);
}
