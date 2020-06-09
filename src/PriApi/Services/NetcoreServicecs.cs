using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Services
{
    public class NetcoreService : INetcoreService
    {
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;

        public Primavera _Primavera { get; set; }

        public NetcoreService(IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions,
            IOptions<Primavera> primavera
            )
        {
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
            _Primavera = primavera.Value;
        }

        

    }
}
