
public interface IProbaRepository : DataBaseRepository<string, Proba>{
    Arbitru FindArbitru(string id);
    void SetArbitruForProba(string id,long arbitru);
}
