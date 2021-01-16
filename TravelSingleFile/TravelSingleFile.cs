// original code by @dfurmans: https://scalafiddle.io/sf/ZPLl2L1/4

using System;

var travel = TravelServiceLiftedImp.Instance.CreateTravelRequest("Łódź", "Malaga");
var database = new TravelRepositoryArangoDb();
Console.WriteLine(travel);

public interface ITravel { }
public interface IRepository<T> 
{ 
    public T Save(T a); 
}
public interface ITravelRepository : IRepository<ITravel> { }

public class TravelRepositoryArangoDb : ITravelRepository
{
    public ITravel Save(ITravel t)
    {
        Console.WriteLine($"Saving travel { t }");
        return t;
    }
}

public interface ITravelServiceLifted
{
    public Func<ITravelRepository, ITravel> CreateTravelRequest(string from, string to);
}

public class TravelServiceLiftedImp : ITravelServiceLifted
{
    private static readonly TravelServiceLiftedImp _instance = new TravelServiceLiftedImp();

    private TravelServiceLiftedImp() { }

    public static TravelServiceLiftedImp Instance => _instance;

    public Func<ITravelRepository, ITravel> CreateTravelRequest(string from, string to) =>
        tr =>
        {
            return tr.Save(new SimpleTravel(from, to));
        };

    public record SimpleTravel(string From, string To) : ITravel;
}
