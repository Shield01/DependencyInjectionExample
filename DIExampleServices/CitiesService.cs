using DIExampleServiceContracts;

namespace DIExampleServices
{
    public class CitiesService : ICitiesService, IDisposable
    {
        private Guid _serviceInstanceId;
        public Guid ServiceInstanceId
        {
            get
            {
                return _serviceInstanceId;
            }
        }

        private List<string> _cities;

        public CitiesService() 
        {
            _cities = new List<string>()
            {
                //Creating dummy data to be used. 
                "London",
                "Paris",
                "New York",
                "Tokyo",
                "Rome"
            };

            _serviceInstanceId = Guid.NewGuid();

            //TODO: Add logic to open database connection
        }

        public List<string> GetCities() 
        {
            return _cities;
        }

        public void Dispose()
        {
            //TODO: Add logic to close database connection
        }
    }
}
