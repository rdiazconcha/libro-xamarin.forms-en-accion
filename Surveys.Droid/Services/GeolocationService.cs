using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Locations;
using Surveys.Core.ServiceInterfaces;
using Surveys.Droid.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(GeolocationService))]

namespace Surveys.Droid.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly LocationManager locationManager = null;

        public GeolocationService()
        {
            locationManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.LocationService) as LocationManager;
        }

        public Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            var provider = locationManager.GetBestProvider(new Criteria() { Accuracy = Accuracy.Fine }, true);

            var location = locationManager.GetLastKnownLocation(provider);

            var result = new Tuple<double, double>(location.Latitude, location.Longitude);

            return Task.FromResult(result);
        }
    }
}