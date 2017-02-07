using System;
using System.Threading.Tasks;

namespace Surveys.Core.ServiceInterfaces
{
    public interface IGeolocationService
    {
        Task<Tuple<double, double>> GetCurrentLocationAsync();
    }
}