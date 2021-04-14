using System;
using System.Collections.Generic;

namespace ShowList.Api.Configuration
{
    
    // <summary>
    // "ShowListService": {
    //     "Endpoint": "http://localhost:6666",
    //     "Routes": {
        //     "Show" : "/api/show/",
        //     "Vote" : "/api/vote/",
        //     "Raffle" : "/api/raffle/"
    //      }
    // }
    // </summary>
    public class ShowServiceConfig
    {
        public const string ShowListService = "ShowListService";
        
        public string Endpoint { get; set; }
        
        public ShowServiceRoutes Routes { get; set; }

        public Uri BaseAddress => new Uri(Endpoint);
    }

    public class ShowServiceRoutes
    {
        public string Show { get; set; }
        public string Vote { get; set; }
        public string Raffle { get; set; }
    }
}