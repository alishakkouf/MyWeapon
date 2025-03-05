using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MyWeapon.Common;
using MyWeapon.Domain.Tenants;
using MyWeapon.Shared;
using MyWeapon.Tenants.Dtos;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MyWeapon.Tenants
{
    //[Authorize]
    public class TenantController : BaseApiController
    {
        private readonly ITenantManager _tenantManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TenantController(ITenantManager tenantManager, ILogger logger,
            IMapper mapper,
            IStringLocalizerFactory factory) : base(factory)
        {
            _tenantManager = tenantManager;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create new Tenant
        /// </summary>
        [HttpPost("CreateTenant")]
        //[Authorize(Roles = Constants.SuperAdminRoleName)]
        public async Task<ActionResult<TenantDto>> CreateNewTenantAsync([FromBody] CreateTenantDto input)
        {
            _logger.Information($"Executing Create new Tenant {input.Name}");

            var result = await _tenantManager.CreateTenantAsync(_mapper.Map<CreateTenantCommand>(input));

            return Ok(_mapper.Map<TenantDto>(result));
        }

    }
}
