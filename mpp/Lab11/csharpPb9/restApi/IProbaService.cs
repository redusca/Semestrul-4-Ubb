namespace restApi
{
    public interface IProbaService
    {
        IEnumerable<Proba> GetAll();
        Proba GetById(string id);
        string Add(string nume, Categorie categorie);
        Proba Update(string id,string nume,Categorie categorie);
        Proba Delete(string id);

        string getNextId(Categorie categorie);
    }
}
