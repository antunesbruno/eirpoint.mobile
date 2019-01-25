using Eirpoint.Mobile.Shared.Interfaces;
using Platform.Ioc.Injection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Shared
{
    public sealed class CommonPlatform
    {
        static CommonPlatform()
        {
            Configuration = Injector.Resolver<IConfiguration>();
        }

        public static IConfiguration Configuration { get; set; }
    }  
}
