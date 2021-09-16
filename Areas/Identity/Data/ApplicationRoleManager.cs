using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.Areas.Identity.Data
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole> , IApplicationRoleManager
    {
        private readonly IdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationRoleManager> _logger;
        private readonly IEnumerable<IRoleValidator<ApplicationRole>> _roleValidators;
        private readonly IRoleStore<ApplicationRole> _store;

        public ApplicationRoleManager(
            IRoleStore<ApplicationRole> store,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationRoleManager> logger,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            IdentityErrorDescriber errors) :
            base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _errors = errors;
            _keyNormalizer = keyNormalizer;
            _logger = logger;
            _store = store;
            _roleValidators = roleValidators;
        }


        public List<ApplicationRole> GetAllRoles()
        {
            return Roles.ToList();
        }


        public List<RolesViewModel> GetAllRolesAndUsersCount()
        {
            return Roles.Select(role =>
                             new RolesViewModel
                             {
                                 RoleID = role.Id,
                                 RoleName = role.Name,
                                 Description = role.Description,
                                 UsersCount = role.Users.Count(),
                             }).ToList();
        }
    }
}
