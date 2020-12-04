using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileApp.Models
{
    public class GlobleFunction
    {
        public async Task<bool> RequestCameraPermission()
        {
            bool resp = false ;
            var result = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (result != PermissionStatus.Granted)
            {
                var permissionStatus = await Permissions.RequestAsync<Permissions.Camera>();
                if (permissionStatus == PermissionStatus.Granted)
                {
                    resp = true;
                }
            }
            else
            {
                resp = true;
            }
            return resp;
        }
        public void SendCrashReport(Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }
}
