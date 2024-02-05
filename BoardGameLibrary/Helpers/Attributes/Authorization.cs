using DAL.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BoardGameLibrary.Helpers.Attributes
{
    public class Authorization : Attribute, IAuthorizationFilter
    {
        private readonly ICollection<Role> _roles;
        public Authorization(params Role[] roles)
        {
            _roles = roles;
        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusObject = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            if (_roles == null)
            {
                context.Result = unauthorizedStatusObject;
            }

            var user = (User)context.HttpContext.Items["User"];
            if (user == null || !_roles.Contains((Role)user.Role))
            {
                context.Result = unauthorizedStatusObject;
            }
        }
    }
}