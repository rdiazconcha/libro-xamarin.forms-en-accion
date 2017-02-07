using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Surveys.Core.ServiceInterfaces;
using Surveys.UWP.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(GeolocationService))]

namespace Surveys.UWP.Services
{
    public class GeolocationService : IGeolocationService
    {
        public async Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            var geolocator = new Geolocator();
            var position = await geolocator.GetGeopositionAsync();
            var result = new Tuple<double, double>(position.Coordinate.Point.Position.Latitude,
                position.Coordinate.Point.Position.Longitude);

            return result;
        }
    }
}