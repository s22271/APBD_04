public interface IDatabaseService
{
    public IEnumerable<Animal> GetAnimals(string orderBy);
    public int AddAnimal(Animal newAnimal);

    public int UpdateAnimal(int id, Animal updatedAnimal);

    public int DeleteAnimal(int id);

}
